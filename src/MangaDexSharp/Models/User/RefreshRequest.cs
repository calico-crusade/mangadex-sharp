namespace MangaDexSharp;

/// <summary>
/// Represents a request to refresh a token
/// </summary>
public class RefreshRequest
{
    /// <summary>
    /// The refresh token
    /// </summary>
    [JsonPropertyName("token")]
    public string Refresh { get; set; } = string.Empty;
}