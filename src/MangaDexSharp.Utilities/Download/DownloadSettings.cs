using System.IO;

namespace MangaDexSharp.Utilities.Download;

using Archives;
using ArchiveFactory = Func<IDownloadSettings, Archives.IArchiveInstance>;
using GroupingFactory = Func<IAsyncEnumerable<DownloadFile>, IAsyncEnumerable<IAsyncGrouping<string?, DownloadFile>>>;


/// <summary>
/// An interface for editing download settings
/// </summary>
public interface IDownloadSettings
{
    /// <summary>
    /// An event that is raised when an image starts downloading
    /// </summary>
    event DownloadDelegate<DownloadFile> OnImageDownloadStarted;

    /// <summary>
    /// An event that is raised when an image is downloaded
    /// </summary>
    event DownloadDelegate<DownloadFile> OnImageDownloadFinished;

    /// <summary>
    /// An event that is raised when an image fails to download
    /// </summary>
    event DownloadDelegate<DownloadFile, Exception> OnImageDownloadFailed;

    /// <summary>
    /// An event that is raised when an archive is created
    /// </summary>
    event DownloadDelegate<string> OnArchiveCreated;

    /// <summary>
    /// An event that is raised when a chapter's image links are fetched
    /// </summary>
    event DownloadDelegate<string[]> OnChapterImagesLoaded;

    /// <summary>
    /// An event that is raised whenever rate-limits are exceeded and a delay is required 
    /// </summary>
    event DownloadDelegate<RateLimit, TimeSpan> OnRateLimited;

    /// <summary>
    /// An event that is raised whenever a rate-limit has been observed and is no longer in effect
    /// </summary>
    event DownloadDelegate<RateLimit> OnRateLimitPassed;

    /// <summary>
    /// An event that is raised when the API response is received
    /// </summary>
    event DownloadDelegate<MangaDexRoot> OnApiResponse;

    /// <summary>
    /// An event that is raised when the download is cancelled
    /// </summary>
    event DownloadDelegate OnCancelled;

    /// <summary>
    /// An event that is raised when an error occurs during the download process
    /// </summary>
    event DownloadDelegate<Exception> OnError;

    /// <summary>
    /// An event that is raised whenever the download instance needs to log something
    /// </summary>
    event DownloadDelegate<LogLevel, string> OnLog;

    /// <summary>
    /// The directory to cache the downloaded images in
    /// </summary>
    string CacheDirectory { get; set; }

    /// <summary>
    /// Whether or not to purge the cache on dispose
    /// </summary>
    bool PurgeCache { get; set; }

    /// <summary>
    /// How long to wait after hitting the rate-limits for downloading images
    /// </summary>
    TimeSpan RateLimitTimeout { get; set; }

    /// <summary>
    /// How many images to download before pausing to avoid rate limits
    /// </summary>
    int RateLimitAfter { get; set; }

    /// <summary>
    /// The total number of times to retry downloading an image before giving up
    /// </summary>
    int MaxRetries { get; set; }

    /// <summary>
    /// The number of images to download in parallel
    /// </summary>
    int ParallelImages { get; set; }

    /// <summary>
    /// The cancellation token to use for the download operation
    /// </summary>
    CancellationToken Token { get; set; }

    /// <summary>
    /// The factory for transforming image names before saving them to the archive
    /// </summary>
    Func<ImageNameTransform, string>? ImageNameFactory { get; set; }

    /// <summary>
    /// Factory for creating the archive instance based on the download settings.
    /// </summary>
    ArchiveFactory ArchiveFactory { get; set; }

    /// <summary>
    /// Factory for grouping downloaded files by a specific key, such as chapter or volume.
    /// </summary>
    GroupingFactory GroupingFactory { get; set; }

    /// <summary>
    /// Whether or not rate limits are enabled for downloading images
    /// </summary>
    bool RateLimitsEnabled { get; }

    /// <summary>
    /// Sets the <see cref="CacheDirectory"/>
    /// </summary>
    /// <param name="dir">The directory</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithCacheDirectory(string dir);

    /// <summary>
    /// Sets the <see cref="CacheDirectory"/> to a temporary cache directory
    /// </summary>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithTempCacheDirectory();

    /// <summary>
    /// Sets the <see cref="PurgeCache"/>
    /// </summary>
    /// <param name="purge">Whether or not to purge the cache </param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithPurgeCache(bool purge = true);

    /// <summary>
    /// Sets the <see cref="FileGroupingType"/>
    /// </summary>
    /// <param name="type">The file grouping type</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithGrouping(FileGroupingType type);

