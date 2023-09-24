namespace MangaDexSharp;

/// <summary>
/// A service that provides a method for fetching the API token and URL data
/// </summary>
public interface ICredentialsService
{
	/// <summary>
	/// The URL for the mangadex API
	/// </summary>
	string ApiUrl { get; }

    /// <summary>
    /// The User-Agent header to send with requests
    /// </summary>
    string UserAgent { get; }

    /// <summary>
    /// How to fetch the user's authentication token
    /// </summary>
    /// <returns>The user's authentication token</returns>
    Task<string?> GetToken();

	/// <summary>
	/// Whether or not to throw an exception if the API returns an error
	/// </summary>
	bool ThrowOnError { get; }
}

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
    /// The User-Agent header to send with requests
    /// </summary>
    public string UserAgent => _config[UserAgentPath] ?? API_USER_AGENT;

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

/// <summary>
/// Represents a provider that stores the credentials in-memory
/// </summary>
public class HardCodedCredentialsService : ICredentialsService
{
	/// <summary>
	/// The user's authentication token
	/// </summary>
	public string? Token { get; set; }

	/// <summary>
	/// The API url
	/// </summary>
	public string ApiUrl { get; set; }

	/// <summary>
	/// The User-Agent header to send with requests
	/// </summary>
	public string UserAgent { get; set; }

    /// <summary>
    /// Whether or not to throw an exception if the API returns an error
    /// </summary>
    public bool ThrowOnError { get; set; }

    /// <summary>
    /// Represents a provider that stores the credentials in-memory
    /// </summary>
    /// <param name="token">The user's authentication token</param>
    /// <param name="apiUrl">The API url</param>
    /// <param name="userAgent">The User-Agent header to send with requests</param>
	/// <param name="throwOnError">Whether or not to throw an exception if the API returns an error</param>
    public HardCodedCredentialsService(string? token = null, string? apiUrl = null, string? userAgent = null, bool throwOnError = false)
	{
		UserAgent = userAgent ?? API_USER_AGENT;
		Token = token;
		ApiUrl = apiUrl ?? API_ROOT;
		ThrowOnError = throwOnError;
	}

	/// <summary>
	/// Returns the user's API token from in memory
	/// </summary>
	/// <returns>The user's authentication token</returns>
	public Task<string?> GetToken()
	{
		return Task.FromResult(Token);
	}
}
