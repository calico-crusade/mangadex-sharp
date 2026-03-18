namespace MangaDexSharp.Cli.Verbs;

[Verb("takedown-test", HelpText = "Test the takedown process.")]
internal class TakedownTestVerbOptions
{

}

internal class TakedownTestVerb(
	IMangaDex _api,
	ILogger<TakedownTestVerb> logger) : BooleanVerb<TakedownTestVerbOptions>(logger)
{
	public override async Task<bool> Execute(TakedownTestVerbOptions options, CancellationToken token)
	{
		try
		{
			var result = await _api.Takedown.Takedowns();
			_logger.LogInformation("Successfully retrieved takedown list with {Count} items.", result.Data.Count);
			return true;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error occurred while testing the takedown process.");
			return false;
		}
	}
}