    /// <summary>
    /// Sets the <see cref="RateLimitTimeout"/>
    /// </summary>
    /// <param name="timeout">The duration of the timeout</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithRateLimitTimeout(TimeSpan timeout);

    /// <summary>
    /// Sets the <see cref="RateLimitTimeout"/> in seconds
    /// </summary>
    /// <param name="seconds">The number of seconds to wait</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithRateLimitTimeout(double seconds) => WithRateLimitTimeout(TimeSpan.FromSeconds(seconds));

    /// <summary>
    /// Sets the <see cref="RateLimitAfter"/>
    /// </summary>
    /// <param name="after">How many images to download before pausing to avoid rate limits</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithRateLimitAfter(int after);

    /// <summary>
    /// Disables image-based rate limits. This is not recommended.
    /// </summary>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithNoRateLimits() => WithRateLimitAfter(0).WithRateLimitTimeout(TimeSpan.Zero);

    /// <summary>
    /// Sets the <see cref="Token"/>
    /// </summary>
    /// <param name="token">The cancellation token to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithCancellationToken(CancellationToken token);

    /// <summary>
    /// Sets the <see cref="ImageNameFactory"/> to use for transforming image names before saving them to the archive.
    /// </summary>
    /// <param name="factory">The factory to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithImageNameFactory(Func<ImageNameTransform, string> factory);

    /// <summary>
    /// Sets the <see cref="ImageNameFactory"/> to use for transforming image names to their ordinal index in the chapter.
    /// </summary>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithOrdinalImageName();

    /// <summary>
    /// Sets the number of images to download in parallel.
    /// </summary>
    /// <param name="parallel">The number of images to download</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithParallelImages(int parallel);

    /// <summary>
    /// Sets the maximum number of retries for downloading an image before giving up.
    /// </summary>
    /// <param name="retries">The number of retries</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithMaxRetries(int retries);

    /// <summary>
    /// Sets the instance for creating archives
    /// </summary>
    /// <param name="factory">The factory to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithArchiveFactory(ArchiveFactory factory);

    /// <summary>
    /// Sets the output directory for downloaded files.
    /// </summary>
    /// <param name="directory">The directory to output to</param>
    /// <param name="subDirFactory">The factory for determining sub directory names (key, index)</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithDirectoryOutput(string? directory = null, Func<string?, int, string>? subDirFactory = null);

    /// <summary>
    /// Sets the output to a zip archive in the specified directory.
    /// </summary>
    /// <param name="directory">The directory to put the files in</param>
    /// <param name="archiveNameFactory">The factory for determining archive names (key, index)</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithZipOutput(string? directory = null, Func<string?, int, string>? archiveNameFactory = null);

    /// <summary>
    /// Sets the output to a zip archive (with cbz extension) in the specified directory.
    /// </summary>
    /// <param name="directory">The directory to put the files in</param>
    /// <param name="archiveNameFactory">The factory for determining archive names (key, index)</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithCbzOutput(string? directory = null, Func<string?, int, string>? archiveNameFactory = null);

    /// <summary>
    /// Sets the grouping factory for grouping downloaded files by a specific key, such as chapter or volume.
    /// </summary>
    /// <param name="factory">The factory</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithGroupingFactory(GroupingFactory factory);

    /// <summary>
    /// Sets the logger to use for logging download events and errors.
    /// </summary>
    /// <param name="logger">The logger to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IDownloadSettings WithLogger(ILogger logger);
}

internal class DownloadSettings : IDownloadSettings
{
    private const string DEFAULT_CACHE_DIRECTORY = "md-utils-page-cache";
    private const string DEFAULT_OUTPUT_DIRECTORY = "md-utils-downloads";

    private IDownloadInstance? _instance = null;
    private Func<string?, int, string> _zipArchiveName = (k, i) => FilePrefixBuilder(k, i, "volume");
    private Func<string?, int, string> _directorySubDir = (k, i) => FilePrefixBuilder(k, i, "volume");

    public event DownloadDelegate<DownloadFile> OnImageDownloadStarted = delegate { };
    public event DownloadDelegate<DownloadFile> OnImageDownloadFinished = delegate { };
    public event DownloadDelegate<DownloadFile, Exception> OnImageDownloadFailed = delegate { };
    public event DownloadDelegate<LogLevel, string> OnLog = delegate { };
    public event DownloadDelegate<string> OnArchiveCreated = delegate { };
    public event DownloadDelegate<string[]> OnChapterImagesLoaded = delegate { };
    public event DownloadDelegate<RateLimit, TimeSpan> OnRateLimited = delegate { };
    public event DownloadDelegate<RateLimit> OnRateLimitPassed = delegate { };
    public event DownloadDelegate<MangaDexRoot> OnApiResponse = delegate { };
    public event DownloadDelegate OnCancelled = delegate { };
    public event DownloadDelegate<Exception> OnError = delegate { };

