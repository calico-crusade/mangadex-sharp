
namespace MangaDexSharp;

/// <summary>
/// The result of a token request
/// </summary>
public class TokenResult
{
    /// <summary>
    /// The access token for the session
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// The refresh token for the session
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// The expiration time of the access token in seconds
    /// </summary>
    [JsonPropertyName("expires_in")]
    public double? ExpiresIn { get; set; }

    /// <summary>
    /// The expiration time of the refresh token in seconds
    /// </summary>
    [JsonPropertyName("refresh_expires_in")]
    public double? RefreshExpiresIn { get; set; }

    /// <summary>
    /// The type of token returned
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    /// <summary>
    /// The not-before policy of the token in seconds
    /// </summary>
    [JsonPropertyName("not-before-policy")]
    public double? NotBeforePolicy { get; set; }

    /// <summary>
    /// The session state GUID
    /// </summary>
    [JsonPropertyName("session_state")]
    public string? SessionState { get; set; }

    /// <summary>
    /// The scopes of the token
    /// </summary>
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }

    /// <summary>
    /// The type of client
    /// </summary>
    [JsonPropertyName("client_type")]
    public string? ClientType { get; set; }

    /// <summary>
    /// The scopes of the token
    /// </summary>
    [JsonIgnore]
    public string[] Scopes => Scope?.Split(' ') ?? [];
}
