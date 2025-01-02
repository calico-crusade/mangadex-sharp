
namespace MangaDexSharp.Cli.Verbs;

[Verb("feed-test", HelpText = "Test the feed endpoint to ensure bug fix")]
internal class TestFeedVerbOptions
{
    public const string DEFAULT_ID = "f7888782-0727-49b0-95ec-a3530c70f83b";

    [Option('m', "manga-id", HelpText = "The ID of the manga to test", Default = DEFAULT_ID)]
    public string MangaId { get; set; } = DEFAULT_ID;

    [Option('l', "language", HelpText = "The language to test")]
    public string? Language { get; set; } 
}

internal class TestFeedVerb(
    IMangaDex _api,
    ILogger<TestFeedVerb> logger) : BooleanVerb<TestFeedVerbOptions>(logger)
{
    public override async Task<bool> Execute(TestFeedVerbOptions options, CancellationToken token)
    {
        try
        {
            var filter = new MangaFeedFilter
            {
                Limit = 100,
                Offset = 0,
                TranslatedLanguage = options.Language is not null ? [options.Language] : [],
            };

            int current = 0;
            while (true)
            {
                var chapters = await _api.Manga.Feed(options.MangaId, filter);
                if (chapters.IsError(out string error))
                {
                    _logger.LogError("Error occurred while fetching feed: {Error}", error);
                    return false;
                }

                if (chapters.Data.Count == 0)
                {
                    _logger.LogWarning("Hit end of feed");
                    break;
                }

                current += chapters.Data.Count;
                filter.Offset += filter.Limit;
                _logger.LogInformation("Found {Count} chapters - Total: {current}/{total}", chapters.Data.Count, current, chapters.Total);
                await Task.Delay(250, token);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while requesting chapters from feed");
            return false;
        }
    }
}