    public string CacheDirectory { get; set; } = DEFAULT_CACHE_DIRECTORY;
    public bool PurgeCache { get; set; } = false;
    public TimeSpan RateLimitTimeout { get; set; } = TimeSpan.FromSeconds(15.0);
    public int RateLimitAfter { get; set; } = 25;
    public CancellationToken Token { get; set; } = CancellationToken.None;
    public Func<ImageNameTransform, string>? ImageNameFactory { get; set; }
    public int ParallelImages { get; set; } = 10;
    public int MaxRetries { get; set; } = 3;
    public ArchiveFactory ArchiveFactory { get; set; } = DefaultArchive;
    public GroupingFactory GroupingFactory { get; set; } = SortVolumes;

    public bool RateLimitsEnabled => RateLimitAfter > 0 && RateLimitTimeout > TimeSpan.Zero;

    public IDownloadSettings WithCacheDirectory(string dir)
    {
        CacheDirectory = dir;
        return this;
    }

    public IDownloadSettings WithCancellationToken(CancellationToken token)
    {
        Token = token;
        return this;
    }

    public IDownloadSettings WithImageNameFactory(Func<ImageNameTransform, string> factory)
    {
        ImageNameFactory = factory;
        return this;
    }

    public IDownloadSettings WithOrdinalImageName()
    {
        static string OrdinalFileName(ImageNameTransform transform)
        {
            int maxLength = transform.TotalPages.ToString().Length;
            string index = transform.Index.ToString().PadLeft(maxLength, '0');
            return $"{index}.{transform.Extension}";
        }

        ImageNameFactory = OrdinalFileName;
        return this;
    }

    public IDownloadSettings WithPurgeCache(bool purge = true)
    {
        PurgeCache = purge;
        return this;
    }

    public IDownloadSettings WithRateLimitAfter(int after)
    {
        if (after < 0) after = 0;
        RateLimitAfter = after;
        return this;
    }

    public IDownloadSettings WithRateLimitTimeout(TimeSpan timeout)
    {
        if (timeout < TimeSpan.Zero) timeout = TimeSpan.Zero;
        RateLimitTimeout = timeout;
        return this;
    }

    public IDownloadSettings WithTempCacheDirectory()
    {
        CacheDirectory = Path.Combine(Path.GetTempPath(), DEFAULT_CACHE_DIRECTORY);
        return this;
    }

    public IDownloadSettings WithParallelImages(int parallel)
    {
        ParallelImages = parallel < 1 ? 1 : parallel;
        return this;
    }

    public IDownloadSettings WithMaxRetries(int retries)
    {
        if (retries < 0) retries = 0;
        MaxRetries = retries;
        return this;
    }

    public IDownloadSettings WithArchiveFactory(ArchiveFactory factory)
    {
        ArchiveFactory = factory;
        return this;
    }

    public IDownloadSettings WithGroupingFactory(GroupingFactory factory)
    {
        GroupingFactory = factory;
        return this;
    }

    public IDownloadSettings WithGrouping(FileGroupingType type)
    {
        switch (type)
        {
            case FileGroupingType.SingleFile:
                GroupingFactory = (items) => items.GroupBy(t => (string?)null);
                _zipArchiveName = (_, _) => "manga-pages";
                _directorySubDir = (_, _) => "manga-pages";
                break;
            case FileGroupingType.Chapters:
                GroupingFactory = (items) => items.GroupBy(t => t.Chapter.Id);
                _zipArchiveName = (key, index) => FilePrefixBuilder(key, index, "chapter");
                _directorySubDir = (key, index) => FilePrefixBuilder(key, index, "chapter");
                break;
            case FileGroupingType.Volumes:
                GroupingFactory = SortVolumes;
                _zipArchiveName = (key, index) => FilePrefixBuilder(key, index, "volume");
                _directorySubDir = (key, index) => FilePrefixBuilder(key, index, "volume");
                break;
            default:
                throw new NotImplementedException();
        }

        return this;
    }

    public IDownloadSettings WithDirectoryOutput(string? directory = null, Func<string?, int, string>? subDirFactory = null)
    {
        return WithArchiveFactory(settings =>
            new DirectoryArchiveInstance(
                settings, 
                directory ?? DEFAULT_OUTPUT_DIRECTORY, 
                subDirFactory ?? _directorySubDir));
    }

    public IDownloadSettings WithZipOutput(string? directory = null, Func<string?, int, string>? archiveNameFactory = null)
    {
        return WithArchiveFactory(settings =>
            new ZipArchiveInstance(
                settings, 
                directory ?? DEFAULT_OUTPUT_DIRECTORY, 
                archiveNameFactory ?? _zipArchiveName));
    }

