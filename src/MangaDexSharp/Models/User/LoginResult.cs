namespace MangaDexSharp;

/// <summary>
/// Represents the result of a login request
/// </summary>
public class LoginResult : MangaDexRateLimits
{
    /// <summary>
    /// Whether or not the result was successful
    /// </summary>
    [JsonPropertyName("result")]
    public string Result { get; set; } = string.Empty;

    /// <summary>
    /// The error message if the request failed
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The token data for the login session
    /// </summary>
    [JsonPropertyName("token")]
    public Token Data { get; set; } = new();

    /// <summary>
    /// Represents an authentication token for a login request
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The authentication token for the session
        /// </summary>
        [JsonPropertyName("session")]
        public string Session { get; set; } = string.Empty;

        /// <summary>
        /// The refresh token for the session
        /// </summary>
        [JsonPropertyName("refresh")]
        public string Refresh { get; set; } = string.Empty;
    }
}
