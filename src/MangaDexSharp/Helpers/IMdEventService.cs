using System.Net.Http;

namespace MangaDexSharp;

/// <summary>
/// Represents a service that handles events triggered within MangaDexSharp
/// </summary>
public interface IMdEventService
{
    /// <summary>
    /// Triggered when an underlying HTTP request finishes
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="error">An optional exception that occurred when the request finished</param>
    void OnRequestFinished(string url, Exception? error);

    /// <summary>
    /// Triggered when an underlying HTTP request finished with an error
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="error">The exception that occurred</param>
    void OnRequestError(string url, Exception error);

    /// <summary>
    /// Triggered when an underlying HTTP request starts
    /// </summary>
    /// <param name="url">The URL of the request</param>
    void OnRequestStarting(string url);

    /// <summary>
    /// Triggered when an underlying HTTP request receives a response
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="response">The response that was received</param>
    /// <param name="request">The request that was sent</param>
    void OnResponseReceived(string url, HttpResponseMessage response, HttpRequestMessage request);

    /// <summary>
    /// Triggered when an underlying HTTP response is parsed as JSON
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="response">The response that was received</param>
    /// <param name="data">The object that was parsed</param>
    void OnResponseParsed(string url, HttpResponseMessage response, object? data);

    /// <summary>
    /// Triggered whenever an underlying HTTP response contains rate-limit headers
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="limits">The rate limit data</param>
    void OnRateLimitDataReceived(string url, RateLimit limits);

    /// <summary>
    /// Triggered whenever the rate limit is exceeded
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="limits">The rate limit data</param>
    void OnRateLimitExceeded(string url, RateLimit limits);
}

/// <summary>
/// Default implementation of <see cref="IMdEventService"/> that does nothing 
/// and provides easy overrides for the events you want
/// </summary>
public abstract class MdEventService : IMdEventService
{
    /// <summary>
    /// Triggered when an underlying HTTP request finished with an error
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="error">The exception that occurred</param>
    public virtual void OnRequestError(string url, Exception error) { }

    /// <summary>
    /// Triggered when an underlying HTTP request finishes
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="error">An optional exception that occurred when the request finished</param>
    public virtual void OnRequestFinished(string url, Exception? error) { }

    /// <summary>
    /// Triggered when an underlying HTTP request starts
    /// </summary>
    /// <param name="url">The URL of the request</param>
    public virtual void OnRequestStarting(string url) { }

    /// <summary>
    /// Triggered when an underlying HTTP request receives a response
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="response">The response that was received</param>
    /// <param name="request">The request that was sent</param>
    public virtual void OnResponseReceived(string url, HttpResponseMessage response, HttpRequestMessage request) { }

    /// <summary>
    /// Triggered when an underlying HTTP response is parsed as JSON
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="response">The response that was received</param>
    /// <param name="data">The object that was parsed</param>
    public virtual void OnResponseParsed(string url, HttpResponseMessage response, object? data) { }

    /// <summary>
    /// Triggered whenever an underlying HTTP response contains rate-limit headers
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="limits">The rate limit data</param>
    public virtual void OnRateLimitDataReceived(string url, RateLimit limits) { }

    /// <summary>
    /// Triggered whenever the rate limit is exceeded
    /// </summary>
    /// <param name="url">The URL of the request</param>
    /// <param name="limits">The rate limit data</param>
    public virtual void OnRateLimitExceeded(string url, RateLimit limits) { }
}