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

    /// <summary>
    /// Whether or not to handle rate limits within the application
    /// </summary>
    bool HandleRateLimits { get; }

	/// <summary>
	/// Whether or not to use conservative rate limits
	/// </summary>
	bool ConservativeLimits { get; }
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
    /// The default conservative limits value
    /// </summary>
    public const bool API_CONSERVATIVE_LIMITS = true;

    /// <summary>
    /// The default for handling rate limits
    /// </summary>
    public const bool API_HANDLE_RATE_LIMITS = false;

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
    /// Where to fetch the ConservativeLimits flag from in the config file
    /// </summary>
    public static string ConservativeLimitsPath { get; set; } = "Mangadex:RateLimits:Conservative";

	/// <summary>
	/// Where to fetch the HandleRateLimits flag from in the config file
	/// </summary>
	public static string HandleRateLimitsPath { get; set; } = "Mangadex:RateLimits:Enabled";

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
	/// Whether or not to use conservative rate limits
	/// </summary>
	public bool ConservativeLimits { get; set; } = API_CONSERVATIVE_LIMITS;

	/// <summary>
	/// Whether or not to handle rate limits within the application
	/// </summary>
	public bool HandleRateLimits { get; set; } = API_HANDLE_RATE_LIMITS;

	/// <summary>
	/// Fetches the API configuration from the provided configuration
	/// </summary>
	/// <param name="config">The configuration to use</param>
	/// <returns>The API configuration</returns>
	public static IConfigurationApi FromConfiguration(IConfiguration config)
    {
        bool GetBool(string path, bool defValue)
        {
			if (config[path] is null)
				return defValue;

            return bool.TryParse(config[path]?.ToLower(), out var result)
                ? result : defValue;
		}

        return new ConfigurationApi
        {
            ApiUrl = config[ApiPath] ?? API_ROOT,
            UserAgent = config[UserAgentPath] ?? API_USER_AGENT,
            ThrowOnError = GetBool(ErrorThrownPath, API_THROW_ON_ERROR),
			ConservativeLimits = GetBool(ConservativeLimitsPath, API_CONSERVATIVE_LIMITS),
            HandleRateLimits = GetBool(HandleRateLimitsPath, API_HANDLE_RATE_LIMITS)
		};
    }

    /// <summary>
    /// Creates a new instance of the API configuration with hardcoded values
    /// </summary>
    /// <param name="apiUrl">The URL for the MangaDex API</param>
    /// <param name="userAgent">The User-Agent header to send with requests</param>
    /// <param name="throwOnError">Whether or not to throw an exception if the API returns an error</param>
    /// <param name="conservativeLimits">Whether or not to use conservative rate limits</param>
    /// <param name="handleRateLimits">Whether or not to handle rate limits within the application</param>
    /// <returns>The API configuration</returns>
    public static IConfigurationApi FromHardCoded(
        string? apiUrl = null, string? userAgent = null, 
        bool? throwOnError = null, bool? handleRateLimits = null,
        bool? conservativeLimits = null)
    {
        return new ConfigurationApi
        {
            ApiUrl = apiUrl ?? API_ROOT,
            UserAgent = userAgent ?? API_USER_AGENT,
            ThrowOnError = throwOnError ?? API_THROW_ON_ERROR,
            ConservativeLimits = conservativeLimits ?? API_CONSERVATIVE_LIMITS,
			HandleRateLimits = handleRateLimits ?? API_HANDLE_RATE_LIMITS
		};
    }
}
