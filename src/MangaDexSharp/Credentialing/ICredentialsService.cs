namespace MangaDexSharp;

/// <summary>
/// A service that provides a method for fetching the API token and URL data
/// </summary>
public interface ICredentialsService
{
    /// <summary>
    /// How to fetch the user's authentication token
    /// </summary>
    /// <returns>The user's authentication token</returns>
    Task<string?> GetToken();
}