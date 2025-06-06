namespace MangaDexSharp.Cli.Verbs;

[Verb("stats", HelpText = "Tests the statistics endpoints")]
public class TestStatsOptions
{

}

internal class TestStatsVerb(
    IMangaDex _api,
    IMdJsonService _json,
    ILogger<TestStatsVerb> logger) : BooleanVerb<TestStatsOptions>(logger)
{
    public async Task<bool> Manga()
    {
        const string ID = "8e74e420-b05e-4975-9844-676c7156bd63"; //duo
        var result = await _api.Statistics.Manga(ID);
        if (result.IsError(out var errors))
        {
            logger.LogError("Failed to fetch manga statistics: {error}", errors);
            return false;
        }

        logger.LogInformation("Manga statistics: {data}", _json.Pretty(result));

        var chapters = await _api.Chapter.List(new()
        {
            IncludeUnavailable = true,
            Manga = ID,
        });
        if (chapters.IsError(out errors))
        {
            logger.LogError("Failed to fetch chapters for manga statistics: {error}", errors);
            return false;
        }

        logger.LogInformation("Manga chapters: {data}", _json.Pretty(chapters));
        return true;
    }

    public async Task<bool> Chapter()
    {
        const string ID = "870cdf2c-501f-4f03-ac64-348b587590fe"; //duo chapter 83
        var result = await _api.Statistics.Chapter(ID);
        if (result.IsError(out var errors))
        {
            logger.LogError("Failed to fetch chapter statistics: {error}", errors);
            return false;
        }

        logger.LogInformation("chapter statistics: {data}", _json.Pretty(result));
        return true;
    }

    public async Task<bool> Group()
    {
        const string ID = "a4b1b68a-5c1f-480c-bc74-c56db113ca9c"; //Church of Crim
        var result = await _api.Statistics.ScanlationGroup(ID);
        if (result.IsError(out var errors))
        {
            logger.LogError("Failed to fetch group statistics: {error}", errors);
            return false;
        }

        logger.LogInformation("group statistics: {data}", _json.Pretty(result));
        return true;
    }

    public override async Task<bool> Execute(TestStatsOptions options, CancellationToken token)
    {
        Func<Task<bool>>[] actions = [Manga, Chapter, Group];
        foreach(var action in actions)
            if (!await action()) 
                return false;

        return true;
    }
}