    public IDownloadSettings WithCbzOutput(string? directory = null, Func<string?, int, string>? archiveNameFactory = null)
    {
        return WithArchiveFactory(settings =>
            new CbzArchiveInstance(
                settings,
                directory ?? DEFAULT_OUTPUT_DIRECTORY,
                archiveNameFactory ?? _zipArchiveName));
    }

    public IDownloadSettings WithLogger(ILogger logger)
    {
        var json = new MdJsonService();
        OnImageDownloadStarted += (instance, item) => logger.LogInformation("Image download started: {File}", item.Url);
        OnImageDownloadFinished += (instance, item) => logger.LogInformation("Image download finished: {File}", item.Url);
        OnImageDownloadFailed += (instance, item, ex) => logger.LogError(ex, "Image download failed: {File}", item.Url);
        OnArchiveCreated += (instance, item) => logger.LogInformation("Archive created: {Archive}", item);
        OnChapterImagesLoaded += (instance, item) => logger.LogInformation("Chapter images loaded: {Images}", string.Join(", ", item));
        OnRateLimited += (instance, limit, delay) => logger.LogWarning("Rate limit exceeded: {Limit} for {Delay}", limit, delay);
        OnRateLimitPassed += (instance, limit) => logger.LogInformation("Rate limit passed: {Limit}", limit);
        OnApiResponse += (instance, response) => logger.LogInformation("API response received: {Response}", json.Pretty(response));
        OnError += (instance, ex) => logger.LogError(ex, "An error occurred during download");
        OnCancelled += (instance) => logger.LogInformation("Download cancelled");
        OnLog += (instance, level, message) => logger.Log(level, message);
        return this;
    }

    public static IAsyncEnumerable<IAsyncGrouping<string?, DownloadFile>> SortVolumes(IAsyncEnumerable<DownloadFile> files)
    {
        static async IAsyncEnumerable<(DownloadFile, double)> Volumize(IAsyncEnumerable<DownloadFile> items)
        {
            double? volume = null;

            await foreach(var item in items)
            {
                double? current = double.TryParse(item.Chapter.Attributes?.Volume, out double v) ? v : null;
                if (volume != current && current is not null)
                    volume = current;

                yield return (item, volume ?? 0);
            }
        }

        return Volumize(files)
            .GroupBy(t => t.Item2 <= 0 ? null : t.Item2.ToString(), t => t.Item1);
    }

    public static IArchiveInstance DefaultArchive(IDownloadSettings settings)
    {
        return new DirectoryArchiveInstance(settings, DEFAULT_OUTPUT_DIRECTORY, (k, i) => FilePrefixBuilder(k, i, "volume"));
    }

    public static string FilePrefixBuilder(string? key, int index, string prefix)
    {
        string?[] parts = [prefix, index.ToString().PadLeft(3, '0'), key];
        return string.Join("-", parts.Where(p => !string.IsNullOrWhiteSpace(p)));
    }

    public void SetInstance(IDownloadInstance instance)
    {
        _instance = instance;
    }

    public void ImageDownloadStarted(DownloadFile item)
    {
        if (_instance is not null)
            OnImageDownloadStarted(_instance, item);
    }

    public void ImageDownloadFinished(DownloadFile item)
    {
        if (_instance is not null)
            OnImageDownloadFinished(_instance, item);
    }

    public void ImageDownloadFailed(DownloadFile item, Exception item2)
    {
        if (_instance is not null)
            OnImageDownloadFailed(_instance, item, item2);
    }

    public void ArchiveCreated(string item)
    {
        if (_instance is not null)
            OnArchiveCreated(_instance, item);
    }

    public void ChapterImagesLoaded(string[] item)
    {
        if (_instance is not null)
            OnChapterImagesLoaded(_instance, item);
    }

    public void RateLimited(RateLimit item1, TimeSpan item2)
    {
        if (_instance is not null)
            OnRateLimited(_instance, item1, item2);
    }

    public void RateLimitPassed(RateLimit item)
    {
        if (_instance is not null)
            OnRateLimitPassed(_instance, item);
    }

    public void ApiResponse(MangaDexRoot item)
    {
        if (_instance is not null)
            OnApiResponse(_instance, item);
    }

    public void Error(Exception ex)
    {
        if (_instance is not null)
            OnError(_instance, ex);
    }

    public void Cancelled()
    {
        if (_instance is not null)
            OnCancelled(_instance);
    }

    public void Log(LogLevel level, string message)
    {
        if (_instance is not null)
            OnLog(_instance, level, message);
    }
}
