namespace MangaDexSharp.Cli.Verbs;

[Verb("recommendations", HelpText = "Test fetching manga recommendations")]
public class RecommendationOptions 
{
    private const string DEFAULT_ID = "9d9c3006-915b-4f7e-8636-9eea6da12ec3";

    [Option('i', "id", Default = DEFAULT_ID, HelpText = "The manga ID to get recommendations for")]
    public string Id { get; set; } = DEFAULT_ID;
}

internal class RecommendationsTestVerb(
    IMangaDex _api,
    ILogger<RecommendationsTestVerb> logger) : BooleanVerb<RecommendationOptions>(logger)
{
    public override async Task<bool> Execute(RecommendationOptions options, CancellationToken token)
    {
        var recs = await _api.Manga.Recommendations(options.Id);
        foreach (var rec in recs.Data)
        {
            var id = rec.Relationships.FirstOrDefault(t => t.Id != options.Id && t.Type == "manga")?.Id;
            _logger.LogInformation("Recommendation: {id} - Score: {score}", id, rec.Attributes?.Score);
        }
        return true;
    }
}
