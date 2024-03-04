namespace MangaDexSharp;


/// <summary>
/// Represents a provider that fetches the <see cref="ICredentialsService"/> from the configuration
/// </summary>
public class ConfigurationCredentialsService : ICredentialsService
{
    private readonly IConfiguration _config;

    /// <summary>
    /// Where to fetch the API token from in the config file
    /// </summary>
    public static string TokenPath { get; set; } = "Mangadex:Token";

    /// <summary>
    /// Where to fetch the API url from in the config file
    /// </summary>
    public static string ApiPath { get; set; } = "Mangadex:ApiUrl";

    /// <summary>
    /// Where to fetch the Auth URL service from in the config file
    /// </summary>
    public static string AuthPath { get; set; } = "Mangadex:AuthUrl";

    /// <summary>
    /// Where to fetch the client ID from in the config file
    /// </summary>
    public static string ClientIdPath { get; set; } = "Mangadex:ClientId";

    /// <summary>
    /// Where to fetch the client secret from in the config file
    /// </summary>
    public static string ClientSecretPath { get; set; } = "Mangadex:ClientSecret";

    /// <summary>
    /// Where to fetch the username (for the password grant OAuth2 requests) from in the config file
    /// </summary>
    public static string UsernamePath { get; set; } = "Mangadex:Username";

    /// <summary>
    /// Where to fetch the password (for the password grant OAuth2 requests) from in the config file
    /// </summary>
    public static string PasswordPath { get; set; } = "Mangadex:Password";

    /// <summary>
    /// Where to fetch the User-Agent header from in the config file
    /// </summary>
    public static string UserAgentPath { get; set; } = "Mangadex:UserAgent";

    /// <summary>
    /// Where to fetch the ThrowOnError flag from in the config file
    /// </summary>
    public static string ErrorThrownPath { get; set; } = "Mangadex:ThrowOnError";

    /// <summary>
    /// The authentication token from the configuration file
    /// </summary>
    public string? Token => _config[TokenPath];

    /// <summary>
    /// The API url from the configuration file
    /// </summary>
    public string ApiUrl => _config[ApiPath] ?? API_ROOT;

    /// <summary>
    /// The Auth URL service for MangaDex
    /// </summary>
    public string AuthUrl => _config[AuthPath] ?? AUTH_URL;

    /// <summary>
    /// The User-Agent header to send with requests
    /// </summary>
    public string UserAgent => _config[UserAgentPath] ?? API_USER_AGENT;

    /// <summary>
    /// The client ID for the authorization endpoint
    /// </summary>
    public string? ClientId => _config[ClientIdPath];

    /// <summary>
    /// The client secret for the authorization endpoint
    /// </summary>
    public string? ClientSecret => _config[ClientSecretPath];

    /// <summary>
    /// The username for password grant requests
    /// </summary>
    public string? Username => _config[UsernamePath];

    /// <summary>
    /// The password for password grant requests
    /// </summary>
    public string? Password => _config[PasswordPath];

    /// <summary>
    /// Whether or not to throw an exception if the API returns an error
    /// </summary>
    public bool ThrowOnError => _config[ErrorThrownPath] == "true";

    /// <summary>
    /// Represents a provider that fetches the <see cref="ICredentialsService"/> from the configuration
    /// </summary>
    /// <param name="config">The <see cref="IConfiguration"/> object to fetch the variables from</param>
    public ConfigurationCredentialsService(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Fetches the user's authentication token from the config file
    /// </summary>
    /// <returns>The user's authentication token</returns>
    public Task<string?> GetToken()
    {
        return Task.FromResult(Token);
    }
}
