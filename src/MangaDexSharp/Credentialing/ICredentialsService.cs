namespace MangaDexSharp;

/// <summary>
/// A service that provides a method for fetching the API token and URL data
/// </summary>
public interface ICredentialsService
{
    /// <summary>
    /// The URL for the MangaDex API
    /// </summary>
    string ApiUrl { get; }

	/// <summary>
	/// The Auth URL service for MangaDex
	/// </summary>
	string AuthUrl { get; }

    /// <summary>
    /// The User-Agent header to send with requests
    /// </summary>
    string UserAgent { get; }

	/// <summary>
	/// The client ID for the authorization endpoint
	/// </summary>
	string? ClientId { get; }

	/// <summary>
	/// The client secret for the authorization endpoint
	/// </summary>
	string? ClientSecret { get; }

	/// <summary>
	/// The username for password grant requests
	/// </summary>
	string? Username { get; }

	/// <summary>
	/// The password for password grant requests
	/// </summary>
	string? Password { get; }

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