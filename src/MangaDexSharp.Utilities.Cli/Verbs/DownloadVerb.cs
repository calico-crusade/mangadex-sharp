using CardboardBox.Extensions;

namespace MangaDexSharp.Utilities.Cli.Verbs;

using Download;

[Verb("download", HelpText = "Download manga chapters from MangaDex.")]
public class DownloadOptions
{
    private const string OUTPUT_TYPE = "zip";
    private const string GROUPING_TYPE = "volumes";
    private const string LANGUAGE = "en";
    private const double RATE_LIMIT_TIMEOUT = 15.0;
    private const int RATE_LIMIT_AFTER = 35;

    [Option('m', "manga-id", HelpText = "The ID of the manga to download chapters from.")]
    public string? MangaId { get; set; }

    [Option('i', "chapter-ids", HelpText = "The IDs of the chapters to download")]
    public IEnumerable<string> ChapterIds { get; set; } = [];

    [Option('c', "cache-directory", HelpText = "The directory to cache images in.")]
    public string? CacheDirectory { get; set; }

    [Option('p', "purge-cache", HelpText = "Whether to purge the cache after downloading images", Default = false)]
    public bool PurgeCache { get; set; } = false;

    [Option('d', "directory", HelpText = "The directory to save downloaded chapters to")]
    public string? Directory { get; set; }

    [Option('o', "output", HelpText = "The output format for the downloaded chapters (EPUB, ZIP, CBZ, DIR)", Default = OUTPUT_TYPE)]
    public string Output { get; set; } = OUTPUT_TYPE;

    [Option('g', "grouping", HelpText = "How to group chapters when creating archives (SingleFile, Volumes, Chapters)", Default = GROUPING_TYPE)]
    public string Grouping { get; set; } = GROUPING_TYPE;

    [Option('t', "rate-limit-timeout", HelpText = "How many seconds to wait after reaching the rate limits for downloading images", Default = RATE_LIMIT_TIMEOUT)]
    public double RateLimitTimeout { get; set; } = RATE_LIMIT_TIMEOUT;

    [Option('a', "rate-limit-after", HelpText = "How many images to download before pausing to avoid rate limits", Default = RATE_LIMIT_AFTER)]
    public int RateLimitAfter { get; set; } = RATE_LIMIT_AFTER;

    [Option('f', "format-image-names", HelpText = "Change the image file names to match the page index instead of the original file names", Default = false)]
    public bool FormatImageNames { get; set; } = false;

    [Option('l', "language", HelpText = "The language to use for the downloaded chapters (default: en)", Default = LANGUAGE)]
    public string? Language { get; set; } = LANGUAGE;

    [Option('p', "preferred-group-ids", HelpText = "If a chapter has multiple versions, the groups specified here will be preferred, otherwise it will be the first chapter returned by the API will be downloaded")]
    public IEnumerable<string> PreferredGroupIds { get; set; } = [];
}

