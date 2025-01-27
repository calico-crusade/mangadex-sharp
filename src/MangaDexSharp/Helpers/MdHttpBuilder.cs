using CardboardBox.Json;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;

namespace MangaDexSharp;

/// <summary>
/// An implementation of the <see cref="IHttpBuilder"/> that is specific to the MangaDex API
/// </summary>
public interface IMdHttpBuilder : IHttpBuilder
{

}

/// <summary>
/// This is copied from https://github.com/cardboards-box/http/blob/3d91c35600be16c8ada13bc65b6cf95b7ecac5ab/src/CardboardBox.Http/HttpBuilder.cs
/// </summary>
internal class MdHttpBuilder : IMdHttpBuilder
{
    /// <summary>
    /// The verb for "GET"ting something
    /// </summary>
    public const string GET_METHOD = "GET";
    /// <summary>
    /// The default accept header for JSON
    /// </summary>
    public const string APP_JSON = "application/json";
    /// <summary>
    /// The default cache folder
    /// </summary>
    public const string CACHE_DIR = "Cache";
    /// <summary>
    /// The default cache minutes
    /// </summary>
    public const int CACHE_MIN = 5;

    private readonly IHttpClientFactory _factory;
    private readonly IJsonService _json;
    private readonly ICacheService _cacheService;
    private readonly ILogger _logger;

    private readonly List<Action<HttpRequestMessage>> _config;

    private string _method = GET_METHOD;
    private string? _uri;
    private string _accept = APP_JSON;

    private bool _cache = false;
    private string _cacheFolder = CACHE_DIR;
    private double _cacheMinutes = CACHE_MIN;
    private bool _failWithNull = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="json"></param>
    /// <param name="cacheService"></param>
    /// <param name="logger"></param>
    public MdHttpBuilder(
        IHttpClientFactory factory,
        IJsonService json,
        ICacheService cacheService,
        ILogger logger)
    {
        _factory = factory;
        _json = json;
        _cacheService = cacheService;
        _config = new List<Action<HttpRequestMessage>>();
        _logger = logger;
    }

    /// <summary>
    /// Sets the Method for the request
    /// </summary>
    /// <param name="method">The method to use</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder Method(string method)
    {
        _method = method.ToUpper().Trim();
        return this;
    }

    /// <summary>
    /// Sets the URI for the request
    /// </summary>
    /// <param name="uri">The URL to use</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder Uri(string uri)
    {
        _uri = uri;
        return this;
    }

    /// <summary>
    /// Instead of throwing an exception, it will log the error and return a default value
    /// </summary>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder FailGracefully()
    {
        _failWithNull = true;
        return this;
    }

    /// <summary>
    /// Throw an exception if the returned status code isn't in the 200 range.
    /// </summary>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder FailWithThrow()
    {
        _failWithNull = false;
        return this;
    }

    /// <summary>
    /// Provides a catch-all configuration object for the <see cref="HttpRequestMessage"/> for anything not handled by default
    /// </summary>
    /// <param name="config">The configuration value</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder With(Action<HttpRequestMessage>? config)
    {
        if (config != null)
            _config.Add(config);
        return this;
    }

    /// <summary>
    /// Sets the Accept header to the given value
    /// </summary>
    /// <param name="accept">The accept header's value</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder Accept(string accept)
    {
        _accept = accept;
        return this;
    }

    /// <summary>
    /// Sets the authorization header for the request to the given token and scheme
    /// </summary>
    /// <param name="token">The token to use</param>
    /// <param name="scheme">The scheme to use (defaults to "Bearer")</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder Authorization(string token, string scheme = "Bearer")
    {
        return With(c => c.Headers.Add("Authorization", $"{scheme} {token}"));
    }

    /// <summary>
    /// Sets the body content of the request
    /// </summary>
    /// <param name="content">The body content</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder BodyContent(HttpContent content)
    {
        return With(msg =>
        {
            msg.Content = content;
        });
    }

    /// <summary>
    /// Sets the body content to the given JSON serialized object
    /// </summary>
    /// <typeparam name="T">The type of JSON object to send</typeparam>
    /// <param name="data">The data to serialize</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder Body<T>(T data)
    {
        var str = _json.Serialize(data);
        var json = new StringContent(str, Encoding.UTF8, APP_JSON);

        return BodyContent(json);
    }

    /// <summary>
    /// Sets the body content to a <see cref="FormUrlEncodedContent"/> built from the given parameters
    /// </summary>
    /// <param name="data">The parameters to be encoded</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder Body(params (string, string)[] data)
    {
        var kvp = data.Select(t => new KeyValuePair<string, string>(t.Item1, t.Item2));
        var content = new FormUrlEncodedContent(kvp);
        return BodyContent(content);
    }

    /// <summary>
    /// Adds the caching layer to the HTTP request. Caching is automatically disabled when used in conjunction with <see cref="HttpStatusResult{TSuccess, TFailure}"/>
    /// </summary>
    /// <param name="enable">Whether or not to enable caching</param>
    /// <param name="folder">The optional cache folder to use (defaults to "Cache")</param>
    /// <param name="minutes">The optional cache minutes to use (defaults to 5 minutes)</param>
    /// <returns>The instance of <see cref="IHttpBuilder"/> for chaining</returns>
    public IHttpBuilder Cache(bool enable = true, string? folder = null, double? minutes = null)
    {
        _cache = enable;
        _cacheFolder = folder ?? CACHE_DIR;
        _cacheMinutes = minutes ?? CACHE_MIN;
        return this;
    }

