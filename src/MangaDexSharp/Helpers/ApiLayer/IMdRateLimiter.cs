namespace MangaDexSharp;

/// <summary>
/// An instance of a rate limiter for the MD API.
/// </summary>
public interface IMdRateLimiter
{
	/// <summary>
	/// Limits the rate of requests for the MD API.
	/// </summary>
	/// <param name="request">The HTTP request builder</param>
	/// <param name="token">The cancellation token</param>
	/// <returns>The disposable for the rate limiters</returns>
	Task<IDisposable> Limit(MdHttpBuilder request, CancellationToken token);

	/// <summary>
	/// Observes the rate limits
	/// </summary>
	/// <param name="url">The URL of the request</param>
	/// <param name="limits">The rate limits</param>
	void Observe(string url, RateLimit limits);
}