internal class DownloadVerb(
    ILogger<DownloadVerb> logger,
    IRateLimitService _rates,
    IDownloadUtilityService _download) : BooleanVerb<DownloadOptions>(logger)
{
    /// <summary>
    /// Filters the given chapters by the options
    /// </summary>
    /// <param name="options">The options to filter by</param>
    /// <param name="chapters">The chapters to filter</param>
    /// <param name="token">The cancellation token</param>
    /// <returns>The filtered chapters</returns>
    public static async IAsyncEnumerable<Chapter> FilterChapters(
        DownloadOptions options, 
        IAsyncEnumerable<Chapter> chapters, 
        [EnumeratorCancellation] CancellationToken token)
    {
        //Group the chapters by the ordinal value of the chapter attribute
        var withOrdinal = chapters.Select(chapter =>
        {
            var key = double.TryParse(chapter.Attributes?.Chapter, out var value)
                ? value
                : 0.0;
            return (key, chapter);
        }).GroupBy(t => t.key, t => t.chapter);

        //Iterate through the chapters
        await foreach (var group in withOrdinal)
        {
            if (token.IsCancellationRequested) yield break;

            //Get all of the chapters with the same ordinal
            var chaps = await group.ToArrayAsync(token);
            if (chaps.Length == 0) continue;

            //If there is only one chapter, return it
            if (chaps.Length == 1)
            {
                yield return chaps.First();
                continue;
            }

            //If there are multiple chapters, prefer the ones from the specified groups
            var chap = chaps.PreferredOrFirst(t => t.Relationship<ScanlationGroup>().Any(a => options.PreferredGroupIds.Contains(a.Id)));
            if (chap is null) continue;

            yield return chap;
        }
    }

    public async Task Download(IDownloadInstance instance, DownloadOptions options, CancellationToken token)
    {
        async Task DownloadMangaChapters(string mangaId)
        {
            //Fetch the manga's data
            var manga = await _rates.Request(t => t.Manga.Get(mangaId), token);
            manga.ThrowIfError();

            //Setup the filter for the manga's chapters
            var filter = new MangaFeedFilter
            {
                Limit = 500,
                TranslatedLanguage = string.IsNullOrWhiteSpace(options.Language) ? [] : [options.Language!],
                IncludeEmptyPages = false,
                IncludeExternalUrl = false,
                IncludeFutureUpdates = false,
                IncludeUnavailable = false,
                Order = new()
                {
                    [MangaFeedFilter.OrderKey.chapter] = OrderValue.asc
                }
            };
            //Fetch all of the chapters
            var chapters = _rates.Request<Chapter, MangaFeedFilter>(
                async (api, filter) => await api.Manga.Feed(mangaId, filter),
                filter,
                token: token);
            //Filter the chapters so there is only 1 of each ordinal
            var filtered = FilterChapters(options, chapters, token);
            //Download all of the chapters for the manga
            await instance.DownloadChapters(filtered, manga.Data);
        }

        async IAsyncEnumerable<Chapter> DownloadChapters(IEnumerable<string> chaptersIds, Manga? manga)
        {
            //Chunk the chapters into batches of 100
            var chunked = chaptersIds.Batch(100);
            foreach(var chunk in chunked)
            {
                //Setup the filter for this chunk of chapters
                var filter = new ChaptersFilter
                {
                    Limit = 100,
                    Ids = chunk,
                    TranslatedLanguage = string.IsNullOrWhiteSpace(options.Language) ? [] : [options.Language!],
                    IncludeEmptyPages = false,
                    IncludeExternalUrl = false,
                    IncludeFutureUpdates = false,
                    IncludeUnavailable = false,
                    Order = new()
                    {
                        [ChaptersFilter.OrderKey.chapter] = OrderValue.asc
                    }
                };
                //Fetch this batch of chapters
                var chapters = await _rates.Request(t => t.Chapter.List(filter), token);
                foreach(var chapter in chapters.Data)
                {
                    if (token.IsCancellationRequested) yield break;

                    yield return chapter;
                }
            }
        }

        //Shouldn't happen
        if (!options.ChapterIds.Any() && string.IsNullOrEmpty(options.MangaId))
            return;

        //Chapter's aren't specified, but manga is - download all chapters
        if (!options.ChapterIds.Any() && !string.IsNullOrEmpty(options.MangaId))
        {
            await DownloadMangaChapters(options.MangaId);
            return;
        }

        Manga? manga = null;
        if (!string.IsNullOrEmpty(options.MangaId))
        {
            var m = await _rates.Request(t => t.Manga.Get(options.MangaId), token);
            m.ThrowIfError();
            manga = m.Data;
        }
        //Fetch all of the chapters
        var chapters = DownloadChapters(options.ChapterIds, manga);
        //Filter the chapters so there is only 1 of each ordinal
        var filtered = FilterChapters(options, chapters, token);
        //Download all of the chapters
        await instance.DownloadChapters(filtered, manga);
    }

    public override async Task<bool> Execute(DownloadOptions options, CancellationToken token)
    {
        if (string.IsNullOrEmpty(options.MangaId) &&
            !options.ChapterIds.Any())
        {
            logger.LogError("You must specify either a manga ID or chapter IDs to download.");
            return false;
        }

        if (!Enum.TryParse<FileGroupingType>(options.Grouping, true, out var grouping))
        {
            logger.LogError("Invalid grouping type: {options}. Valid values are: SingleFile, Volumes, Chapters.", options.Grouping);
            return false;
        }

        if (!Enum.TryParse<ArchiveType>(options.Output, true, out var archiveType))
        {
            logger.LogError("Invalid output type: {options}. Valid values are: ZIP, CBZ, DIR, EPUB.", options.Output);
            return false;
        }

        using var download = _download.Start(c =>
        {
            c.WithPurgeCache(options.PurgeCache)
             .WithRateLimitTimeout(options.RateLimitTimeout)
             .WithRateLimitAfter(options.RateLimitAfter)
             .WithGrouping(grouping)
             .WithCancellationToken(token)
             .WithLogger(_logger);

            if (!string.IsNullOrEmpty(options.CacheDirectory))
                c.WithCacheDirectory(options.CacheDirectory);

            if (options.FormatImageNames)
                c.WithOrdinalImageName();

            switch(archiveType)
            {
                case ArchiveType.ZIP:
                    c.WithZipOutput(options.Directory?.ForceNull());
                    break;
                case ArchiveType.CBZ:
                    c.WithCbzOutput(options.Directory?.ForceNull());
                    break;
                case ArchiveType.DIR:
                    c.WithDirectoryOutput(options.Directory?.ForceNull());
                    break;
                default:
                    throw new NotSupportedException("Unsupported archive type: " + archiveType);
            }
        });

        await Download(download, options, token);
        await download.WaitUntilFinish();
        _logger.LogInformation("Finished downloading {count} images with {errors} errors", download.TotalDownloaded, download.TotalFailed);
        return true;
    }
}