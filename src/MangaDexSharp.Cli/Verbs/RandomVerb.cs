namespace MangaDexSharp.Cli.Verbs;

[Verb("random", HelpText = "Using the random route to test rate limits")]
internal class RandomVerbOptions
{

}

internal class RandomVerb(
    IMangaDex _api,
    IMdJsonService _json,
    ILogger<RandomVerb> logger) : BooleanVerb<RandomVerbOptions>(logger)
{
    public override async Task<bool> Execute(RandomVerbOptions options, CancellationToken token)
    {
        var random = await _api.Manga.Random();
        var pretty = _json.Pretty(random);
        _logger.LogInformation("Random manga: {Random}", pretty);
        return true;
    }
}
