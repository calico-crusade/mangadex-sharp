namespace MangaDexSharp;

/// <summary>
/// Represents the request to login to MD using the old username/password combination
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// The username of the account
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The email address of the account
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The password of the account
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
