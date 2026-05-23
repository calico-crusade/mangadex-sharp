using System.Net.Http;

namespace MangaDexSharp;

/// <summary>
/// The base interface for <see cref="IMdEventService"/>
/// </summary>
public interface IMdEventServiceBase
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

	/// <summary>
	/// Triggered whenever the rate limit is exceeded and the client is paused until it resets
	/// </summary>
	/// <param name="url">The URL of the request</param>
	/// <param name="limits">The rate limit data</param>
	/// <param name="span">The duration of the timeout</param>
	void OnRateLimitGlobalPaused(string url, RateLimit limits, TimeSpan span);

	/// <summary>
	/// Triggered whenever the rate limit is unpaused and the client can resume sending requests
	/// </summary>
	/// <param name="url">The URL of the request</param>
	/// <param name="limits">The rate limit data</param>
	void OnRateLimitGlobalUnpaused(string url, RateLimit limits);
}

/// <summary>
/// Represents a service that handles events triggered within MangaDexSharp
/// </summary>
public interface IMdEventService : IMdEventServiceBase
{
    
}

/// <summary>
/// Default implementation of <see cref="IMdEventService"/> that does nothing 
/// and provides easy overrides for the events you want
/// </summary>
public abstract class MdEventService : IMdEventService
{
    /// <inheritdoc />
    public virtual void OnRequestError(string url, Exception error) { }

	/// <inheritdoc />
	public virtual void OnRequestFinished(string url, Exception? error) { }

	/// <inheritdoc />
	public virtual void OnRequestStarting(string url) { }

	/// <inheritdoc />
	public virtual void OnResponseReceived(string url, HttpResponseMessage response, HttpRequestMessage request) { }

	/// <inheritdoc />
	public virtual void OnResponseParsed(string url, HttpResponseMessage response, object? data) { }

	/// <inheritdoc />
	public virtual void OnRateLimitDataReceived(string url, RateLimit limits) { }

	/// <inheritdoc />
	public virtual void OnRateLimitExceeded(string url, RateLimit limits) { }

	/// <inheritdoc />
	public virtual void OnRateLimitGlobalPaused(string url, RateLimit limits, TimeSpan span) { }

	/// <inheritdoc />
	public virtual void OnRateLimitGlobalUnpaused(string url, RateLimit limits) { }
}