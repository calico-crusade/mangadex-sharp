namespace MangaDexSharp.Cli.Verbs;

[Verb("user", HelpText = "Fetches the current user's information")]
internal class UserVerbOptions { }

internal class UserVerb(
    IMangaDex _api,
    IMdJsonService _json,
    ILogger<UserVerb> logger) : BooleanVerb<UserVerbOptions>(logger)
{
    public override async Task<bool> Execute(UserVerbOptions options, CancellationToken token)
    {
        var res = await _api.User.Me();
        if (res.IsError(out string error, out var user))
        {
            _logger.LogError("An error occurred while fetching user: {error}", error);
            return false;
        }

        _logger.LogInformation("User Data: {json}", _json.Pretty(user));
        return true;
    }
}
