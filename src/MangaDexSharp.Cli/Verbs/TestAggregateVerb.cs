
namespace MangaDexSharp.Cli.Verbs;

[Verb("aggregate-test", HelpText = "Test the aggregate endpoint to ensure bug fix")]
internal class TestAggregateVerbOptions
{
    public const string DEFAULT_ID = "32fdfe9b-6e11-4a13-9e36-dcd8ea77b4e4";

    [Option('m', "manga-id", HelpText = "The ID of the manga to test", Default = DEFAULT_ID)]
    public string MangaId { get; set; } = DEFAULT_ID;

    [Option('l', "language", HelpText = "The language to test")]
    public string? Language { get; set; } 
}

internal class TestAggregateVerb(
    IMangaDex _api,
    IMdJsonService _json,
    ILogger<TestAggregateVerb> logger) : BooleanVerb<TestAggregateVerbOptions>(logger)
{
    public override async Task<bool> Execute(TestAggregateVerbOptions options, CancellationToken token)
    {
        var langs = options.Language is not null ? new string[] { options.Language } : null;
        var manga = await _api.Manga.Aggregate(options.MangaId, langs);

        var json = _json.Pretty(manga);
        _logger.LogInformation("Manga Aggregate: {Json}", json);
        return true;
    }
}
