namespace MangaDexSharp;

/// <summary>
/// The configuration options for Open-ID Connect (OIDC) services
/// </summary>
public interface IConfigurationOIDC
{
    /// <summary>
    /// The Auth URL service for MangaDex
    /// </summary>
    string AuthUrl { get; }

    /// <summary>
    /// The portion of the URL that indicates the realm to use
    /// </summary>
    string RealmPath { get; }

    /// <summary>
    /// The client ID for the authorization endpoint
    /// </summary>
    string? ClientId { get; }

    /// <summary>
    /// The client secret for the authorization endpoint
    /// </summary>
    string? ClientSecret { get; }

    /// <summary>
    /// The username for password grant requests
    /// </summary>
    string? Username { get; }

    /// <summary>
    /// The password for password grant requests
    /// </summary>
    string? Password { get; }
}

/// <summary>
/// The configuration options for Open-ID Connect (OIDC) services
/// </summary>
public class ConfigurationOIDC : IConfigurationOIDC
{
    /// <summary>
    /// The base URL for the MangaDex authentication service
    /// </summary>
    public const string AUTH_URL = "https://auth.mangadex.org";

    /// <summary>
    /// The base URL for the MangaDex developer authentication service
    /// </summary>
    public const string AUTH_URL_DEV = "https://auth.mangadex.dev";

    /// <summary>
    /// The default portion of the URL that indicates the realm to use
    /// </summary>
    public const string REALM_PATH = "realms/mangadex/protocol/openid-connect/token";

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
    /// Where to the fetch the realm path from in the config file
    /// </summary>
    public static string RealmPathPath { get; set; } = "Mangadex:RealmPath";

    /// <summary>
    /// The Auth URL service for MangaDex
    /// </summary>
    public string AuthUrl { get; set; } = AUTH_URL;

    /// <summary>
    /// The portion of the URL that indicates the realm to use
    /// </summary>
    public string RealmPath { get; set; } = REALM_PATH;

    /// <summary>
    /// The client ID for the authorization endpoint
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// The client secret for the authorization endpoint
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// The username for password grant requests
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The password for password grant requests
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Fetches the OIDC configuration from the provided configuration
    /// </summary>
    /// <param name="config">The configuration to use</param>
    /// <returns>The OIDC configuration</returns>
    public static IConfigurationOIDC FromConfiguration(IConfiguration config)
    {
        return new ConfigurationOIDC
        {
            AuthUrl = config[AuthPath] ?? AUTH_URL,
            RealmPath = config[RealmPathPath] ?? REALM_PATH,
            ClientId = config[ClientIdPath],
            ClientSecret = config[ClientSecretPath],
            Username = config[UsernamePath],
            Password = config[PasswordPath]
        };
    }

    /// <summary>
    /// Fetches the OIDC configuration from the provided configuration
    /// </summary>
    /// <param name="clientId">The client ID for the authorization endpoint</param>
    /// <param name="clientSecret">The client secret for the authorization endpoint</param>
    /// <param name="username">The username for password grant requests</param>
    /// <param name="password">The password for password grant requests</param>
    /// <param name="authUrl">The Auth URL service for MangaDex</param>
    /// <param name="realmPath">The portion of the URL that indicates the realm to use</param>
    /// <returns>The OIDC configuration</returns>
    public static IConfigurationOIDC FromHardCoded(
        string? clientId = null, string? clientSecret = null,
        string? username = null, string? password = null,
        string? authUrl = null, string? realmPath = null)
    {
        return new ConfigurationOIDC
        {
            AuthUrl = authUrl ?? AUTH_URL,
            RealmPath = realmPath ?? REALM_PATH,
            ClientId = clientId,
            ClientSecret = clientSecret,
            Username = username,
            Password = password
        };
    }
}
