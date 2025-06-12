namespace MangaDexSharp.Utilities.Cli.Verbs;

using Services;

[Verb("check-auth", HelpText = "Check to see if the given authentication is valid.")]
public class CheckAuthOptions : AuthOptions
{

}

internal class CheckAuthVerb(
    ILogger<CheckAuthVerb> logger,
    AuthOptionsCache _cache,
    IMangaDex _md,
    IMdJsonService _json) : BooleanVerb<CheckAuthOptions>(logger)
{
    public override async Task<bool> Execute(CheckAuthOptions options, CancellationToken token)
    {
        _cache.Auth = options;
        var me = await _md.User.Me();
        if (me.IsError(out var errors))
        {
            _logger.LogError("Authentication failed: {Errors}", string.Join(", ", errors));
            return false;
        }

        _logger.LogInformation("Authentication successful! Profile: {profile}", _json.Pretty(me));
        return true;
    }
}