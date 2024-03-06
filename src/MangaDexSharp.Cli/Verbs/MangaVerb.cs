namespace MangaDexSharp.Cli.Verbs;

[Verb("manga", HelpText = "Fetches a manga's information")]
public class MangaVerbOptions
{
    [Option('i', "id", Required = true, HelpText = "The id of the manga")]
    public string Id { get; set; } = string.Empty;
}

public class MangaVerb(
    IMangaDex _api, 
    ILogger<MangaVerb> logger,
    IMdJsonService _json) 
    : BooleanVerb<MangaVerbOptions>(logger)
{
    public override async Task<bool> Execute(MangaVerbOptions options, CancellationToken token)
    {
        if (string.IsNullOrEmpty(options.Id))
        {
            _logger.LogWarning("Manga ID is required!");
            return false;
        }

        var res = await _api.Manga.Get(options.Id);
        if (res.IsError(out string error, out var manga))
        {
            _logger.LogError("An error occurred while fetching manga: {error}", error);
            return false;
        }

        _logger.LogInformation("Manga Data: {json}", _json.Pretty(manga));
        return true;
    }
}
