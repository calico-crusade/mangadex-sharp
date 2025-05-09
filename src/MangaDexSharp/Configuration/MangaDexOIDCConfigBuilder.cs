namespace MangaDexSharp;

/// <summary>
/// The builder for the <see cref="IConfigurationOIDC"/> service
/// </summary>
public class MangaDexOIDCConfigBuilder
{
    /// <summary>
    /// The Auth URL service for MangaDex
    /// </summary>
    public string? AuthUrl { get; set; }

    /// <summary>
    /// The portion of the URL that indicates the realm to use
    /// </summary>
    public string? RealmPath { get; set; }

    /// <summary>
    /// The client ID for the authorization endpoint
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// The client secret for the authorization endpoint
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// The username for password grant requests
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// The password for password grant requests
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Sets the url for the authorization endpoint
    /// </summary>
    /// <param name="url">The URL to use</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexOIDCConfigBuilder WithAuthUrl(string? url)
    {
        AuthUrl = url;
        return this;
    }

    /// <summary>
    /// Sets the url for the authorization endpoint to the dev URL
    /// </summary>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexOIDCConfigBuilder WithDevAuthUrl()
    {
        AuthUrl = ConfigurationOIDC.AUTH_URL_DEV;
        return this;
    }

    /// <summary>
    /// Sets the portion of the URL that indicates the realm to use
    /// </summary>
    /// <param name="path">The portion of the URL that indicates the realm to use</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexOIDCConfigBuilder WithRealmPath(string? path)
    {
        RealmPath = path;
        return this;
    }

    /// <summary>
    /// Sets the client ID for the authorization endpoint
    /// </summary>
    /// <param name="id">The client ID for the authorization endpoint</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexOIDCConfigBuilder WithClientId(string? id)
    {
        ClientId = id;
        return this;
    }

    /// <summary>
    /// Sets the client secret for the authorization endpoint
    /// </summary>
    /// <param name="secret">The client secret for the authorization endpoint</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexOIDCConfigBuilder WithClientSecret(string? secret)
    {
        ClientSecret = secret;
        return this;
    }

    /// <summary>
    /// Sets the username for password grant requests
    /// </summary>
    /// <param name="username">The username for password grant requests</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexOIDCConfigBuilder WithUsername(string? username)
    {
        Username = username;
        return this;
    }

    /// <summary>
    /// Sets the password for password grant requests
    /// </summary>
    /// <param name="password">The password for password grant requests</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexOIDCConfigBuilder WithPassword(string? password)
    {
        Password = password;
        return this;
    }

    internal IConfigurationOIDC Build()
    {
        return ConfigurationOIDC.FromHardCoded(ClientId, ClientSecret, Username, Password, AuthUrl, RealmPath);
    }
}
