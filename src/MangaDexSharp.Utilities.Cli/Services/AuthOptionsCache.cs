
namespace MangaDexSharp.Utilities.Cli.Services;

internal class AuthOptionsCache
{
    public AuthOptions? Auth { get; set; }
}

internal class AuthConfigurationOIDC(
    IConfiguration _config, 
    AuthOptionsCache _auth) : IConfigurationOIDC
{
    private IConfigurationOIDC? _fromConfig;

    public IConfigurationOIDC FromConfig => _fromConfig ??= ConfigurationOIDC.FromConfiguration(_config);

    public string AuthUrl => FromConfig.AuthUrl;

    public string RealmPath => FromConfig.RealmPath;

    public string? ClientId => _auth.Auth?.ClientId ?? FromConfig.ClientId;

    public string? ClientSecret => _auth.Auth?.ClientSecret ?? FromConfig.ClientSecret;

    public string? Username => _auth.Auth?.Username ?? FromConfig.Username;

    public string? Password => _auth.Auth?.Password ?? FromConfig.Password;
}

internal class AuthCredentialsService(
    IConfigurationOIDC _config,
    IOIDCService _auth,
    AuthOptionsCache _authCache,
    ITokenCacheService? _cache = null) : PersonalCredentialsService(_config, _auth, _cache)
{
    public override Task<string?> GetToken()
    {
        if (!string.IsNullOrEmpty(_authCache.Auth?.AccessToken))
            return Task.FromResult(_authCache.Auth?.AccessToken);

        return base.GetToken();
    }
}