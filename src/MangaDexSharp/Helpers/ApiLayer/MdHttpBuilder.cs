using System.Net.Http;

namespace MangaDexSharp;

/// <summary>
/// A customized instance of <see cref="HttpBuilder"/> that handles rate limits
/// </summary>
/// <param name="_factory">The factory for creating <see cref="HttpClient"/>s</param>
/// <param name="_json">The service for parsing JSON responses</param>
/// <param name="_limiter">The optional rate limiter for controlling request rates</param>
public class MdHttpBuilder(
	IHttpClientFactory _factory,
	IMdJsonService _json,
	IMdRateLimiter _limiter) : HttpBuilder(_factory, _json)
{
	/// <summary>
	/// The full URI of the request if set
	/// </summary>
	public Uri? RequestUri { get; private set; }

	/// <summary>
	/// The HTTP method of the request if set
	/// </summary>
	public string? RequestMethod { get; private set; }

	/// <summary>
	/// Sets the request URI for the HTTP request.
	/// </summary>
	/// <param name="uri">The URI string.</param>
	/// <returns>The current <see cref="MdHttpBuilder"/> instance for method chaining.</returns>
	public MdHttpBuilder Uri(string uri)
	{
		if (RequestUri is not null)
			throw new InvalidOperationException("Request URI has already been set and cannot be modified.");

		RequestUri = new Uri(uri);
		Message(c =>
		{
			c.RequestUri = RequestUri;
		});
		return this;
	}

	/// <summary>
	/// Sets the request method
	/// </summary>
	/// <param name="method">The HTTP method to use for the request.</param>
	/// <returns>The current <see cref="MdHttpBuilder"/> instance for method chaining.</returns>
	public MdHttpBuilder Method(string method)
	{
		if (!string.IsNullOrWhiteSpace(RequestMethod))
			throw new InvalidOperationException("Request method has already been set and cannot be modified.");

		RequestMethod = method.ToUpper().Trim();
		Message(c =>
		{
			c.Method = new HttpMethod(RequestMethod);
		});
		return this;
	}

	/// <inheritdoc />
	public override async Task<HttpResponseMessage> MakeRequest(HttpClient client, bool ensureAccept, CancellationToken token)
	{
		using var limit = await _limiter.Limit(this, token);
		return await base.MakeRequest(client, ensureAccept, token);
	}
}
