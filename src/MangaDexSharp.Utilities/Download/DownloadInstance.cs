using System.Diagnostics;
using System.IO;

namespace MangaDexSharp.Utilities.Download;

using Archives;

/// <summary>
/// Represents a download session
/// </summary>
/// <remarks>Make sure to call <see cref="WaitUntilFinish"/>!</remarks>
public interface IDownloadInstance : IDisposable
{
    /// <summary>
    /// Whether or not the download is currently in progress
    /// </summary>
    bool IsDownloading { get; }

    /// <summary>
    /// How long the download has been running
    /// </summary>
    TimeSpan Elapsed { get; }

    /// <summary>
    /// The total number of images that have been downloaded so far
    /// </summary>
    int TotalDownloaded { get; }

    /// <summary>
    /// The total number of images that are queued for download
    /// </summary>
    int TotalQueued { get; }

    /// <summary>
    /// The total number of images actively being downloaded
    /// </summary>
    int TotalActive { get; }

    /// <summary>
    /// The total number of images that have failed to download
    /// </summary>
    int TotalFailed { get; }

    /// <summary>
    /// The URLs of all of the images
    /// </summary>
    IEnumerable<string> ImageUrls { get; }

    /// <summary>
    /// All of the files that are being downloaded or have been downloaded
    /// </summary>
    IEnumerable<DownloadFile> Files { get; }

    /// <summary>
    /// Adds all of the chapters from the given manga to the download queue.
    /// </summary>
    /// <param name="mangaId">The ID of the manga</param>
    /// <param name="filter">How to filter the chapters</param>
    Task DownloadManga(string mangaId, MangaFeedFilter? filter = null);

    /// <summary>
    /// Adds all of the chapters from the given manga to the download queue.
    /// </summary>
    /// <param name="manga">The manga</param>
    /// <param name="filter">How to filter the chapters</param>
    Task DownloadManga(Manga manga, MangaFeedFilter? filter = null);

    /// <summary>
    /// Adds all of the chapters to the download queue.
    /// </summary>
    /// <param name="chapters">The chapters to queue</param>
    /// <param name="manga">The manga the chapter belongs to</param>
    Task DownloadChapters(ChapterList chapters, Manga? manga = null);

    /// <summary>
    /// Adds a specific chapter to the download queue.
    /// </summary>
    /// <param name="chapterId">The ID of the chapter</param>
    /// <param name="manga">The manga the chapter belongs to</param>
    Task DownloadChapter(string chapterId, Manga? manga = null);

    /// <summary>
    /// Adds a specific chapter to the download queue.
    /// </summary>
    /// <param name="chapter">The chapter</param>
    /// <param name="manga">The manga the chapter belongs to</param>
    /// <returns></returns>
    Task DownloadChapter(Chapter chapter, Manga? manga = null);

    /// <summary>
    /// Adds all of the chapters from the given async enumerable to the download queue.
    /// </summary>
    /// <param name="chapters">The chapters to queue</param>
    /// <param name="manga">The manga the chapter belongs to</param>
    Task DownloadChapters(IAsyncEnumerable<Chapter> chapters, Manga? manga = null);

    /// <summary>
    /// Indicates that all of the chapters have been queued and waits until all of the chapters have been downloaded
    /// </summary>
    /// <remarks>You need to call this once!</remarks>
    Task WaitUntilFinish();
}

