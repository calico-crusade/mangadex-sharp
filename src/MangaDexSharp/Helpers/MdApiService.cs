using CardboardBox.Json;
using Microsoft.Extensions.Logging;
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
    Task<Action<HttpRequestMessage>> Auth(string? token, bool optional = false);
}

/// <summary>
/// The default implementation of the <see cref="IApiService"/>
/// </summary>
public class MdApiService : ApiService, IMdApiService
{
    private readonly ICredentialsService _creds;

    /// <summary>
    /// The DI constructor
    /// </summary>
    /// <param name="httpFactory"></param>
    /// <param name="json"></param>
    /// <param name="cache"></param>
    /// <param name="logger"></param>
    /// <param name="creds"></param>
    public MdApiService(
        IHttpClientFactory httpFactory, 
        IJsonService json, 
        ICacheService cache, 
        ILogger<ApiService> logger,
        ICredentialsService creds) : base(httpFactory, json, cache, logger)
    {
        _creds = creds;
    }

    /// <summary>
    /// Provides a method of resolving the user's authentication token from varying sources
    /// </summary>
    /// <param name="token">The contextual token</param>
    /// <param name="optional">Whether or not the token is optional for this request</param>
    /// <returns>The current request with the attached authentication token</returns>
    /// <exception cref="ArgumentException">Thrown if the authentication token is required but is missing</exception>
    public async Task<Action<HttpRequestMessage>> Auth(string? token, bool optional = false)
    {
        token ??= await _creds.GetToken();
        if (string.IsNullOrEmpty(token) && optional) return c => { };

        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("No token provided by credentials service", nameof(token));

        return c => c.Headers.Add("Authorization", "Bearer " + token);
    }

    /// <summary>
    /// Adds the <see cref="ICredentialsService.ApiUrl"/> to the begining of the URL if it is not already present
    /// </summary>
    /// <param name="url">The URL to wrap</param>
    /// <returns>The wrapped URL</returns>
    public string WrapUrl(string url)
    {
        if (url.ToLower().StartsWith("http")) return url;

        return $"{_creds.ApiUrl.TrimEnd('/')}/{url.TrimStart('/')}";
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
        var res = base
            .Create(WrapUrl(url), json, method)
            .With(t =>
            {
                t.Headers.Add("User-Agent", _creds.UserAgent);
            });

        if (_creds.ThrowOnError)
            res.FailWithThrow();
        else
            res.FailGracefully();

        return res;
    }
}