    /// <summary>
    /// Executes the HTTP request and returns the results
    /// </summary>
    /// <returns>The <see cref="HttpResponseMessage"/> results</returns>
    /// <exception cref="ArgumentNullException">Thrown if the URI is not set for the request</exception>
    public async Task<HttpResponseMessage?> Result()
    {
        try
        {
            if (string.IsNullOrEmpty(_uri))
                throw new ArgumentNullException(nameof(_uri));

            using var client = _factory.CreateClient();
            return await MakeRequest(client);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while running {_uri}");
            if (_failWithNull) return null;

            throw;
        }
    }

    /// <summary>
    /// Executes the HTTP request and returns the results as the given type
    /// </summary>
    /// <typeparam name="T">The type to deserialize the results to</typeparam>
    /// <returns>A task representing the results of the request</returns>
    /// <exception cref="ArgumentNullException">Thrown if the URI is not set for the request</exception>
    public async Task<T?> Result<T>()
    {
        try
        {
            if (string.IsNullOrEmpty(_uri))
                throw new ArgumentNullException(nameof(_uri));

            var valid = _cacheService.Validate(_uri, out string? filename, _cacheFolder, _cacheMinutes);
            if (valid && _cache && _method == GET_METHOD && !string.IsNullOrEmpty(filename)) return await _cacheService.Load<T>(filename);

            using var client = _factory.CreateClient();
            var resp = await MakeRequest(client);
            var data = await Json<T>(resp);

            if (!string.IsNullOrEmpty(filename) && _cache && _method == GET_METHOD)
                await _cacheService.Save(data, filename);

            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while running {_uri}");
            if (_failWithNull) return default;

            throw;
        }
    }

    /// <summary>
    /// Executes the HTTP request and returns the results as the given types
    /// </summary>
    /// <typeparam name="TSuccess">The type to use for a successful request</typeparam>
    /// <typeparam name="TFailure">The type to use for a failed request</typeparam>
    /// <returns>A task representing the <see cref="HttpStatusResult{TSuccess, TFailure}"/> which contains the results of the request </returns>
    public async Task<HttpStatusResult<TSuccess, TFailure>> Result<TSuccess, TFailure>()
    {
        using var client = _factory.CreateClient();
        var resp = await MakeRequest(client);
        return await Json<TSuccess, TFailure>(resp);
    }

    /// <summary>
    /// Executes the given request and returns the <see cref="HttpRequestMessage"/>
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> to use to make the request</param>
    /// <returns>A task representing the results for the request</returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if the <see cref="_uri"/> isn't set correctly.</exception>
    public Task<HttpResponseMessage> MakeRequest(HttpClient client)
    {
        if (string.IsNullOrEmpty(_uri))
            throw new ArgumentNullException(nameof(_uri));

        var method = new HttpMethod(_method);
        var request = new HttpRequestMessage(method, _uri);
        request.Headers.Add("Accept", _accept);

        foreach (var config in _config)
            config?.Invoke(request);

        return client.SendAsync(request);
    }

    /// <summary>
    /// Populate the <see cref="MangaDexRateLimits"/> models if possible
    /// </summary>
    /// <typeparam name="T">The type of data being processed</typeparam>
    /// <param name="data">The data being processed</param>
    /// <param name="resp">The response message from MangaDex</param>
    public void FillRateLimits<T>(T? data, HttpResponseMessage resp)
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
    /// Deserializes the <see cref="HttpResponseMessage"/> from Json to the given type
    /// </summary>
    /// <typeparam name="T">The type to deserialize too</typeparam>
    /// <param name="resp">The response message to deserialize</param>
    /// <returns>A task representing the returned deserialized result</returns>
    public async Task<T?> Json<T>(HttpResponseMessage resp)
    {
        if (!_failWithNull) resp.EnsureSuccessStatusCode();

        using var rs = await resp.Content.ReadAsStreamAsync();
        var output = await _json.Deserialize<T>(rs);
        FillRateLimits(output, resp);
        return output;
    }

    /// <summary>
    /// Deserializes the <see cref="HttpResponseMessage"/> from JSON to the given types
    /// </summary>
    /// <typeparam name="TSuccess">The type to use if the result was successful</typeparam>
    /// <typeparam name="TFailure">The type to use if the result was failure</typeparam>
    /// <param name="resp">The response message to deserialize</param>
    /// <returns>A task representing the return deserialized result</returns>
    public async Task<HttpStatusResult<TSuccess, TFailure>> Json<TSuccess, TFailure>(HttpResponseMessage resp)
    {
        using var rs = await resp.Content.ReadAsStreamAsync();

        if (resp.IsSuccessStatusCode)
        {
            var data = await _json.Deserialize<TSuccess>(rs);
            FillRateLimits(data, resp);
            return HttpStatusResult<TSuccess, TFailure>.FromSuccess(data, resp.StatusCode);
        }

        var error = await _json.Deserialize<TFailure>(rs);
        FillRateLimits(error, resp);
        return HttpStatusResult<TSuccess, TFailure>.FromFailure(error, resp.StatusCode);
    }
}
