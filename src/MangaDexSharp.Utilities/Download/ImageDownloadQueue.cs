using CardboardBox.Extensions;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Channels;
using System.Net;

namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// A utility for downloading images
/// </summary>
public interface IImageDownloadQueue : IDisposable
{
    /// <summary>
    /// All of the files that have been queued
    /// </summary>
    IEnumerable<DownloadFile> AllFiles { get; }

    /// <summary>
    /// All of the files that have been downloaded or failed to download.
    /// </summary>
    IAsyncEnumerable<DownloadFile> Downloaded { get; }

    /// <summary>
    /// Initializes the thread that will process the queued downloads.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Marks the queues as complete and waits for the reader to finish processing all downloads.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if <see cref="Initialize"/> has never been called</exception>
    /// <remarks>You should only call this once you've finished queuing all images</remarks>
    Task WaitToFinish();

    /// <summary>
    /// Queues a file for download
    /// </summary>
    /// <param name="file">The file to download</param>
    ValueTask Queue(DownloadFile file);
}

/// <inheritdoc/>
internal class ImageDownloadQueue(
    IMdApiService _api,
    IDownloadSettings _settings,
    CancellationToken _token) : IImageDownloadQueue
{
    private Task? _readerThread;
    private int _rateLimitChecks = 0;
    private readonly ConcurrentBag<DownloadFile> _allFiles = [];
    private readonly Channel<DownloadFile> _queued = Channel.CreateUnbounded<DownloadFile>();
    private readonly Channel<DownloadFile> _downloaded = Channel.CreateUnbounded<DownloadFile>();
    private readonly SemaphoreSlim _imageRateLimitSemaphore = new(1);

    /// <summary>
    /// The settings to use for downloading
    /// </summary>
    internal DownloadSettings? Settings { get; } = _settings as DownloadSettings;

    /// <inheritdoc/>
    public IEnumerable<DownloadFile> AllFiles => _allFiles;

    /// <inheritdoc/>
    public IAsyncEnumerable<DownloadFile> Downloaded => _downloaded.Reader.ReadAllAsync();

    /// <summary>
    /// Generates the cache path for a given URL.
    /// </summary>
    /// <param name="url">The URL to generate for</param>
    /// <returns>The cache path</returns>
    public string GenerateCachePath(string url)
    {
        return Path.Combine(_settings.CacheDirectory, url.MD5Hash() + ".cache");
    }

    /// <summary>
    /// Checks to see if the download queue should be paused to avoid rate limits, and pauses if necessary
    /// </summary>
    /// <param name="token">The cancellation token for the request</param>
    /// <param name="force">Whether or not to force the rate limit to be observed</param>
    public async ValueTask ObserveRateLimit(bool force, CancellationToken token)
    {
        // Check if rate limiting is enabled
        if (!_settings.RateLimitsEnabled) return;

        try
        {
            //Ensure we're observing rate limits one at a time
            await _imageRateLimitSemaphore.WaitAsync(token);
            //Increment the rate limit checks counter
            _rateLimitChecks++;
            //Check to see if we should rate limit
            var shouldLimit = _rateLimitChecks % _settings.RateLimitAfter == 0;
            if (!shouldLimit && !force) return;
            //Create a rate limits object for reporting
            var limits = new RateLimit
            {
                Limit = _settings.RateLimitAfter,
                Remaining = 0,
                RetryAfter = DateTime.UtcNow.Add(_settings.RateLimitTimeout)
            };
            //Report that the rate limit has been reached
            Settings?.RateLimited(limits, _settings.RateLimitTimeout);
            //Wait for the specified timeout before continuing
            await Task.Delay(_settings.RateLimitTimeout, token);
            //Report that the rate limit has passed
            Settings?.RateLimitPassed(limits);
        }
        finally
        {
            // Release the semaphore to allow the next download to proceed
            _imageRateLimitSemaphore.Release();
        }
    }

    /// <summary>
    /// Downloads the given file to the cache location
    /// </summary>
    /// <param name="file">The file to download</param>
    /// <param name="times">The total number of times the request has been retried</param>
    /// <param name="token">The cancellation token for the request</param>
    /// <param name="cache">The output location for the file</param>
    /// <exception cref="NullReferenceException">Thrown if the download returns null</exception>
    public async ValueTask DownloadFile(DownloadFile file, int times, string cache, CancellationToken token)
    {
        //Ensure we're adhering to the image-download rate limit
        await ObserveRateLimit(false, token);
        //Set the total number of retries
        file.TotalRetries = times;
        //Trigger the download for the file
        var result = await _api.Get(file.Url, token: token)
            ?? throw new NullReferenceException("Download null for: " + file.Url);
        if (!result.IsSuccessStatusCode && result.StatusCode == HttpStatusCode.TooManyRequests)
        {
            if (_settings.MaxRetries <= times)
                result.EnsureSuccessStatusCode();

            await DownloadFile(file, times + 1, cache, token);
            return;
        }

        //Ensure the response was successful
        result.EnsureSuccessStatusCode();
        //Create the cache file to store the image
        using var io = File.Create(cache);
        #if NET8_0_OR_GREATER
        //Get the stream from the response
        using var stream = await result.Content.ReadAsStreamAsync(token);
        //Copy it to the cache file
        await stream.CopyToAsync(io, token);
        #else
        throw new NotSupportedException("This is only available on dotnet 8.0 or greater");
        #endif
    }

    /// <summary>
    /// Downloads the given file to the cache location
    /// </summary>
    /// <param name="file">The file to download</param>
    /// <param name="token">The cancellation token for the request</param>
    /// <exception cref="NullReferenceException">Thrown if the download returns null</exception>
    public async ValueTask DownloadFile(DownloadFile file, CancellationToken token)
    {
        try
        {
            //Indicate that the file has started being downloaded
            file.Start();
            //Get the output path for the download
            var cache = GenerateCachePath(file.Url);
            //Indicate the image download has started
            Settings?.ImageDownloadStarted(file);
            //If the image already exists, skip it.
            if (File.Exists(cache))
            {
                file.Complete(cache);
                Settings?.ImageDownloadFinished(file);
                return;
            }
            //Trigger the download of the file
            await DownloadFile(file, 0, cache, token);
            //Complete the download
            file.Complete(cache);
            Settings?.ImageDownloadFinished(file);
        }
        catch (Exception ex)
        {
            //Mark the file as failed
            file.Fail(ex.ToString());
            //Log the error
            Settings?.ImageDownloadFailed(file, ex);
            Settings?.Error(ex);
        }
        finally
        {
            //Add the file to the list of all files
            await _downloaded.Writer.WriteAsync(file, token);
        }
    }

    /// <summary>
    /// Reads from the queue and downloads all of the queued images in parallel.
    /// </summary>
    public async ValueTask ReaderThread()
    {
        try
        {
            #if NET8_0_OR_GREATER
            Settings?.Log(LogLevel.Information, $"Starting reader thread for download queue to `{_settings.CacheDirectory}`...");
            //Create parallelism options for the download
            var settings = new ParallelOptions
            {
                CancellationToken = _token,
                MaxDegreeOfParallelism = _settings.ParallelImages,
            };
            //Fetch all of the queued images
            var queued = _queued.Reader.ReadAllAsync(_token);
            //Trigger the download in parallel
            await Parallel.ForEachAsync(queued, settings, DownloadFile);
            Settings?.Log(LogLevel.Information, "Finished reader thread for download queue");
            #else
            await Task.CompletedTask;
            throw new NotSupportedException("This is only available on dotnet 8.0 or greater");
            #endif
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            Settings?.Error(ex);
            throw;
        }
        finally
        {
            //Mark the download channel as complete
            _downloaded.Writer.TryComplete();
            //Clear the reader thread
            _readerThread = null;
        }
    }

    /// <inheritdoc/>
    public void Initialize()
    {
        #if NET8_0_OR_GREATER
        //Ensure the reader thread isn't already initialized
        if (_readerThread is not null)
            return;

        if (!Directory.Exists(_settings.CacheDirectory))
            Directory.CreateDirectory(_settings.CacheDirectory);

        //Initialize the reader thread to process downloads
        _readerThread = Task.Run(ReaderThread, _token);
        #else
        throw new NotSupportedException("This is only available on dotnet 8.0 or greater");
        #endif
    }

    /// <inheritdoc/>
    public ValueTask Queue(DownloadFile file)
    {
        _allFiles.Add(file);
        return _queued.Writer.WriteAsync(file, _token);
    }

    /// <inheritdoc/>
    public Task WaitToFinish()
    {
        if (_readerThread is null)
            throw new InvalidOperationException("Reader thread not initialized. Call `Initialize()` first.");

        Settings?.Log(LogLevel.Information, "Waiting for download queue to finish...");
        _queued.Writer.TryComplete();
        return _readerThread;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _downloaded.Writer.TryComplete();
        _queued.Writer.TryComplete();
        _imageRateLimitSemaphore.Dispose();
        GC.SuppressFinalize(this);
    }
}
