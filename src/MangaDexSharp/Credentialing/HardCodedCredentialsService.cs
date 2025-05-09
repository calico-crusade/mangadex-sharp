namespace MangaDexSharp;

/// <summary>
/// Represents a provider that stores the credentials in-memory
/// </summary>
/// <param name="token">The user's authentication token</param>
public class HardCodedCredentialsService(string? token = null) : ICredentialsService
{
    /// <summary>
    /// The user's authentication token
    /// </summary>
    public string? Token { get; set; } = token;

    /// <summary>
    /// Returns the user's API token from in memory
    /// </summary>
    /// <returns>The user's authentication token</returns>
    public Task<string?> GetToken()
    {
        return Task.FromResult(Token);
    }
}
