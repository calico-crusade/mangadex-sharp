namespace MangaDexSharp;

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
    /// The Auth URL service for MangaDex
    /// </summary>
    public string AuthUrl { get; set; }

    /// <summary>
    /// The client ID for the authorization endpoint
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// The client secret for the authorization endpoint
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// The User-Agent header to send with requests
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// Whether or not to throw an exception if the API returns an error
    /// </summary>
    public bool ThrowOnError { get; set; }

    /// <summary>
    /// The username for the password grant OAuth2 requests
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The password for the password grant OAuth2 requests
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Represents a provider that stores the credentials in-memory
    /// </summary>
    /// <param name="token">The user's authentication token</param>
    /// <param name="apiUrl">The API url</param>
    /// <param name="userAgent">The User-Agent header to send with requests</param>
	/// <param name="throwOnError">Whether or not to throw an exception if the API returns an error</param>
	/// <param name="authUrl">The Auth URL service for MangaDex</param>
	/// <param name="clientId">The client ID for the authorization endpoint</param>
	/// <param name="clientSecret">The client secret for the authorization endpoint</param>
	/// <param name="username">The username for the password grant OAuth2 requests</param>
	/// <param name="password">The password for the password grant OAuth2 requests</param>
    public HardCodedCredentialsService(
        string? token = null,
        string? apiUrl = null,
        string? userAgent = null,
        bool throwOnError = false,
        string? authUrl = null,
        string? clientId = null,
        string? clientSecret = null,
        string? username = null,
        string? password = null)
    {
        UserAgent = userAgent ?? API_USER_AGENT;
        Token = token;
        ApiUrl = apiUrl ?? API_ROOT;
        AuthUrl = authUrl ?? AUTH_URL;
        ClientId = clientId;
        ClientSecret = clientSecret;
        Username = username;
        Password = password;
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
