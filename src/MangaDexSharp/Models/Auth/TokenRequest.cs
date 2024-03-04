namespace MangaDexSharp;

/// <summary>
/// Authorization token request parameters
/// </summary>
public class TokenRequest
{
    /// <summary>
    /// The text for the "password" OAuth 2.0 grant type
    /// </summary>
    public const string GRANT_TYPE_PASSWORD = "password";

    /// <summary>
    /// The text for the "refresh_token" OAuth 2.0 grant type
    /// </summary>
    public const string GRANT_TYPE_REFRESH = "refresh_token";

    /// <summary>
    /// The grant type for the request
    /// </summary>
    public string GrantType { get; set; } = GRANT_TYPE_PASSWORD;

    /// <summary>
    /// The client ID for the request
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// The client secret for the request
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// The refresh token for the request
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// The username for the request
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The password for the request
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The code for the request
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Creates a token request for the "password" OAuth 2.0 grant type
    /// </summary>
    /// <param name="clientId">The client ID for the request</param>
    /// <param name="clientSecret">The client secret for the request</param>
    /// <param name="username">The username for the request</param>
    /// <param name="password">The password for the request</param>
    /// <returns>The token request for the "password" OAuth 2.0 grant type</returns>
    public static TokenRequest PasswordFlow(string clientId, string clientSecret, string username, string password)
    {
        return new TokenRequest
        {
            GrantType = GRANT_TYPE_PASSWORD,
            ClientId = clientId,
            ClientSecret = clientSecret,
            Username = username,
            Password = password
        };
    }

    /// <summary>
    /// Creates a token request for the "refresh_token" OAuth 2.0 grant type
    /// </summary>
    /// <param name="clientId">The client ID for the request</param>
    /// <param name="clientSecret">The client secret for the request</param>
    /// <param name="refreshToken">The refresh token for the request</param>
    /// <returns>The token request for the "refresh_token" OAuth 2.0 grant type</returns>
    public static TokenRequest RefreshFlow(string clientId, string clientSecret, string refreshToken)
    {
        return new TokenRequest
        {
            GrantType = GRANT_TYPE_REFRESH,
            ClientId = clientId,
            ClientSecret = clientSecret,
            RefreshToken = refreshToken
        };
    }
}
