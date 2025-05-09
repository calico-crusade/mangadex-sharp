namespace MangaDexSharp;

/// <summary>
/// The configuration options for the MangaDex API services
/// </summary>
public interface IConfigurationApi
{
    /// <summary>
    /// The URL for the MangaDex API
    /// </summary>
    string ApiUrl { get; }

    /// <summary>
    /// The User-Agent header to send with requests
    /// </summary>
    string UserAgent { get; }

    /// <summary>
    /// Whether or not to throw an exception if the API returns an error
    /// </summary>
    bool ThrowOnError { get; }
}

/// <summary>
/// The configuration options for the MangaDex API services
/// </summary>
public class ConfigurationApi : IConfigurationApi
{
    /// <summary>
    /// The base API URL for the production MD instance
    /// </summary>
    public const string API_ROOT = "https://api.mangadex.org";

    /// <summary>
    /// The base API URL for the developer sandbox MD instance
    /// </summary>
    public const string API_ROOT_DEV = "https://api.mangadex.dev";

    /// <summary>
    /// The user agent to use for all requests
    /// </summary>
    public const string API_USER_AGENT = "manga-dex-sharp";

    /// <summary>
    /// The default value for whether or not to throw an exception if the API returns an error
    /// </summary>
    public const bool API_THROW_ON_ERROR = false;

    /// <summary>
    /// Where to fetch the API url from in the config file
    /// </summary>
    public static string ApiPath { get; set; } = "Mangadex:ApiUrl";

    /// <summary>
    /// Where to fetch the User-Agent header from in the config file
    /// </summary>
    public static string UserAgentPath { get; set; } = "Mangadex:UserAgent";

    /// <summary>
    /// Where to fetch the ThrowOnError flag from in the config file
    /// </summary>
    public static string ErrorThrownPath { get; set; } = "Mangadex:ThrowOnError";

    /// <summary>
    /// The URL for the MangaDex API
    /// </summary>
    public string ApiUrl { get; set; } = API_ROOT;

    /// <summary>
    /// The User-Agent header to send with requests
    /// </summary>
    public string UserAgent { get; set; } = API_USER_AGENT;

    /// <summary>
    /// Whether or not to throw an exception if the API returns an error
    /// </summary>
    public bool ThrowOnError { get; set; } = API_THROW_ON_ERROR;

    /// <summary>
    /// Fetches the API configuration from the provided configuration
    /// </summary>
    /// <param name="config">The configuration to use</param>
    /// <returns>The API configuration</returns>
    public static IConfigurationApi FromConfiguration(IConfiguration config)
    {
        return new ConfigurationApi
        {
            ApiUrl = config[ApiPath] ?? API_ROOT,
            UserAgent = config[UserAgentPath] ?? API_USER_AGENT,
            ThrowOnError = config[ErrorThrownPath] == "true"
        };
    }

    /// <summary>
    /// Creates a new instance of the API configuration with hardcoded values
    /// </summary>
    /// <param name="apiUrl">The URL for the MangaDex API</param>
    /// <param name="userAgent">The User-Agent header to send with requests</param>
    /// <param name="throwOnError">Whether or not to throw an exception if the API returns an error</param>
    /// <returns>The API configuration</returns>
    public static IConfigurationApi FromHardCoded(
        string? apiUrl = null, string? userAgent = null, bool? throwOnError = null)
    {
        return new ConfigurationApi
        {
            ApiUrl = apiUrl ?? API_ROOT,
            UserAgent = userAgent ?? API_USER_AGENT,
            ThrowOnError = throwOnError ?? API_THROW_ON_ERROR
        };
    }
}
