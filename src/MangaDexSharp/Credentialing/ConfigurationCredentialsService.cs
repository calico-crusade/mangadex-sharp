namespace MangaDexSharp;

/// <summary>
/// Represents a provider that fetches the <see cref="ICredentialsService"/> from the configuration
/// </summary>
/// <param name="_config">The <see cref="IConfiguration"/> object to fetch the variables from</param>
public class ConfigurationCredentialsService(IConfiguration _config) : ICredentialsService
{
    /// <summary>
    /// Where to fetch the API token from in the config file
    /// </summary>
    public static string TokenPath { get; set; } = "Mangadex:Token";

    /// <summary>
    /// The authentication token from the configuration file
    /// </summary>
    public string? Token => _config[TokenPath];

    /// <summary>
    /// Fetches the user's authentication token from the config file
    /// </summary>
    /// <returns>The user's authentication token</returns>
    public virtual Task<string?> GetToken()
    {
        return Task.FromResult(Token);
    }
}
