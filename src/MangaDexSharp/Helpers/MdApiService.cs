using CardboardBox.Json;
using System.Net.Http;

namespace MangaDexSharp;

/// <summary>
/// An implementation of the <see cref="IApiService"/> that is specific to the MangaDex API
/// </summary>
public interface IMdApiService : IApiService
{
    /// <summary>
    /// Provides a method of resolving the user's authentication token from varying sources
    /// </summary>
    /// <param name="token">The contextual token</param>
    /// <param name="optional">Whether or not the token is optional for this request</param>
    /// <returns>The current request with the attached authentication token</returns>
    /// <exception cref="ArgumentException">Thrown if the authentication token is required but is missing</exception>
    Task<Action<IHttpBuilderConfig>> Auth(string? token, bool optional = false);
}

/// <summary>
/// The default implementation of the <see cref="IApiService"/>
/// </summary>
/// <remarks>
/// The DI constructor
/// </remarks>
/// <param name="_factory">The factory for creating <see cref="HttpClient"/>s</param>
/// <param name="_json">The service for parsing JSON responses</param>
/// <param name="_api">The API configuration</param>
/// <param name="_creds">The credentials to use for the application</param>
/// <param name="_events">The service for handling events</param>
/// <param name="_config">The optional configuration method for changing how the underlying HTTP requests are made</param>
public class MdApiService(
    IHttpClientFactory _factory,
    IMdJsonService _json,
    IConfigurationApi _api,
    ICredentialsService _creds,
    IMdEventsService _events,
    IMdRequestConfigurationService? _config = null) : ApiService(_factory, _json), IMdApiService
{
    /// <summary>
    /// Provides a method of resolving the user's authentication token from varying sources
    /// </summary>
    /// <param name="token">The contextual token</param>
    /// <param name="optional">Whether or not the token is optional for this request</param>
    /// <returns>The current request with the attached authentication token</returns>
    /// <exception cref="ArgumentException">Thrown if the authentication token is required but is missing</exception>
    public async Task<Action<IHttpBuilderConfig>> Auth(string? token, bool optional = false)
    {
        token ??= await _creds.GetToken();
        if (string.IsNullOrEmpty(token) && optional) return c => { };

        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("No token provided by credentials service", nameof(token));

        return c => c.Message(t => t.Headers.Add("Authorization", "Bearer " + token));
    }

    /// <summary>
    /// Adds the <see cref="IConfigurationApi.ApiUrl"/> to the beginning of the URL if it is not already present
    /// </summary>
    /// <param name="url">The URL to wrap</param>
    /// <returns>The wrapped URL</returns>
    public string WrapUrl(string url)
    {
        if (url.ToLower().StartsWith("http")) return url;

        return $"{_api.ApiUrl.TrimEnd('/')}/{url.TrimStart('/')}";
    }

    /// <summary>
    /// Populate the <see cref="MangaDexRateLimits"/> models if possible
    /// </summary>
    /// <param name="data">The data being processed</param>
    /// <param name="resp">The response message from MangaDex</param>
    public void FillRateLimits(HttpResponseMessage resp, object? data)
    {
        if (data is null || data is not MangaDexRateLimits limits) return;

        var rateLimits = new RateLimit();

        if (resp.Headers.TryGetValues("X-RateLimit-Limit", out var strLimit) &&
            int.TryParse(strLimit.FirstOrDefault(), out var limit))
            rateLimits.Limit = limit;

        if (resp.Headers.TryGetValues("X-RateLimit-Remaining", out var strRemaining) &&
            int.TryParse(strRemaining.FirstOrDefault(), out var remaining))
            rateLimits.Remaining = remaining;

        if (resp.Headers.TryGetValues("X-RateLimit-Retry-After", out var strRetry) &&
            double.TryParse(strRetry.FirstOrDefault(), out var retry))
            rateLimits.RetryAfter = DateTime.UnixEpoch.AddSeconds(retry);

        limits.RateLimit = rateLimits;
    }

    /// <summary>
    /// Creates an <see cref="IHttpBuilder"/> for the given URL and method
    /// </summary>
    /// <param name="url">The url to request data from</param>
    /// <param name="json">The JSON provider to use for this request</param>
    /// <param name="method">The method used for this HTTP request</param>
    /// <returns>The instance of the <see cref="IHttpBuilder"/></returns>
    public override IHttpBuilder Create(string url, IJsonService json, string method = "GET")
    {
        var builder = new HttpBuilder(_factory, _json);

        var uri = WrapUrl(url);
        _config?.Configure(uri, builder);
        builder
            .Method(method)
            .Uri(uri)
            .UserAgent(_api.UserAgent)
            .OnResponseParsed(FillRateLimits);

        _events.Bind(uri, builder);

        if (_api.ThrowOnError)
            builder.FailWithThrow();
        else
            builder.FailGracefully();

        return builder;
    }
}