internal class DownloadInstance(
    IRateLimitService _rates,
    IMdApiService _api,
    DownloadSettings _settings) : IDownloadInstance
{
    private ImageDownloadQueue? _download;
    private IArchiveInstance? _archive;
    private Task? _readerThread;
    private readonly Stopwatch _watch = new();
    private readonly CancellationTokenSource _cts = new();

    public bool IsDownloading => _watch.IsRunning;

    public TimeSpan Elapsed => _watch.Elapsed;

    public CancellationToken Token => _cts.Token;

    public IImageDownloadQueue DownloadQueue => LazyCreateQueue();

    public int TotalDownloaded => Files.Count(t => t.Status == DownloadStatus.Completed);

    public int TotalQueued => Files.Count(t => t.Status == DownloadStatus.Queued);

    public int TotalActive => Files.Count(t => t.Status == DownloadStatus.Downloading);

    public int TotalFailed => Files.Count(t => t.Status == DownloadStatus.Failed);

    public IEnumerable<string> ImageUrls => Files.Select(t => t.Url);

    public IEnumerable<DownloadFile> Files => DownloadQueue.AllFiles;

    public IImageDownloadQueue LazyCreateQueue()
    {
        if (_download is not null) return _download;

        _settings.SetInstance(this);
        _cts.Token.Register(_settings.Cancelled);
        _download = new ImageDownloadQueue(_api, _settings, _cts.Token);
        _download.Initialize();
        _archive = _settings.ArchiveFactory(_settings);
        _readerThread = Task.Run(async () =>
        {
            var files = _download.Downloaded;
            var grouping = _settings.GroupingFactory(files);
            await foreach(var group in grouping)
                await _archive.AddFiles(group);
            await _archive.Finished();
            _settings.Log(LogLevel.Debug, "Archiving complete");
        }, _cts.Token);
        _settings.Log(LogLevel.Debug, "Download queue created");
        return _download;
    }

    public RateLimitSettings RateLimitSettings()
    {
        return new RateLimitSettings
        {
            TimeoutRequired = _settings.RateLimited,
            TimeoutPassed = _settings.RateLimitPassed,
            ResponseReceived = _settings.ApiResponse
        };
    }

    public Task<T> MakeRequest<T>(Func<IMangaDex, Task<T>> request)
        where T : MangaDexRoot, new()
    {
        return _rates.Request(request, Token, RateLimitSettings());
    }

    public async Task DownloadManga(string mangaId, MangaFeedFilter? filter = null)
    {
        var manga = await MakeRequest(t => t.Manga.Get(mangaId));
        manga.ThrowIfError();
        await DownloadManga(manga.Data, filter);
    }

    public async Task DownloadManga(Manga manga, MangaFeedFilter? filter = null)
    {
        filter ??= new();
        filter.Limit = 500;

        var chapterKeys = new HashSet<string>();
        var chapters = _rates.Request<Chapter, MangaFeedFilter>(
            async (api, filter) => await api.Manga.Feed(manga.Id, filter), 
            filter, 
            chapter =>
            {
                var key = chapter.Attributes?.Chapter ?? string.Empty;

                if (chapterKeys.Contains(key)) return false;

                chapterKeys.Add(key);
                return true;
            },
            RateLimitSettings(),
            Token);

        await DownloadChapters(chapters, manga);
    }

    public async Task DownloadChapters(ChapterList chapters, Manga? manga = null)
    {
        foreach(var chapter in chapters.Data)
        {
            if (Token.IsCancellationRequested) break;

            await DownloadChapter(chapter, manga);
        }
    }

    public async Task DownloadChapter(string chapterId, Manga? manga = null)
    {
        var chapter = await MakeRequest(t => t.Chapter.Get(chapterId));
        chapter.ThrowIfError();
        await DownloadChapter(chapter.Data, manga);
    }

    public async Task DownloadChapter(Chapter chapter, Manga? manga = null)
    {
        var pages = await MakeRequest(t => t.Pages.Pages(chapter.Id));
        pages.ThrowIfError();

        var images = pages.Images;
        if (images.Length == 0) return;

        for (var i = 0; i < images.Length; i++)
        {
            if (Token.IsCancellationRequested) break;

            var file = new DownloadFile(images[i], chapter, i, images.Length, manga);
            await DownloadQueue.Queue(file);
        }

        _settings.ChapterImagesLoaded(images);
    }

    public async Task DownloadChapters(IAsyncEnumerable<Chapter> chapters, Manga? manga = null)
    {
        await foreach(var chapter in chapters.WithCancellation(Token))
        {
            if (Token.IsCancellationRequested) break;

            await DownloadChapter(chapter, manga);
        }
    }

    public async Task WaitUntilFinish()
    {
        if (_readerThread is null)
            throw new InvalidOperationException("Reader thread not initialized. There were no chapters queued.");

        _settings.Log(LogLevel.Information, "Waiting for download to finish...");
        await DownloadQueue.WaitToFinish();
        await _readerThread;
    }

    public void Dispose()
    {
        if (!_cts.IsCancellationRequested)
            _cts.Cancel();
        if (_watch.IsRunning) 
            _watch.Stop();

        _cts.Dispose();
        DownloadQueue.Dispose();

        if (_settings.PurgeCache &&
            Directory.Exists(_settings.CacheDirectory))
            Directory.Delete(_settings.CacheDirectory, true);

        GC.SuppressFinalize(this);
    }
}