namespace MangaDexSharp;

/// <summary>
/// The builder for the <see cref="IConfigurationApi"/> service
/// </summary>
public class MangaDexApiConfigBuilder
{
    /// <summary>
    /// The URL for the MangaDex API
    /// </summary>
    public string? ApiUrl { get; set; }

    /// <summary>
    /// The User-Agent header to send with requests
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Whether or not to throw an exception if the API returns an error
    /// </summary>
    public bool? ThrowOnError { get; set; }

    /// <summary>
    /// Uses the given API url
    /// </summary>
    /// <param name="url">The URL to use</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexApiConfigBuilder WithApiUrl(string? url)
    {
        ApiUrl = url;
        return this;
    }

    /// <summary>
    /// Uses the developer API
    /// </summary>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexApiConfigBuilder WithDevApi()
    {
        return WithApiUrl(ConfigurationApi.API_ROOT_DEV);
    }

    /// <summary>
    /// Uses the given user-agent
    /// </summary>
    /// <param name="userAgent">The user-agent to use</param>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexApiConfigBuilder WithUserAgent(string? userAgent)
    {
        UserAgent = userAgent;
        return this;
    }

    /// <summary>
    /// Throws an exception if the API returns an error
    /// </summary>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexApiConfigBuilder ThrowExceptionOnError()
    {
        ThrowOnError = true;
        return this;
    }

    /// <summary>
    /// Doesn't throw an exception if the API returns an error
    /// </summary>
    /// <returns>The current builder for method chaining</returns>
    public MangaDexApiConfigBuilder FailGracefully()
    {
        ThrowOnError = false;
        return this;
    }

    internal IConfigurationApi Build()
    {
        return ConfigurationApi.FromHardCoded(ApiUrl, UserAgent, ThrowOnError);
    }
}
