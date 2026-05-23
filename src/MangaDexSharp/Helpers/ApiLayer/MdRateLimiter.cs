using System.IO.Enumeration;
using System.Threading.RateLimiting;

namespace MangaDexSharp;

using Limit = (Func<MdHttpBuilder, bool> Matches, RateLimiter Limiter);

/// <summary>
/// The default rate limiter that 
/// </summary>
public class MdRateLimiter(
	IConfigurationApi _config,
    IMdEventsService _events) : IMdRateLimiter, IDisposable
{
    private const StringComparison COMPARE = StringComparison.InvariantCultureIgnoreCase;
    private const int GLOBAL_LIMIT = 5;
    private Limit[]? _customLimits;
    private RateLimiter? _globalLimits;
    private readonly TooManyRequestsPause _globalPause = new();
    private readonly SemaphoreSlim _lock = new(1, 1);

	/// <summary>
	/// Gets the custom limits for end points
	/// </summary>
	/// <returns>The rate limits</returns>
	/// <remarks>
	/// These are asserted in order of declaration, so more specific limits should be declared first.
	/// This should not be called directly. Use the <see cref="LazyLoadLimits(CancellationToken)"/> instead.
	/// </remarks>
	public virtual Limit[] GenerateCustomLimits()
    {
        var uploadBegin = GenerateLimiter(20, TimeSpan.FromMinutes(1));
        var uploadFiles = GenerateLimiter(250, TimeSpan.FromMinutes(1));

		//Limits can be found here: https://api.mangadex.org/docs/2-limitations/#endpoint-specific-rate-limits
		return
		[
            Limiter("get", "at-home/server/*", 40, 1),
            Limiter("post", "auth/login", 30, 60),
            Limiter("post", "auth/refresh", 60, 60),
            Limiter("post", "author", 10, 60),
            Limiter("put", "author", 10, 1),
            Limiter("delete", "author/*", 10, 10),
            Limiter("post", "captcha/solve", 10, 10),
            Limiter("post", "cover", 100, 10),
            Limiter("put", "cover", 100, 10),
            Limiter("delete", "cover/*", 100, 10),
            Limiter("post", "chapter/*/read", 300, 10),
            Limiter("put", "chapter/*", 10, 1),
            Limiter("delete", "chapter/*", 10, 1),
            Limiter("post", "forums/threads", 10, 1),
            Limiter("post", "manga", 10, 60),
            Limiter("put", "manga/*", 10, 60),
            Limiter("delete", "manga/*", 10, 10),
            Limiter("post", "manga/draft/*/commit", 10, 60),
            Limiter("get", "manga/random", 60, 1),
            Limiter("post", "report", 10, 1),
            Limiter("get", "report", 10, 1),
            Limiter("post", "group", 10, 60),
            Limiter("put", "group/*", 10, 1),
            Limiter("delete", "group/*", 10, 10),
            Limiter("get", "upload", 30, 1),
            Limiter("post", "upload/begin", uploadBegin),
            Limiter("post", "upload/begin/*", uploadBegin),
            Limiter("post", "upload/*/commit", 10, 1),
            Limiter("delete", "uploads/*", 30, 1),
            Limiter("post", "upload/*", uploadFiles),
			Limiter("delete", "upload/*/batch", uploadFiles),
			Limiter("delete", "upload/*/*", uploadFiles),
		];
    }

    /// <summary>
    /// Gets the global rate limit
    /// </summary>
    /// <returns>The rate limit</returns>
    /// <remarks>This should not be called directly. Use the <see cref="LazyLoadLimits(CancellationToken)"/> instead.</remarks>
    public virtual RateLimiter GenerateGlobalLimit()
    {
        var limit = _config.ConservativeLimits ? GLOBAL_LIMIT - 1 : GLOBAL_LIMIT;
		return new TokenBucketRateLimiter(new()
        {
			TokenLimit = limit,
			TokensPerPeriod = limit,
			QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
			QueueLimit = int.MaxValue,
			ReplenishmentPeriod = TimeSpan.FromSeconds(1),
			AutoReplenishment = true
		});
    }

    /// <summary>
    /// Lazily generates the limits for the rate limiters.
    /// </summary>
    /// <param name="token">The cancellation token</param>
    /// <returns>The custom limits and the global rate limiter</returns>
    public async Task<(Limit[], RateLimiter)> LazyLoadLimits(CancellationToken token)
	{
		if (_customLimits is not null && _globalLimits is not null)
			return (_customLimits, _globalLimits);

		await _lock.WaitAsync(token);

		try
		{
			if (_customLimits is not null && _globalLimits is not null)
				return (_customLimits, _globalLimits);

			_customLimits = GenerateCustomLimits();
			_globalLimits = GenerateGlobalLimit();
			return (_customLimits, _globalLimits);
		}
		finally
		{
			_lock.Release();
		}
	}

	/// <summary>
	/// Generates a rate limiter in accordance with the settings
	/// </summary>
	/// <param name="requests">The number of requests per period</param>
	/// <param name="period">The time period</param>
	/// <returns>A rate limiter</returns>
	public virtual RateLimiter GenerateLimiter(int requests, TimeSpan period)
    {
		if (_config.ConservativeLimits && requests > 1)
			requests--;
		return new TokenBucketRateLimiter(new()
		{
			TokenLimit = requests,
			TokensPerPeriod = requests,
			QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
			QueueLimit = int.MaxValue,
			ReplenishmentPeriod = period,
			AutoReplenishment = true
		});
	}

	/// <summary>
	/// Gets a limit for the given specification
	/// </summary>
	/// <param name="method">The optional HTTP Method to match</param>
	/// <param name="path">The optional path to match</param>
	/// <param name="limiter">The rate limiter to use</param>
	/// <returns>A limit for the given specification</returns>
	/// <remarks>
	/// <paramref name="path"/> supports the following wildcards: '*', '?', '&lt;', '&gt;', '"'. The backslash character '' escapes.
	/// </remarks>
	public virtual Limit Limiter(string? method, string? path, RateLimiter limiter)
    {
        bool IsApi(MdHttpBuilder builder, out string path)
        {
            path = string.Empty;
			var uri = builder.RequestUri?.ToString();
            if (string.IsNullOrWhiteSpace(uri)) return false;

            if (!uri.StartsWith(_config.ApiUrl, COMPARE))
                return false;
            
            path = uri[_config.ApiUrl.Length..].TrimStart('/').Split('?').First();
            return true;
		}

        bool IsPath(string check)
        {
            return string.IsNullOrWhiteSpace(path) ||
                FileSystemName.MatchesWin32Expression(path.TrimStart('/'), check, true);
        }

        bool IsMethod(MdHttpBuilder builder)
		{
			return string.IsNullOrWhiteSpace(method) ||
				string.Equals(builder.RequestMethod, method, COMPARE);
		}

        return (builder => IsApi(builder, out var p) && IsPath(p) && IsMethod(builder), limiter);
	}

	/// <summary>
	/// Gets a limit for the given specification
	/// </summary>
	/// <param name="method">The optional HTTP Method to match</param>
	/// <param name="path">The optional path to match</param>
	/// <param name="requests">The number of requests allowed</param>
	/// <param name="period">The time period</param>
	/// <returns>A limit for the given specification</returns>
	public virtual Limit Limiter(string? method, string? path, int requests, TimeSpan period)
    {
		return Limiter(method, path, GenerateLimiter(requests, period));
    }

    /// <summary>
    /// Gets a limit for the given specification
    /// </summary>
    /// <param name="method">The optional HTTP Method to match</param>
    /// <param name="path">The optional path to match</param>
    /// <param name="requests">The number of requests allowed</param>
    /// <param name="minutes">The time period in minutes</param>
    /// <returns>A limit for the given specification</returns>
    public virtual Limit Limiter(string? method, string? path, int requests, double minutes)
    {
		return Limiter(method, path, requests, TimeSpan.FromMinutes(minutes));
	}

	/// <summary>
	/// Finds the first limiter that matches the request
	/// </summary>
	/// <param name="request">The fully build request</param>
	/// <param name="token">The cancellation token</param>
	/// <returns>The rate limiter that matches the request, or null if none match</returns>
	public virtual async Task<(RateLimiter? custom, RateLimiter global)> GetLimiter(MdHttpBuilder request, CancellationToken token)
    {
        var (Limits, global) = await LazyLoadLimits(token);

		foreach (var limit in Limits)
		{
			if (limit.Matches(request))
				return (limit.Limiter, global);
		}

		return (null, global);
	}

    /// <inheritdoc />
    public async Task<IDisposable> Limit(MdHttpBuilder request, CancellationToken token)
    {
		List<IDisposable> disposables = [];

        try
        {
            //If we're not handling rate limits, just let it through
            if (!_config.HandleRateLimits)
                return new AggregateDisposable([]);

            //Observe global header-based rate limits
            await _globalPause.WaitAsync(token);

            //Check to see if there is a route-specific limiter and observe it.
            var (limiter, global) = await GetLimiter(request, token);
            if (limiter is not null)
                disposables.Add(await limiter.AcquireAsync(1, token));

            //Observe the global limiter last
            disposables.Add(await global.AcquireAsync(1, token));
            //Return all of the disposables so that they can be disposed of when the request is sent
            return new AggregateDisposable(disposables);
        }
        catch
        {
			//If anything goes wrong, dispose of any disposables we have acquired so far
			foreach (var disposable in disposables)
				disposable.Dispose();
			throw;
		}
	}

    /// <inheritdoc />
    public void Observe(string url, RateLimit limits)
    {
        if (!limits.IsLimited ||
            limits.RetryPassed() ||
            limits.RetryAfter is null)
            return;

        var span = limits.RetryAfter.Value - DateTime.UtcNow;
		_events.OnRateLimitGlobalPaused(url, limits, span);
		_globalPause.Pause(span);
        _globalPause.WaitAsync(CancellationToken.None)
            .ContinueWith(_ => _events.OnRateLimitGlobalUnpaused(url, limits), 
            TaskScheduler.Default);
	}

	/// <inheritdoc />
	public void Dispose()
	{
		_lock.Dispose();
        GC.SuppressFinalize(this);
	}

    /// <summary>
    /// An aggregate disposable
    /// </summary>
    /// <param name="_disposers">The things to dispose</param>
    public class AggregateDisposable(
        IEnumerable<IDisposable> _disposers) : IDisposable
	{
        /// <inheritdoc />
		public void Dispose()
		{
            foreach(var disposer in _disposers)
                disposer.Dispose();
            //Nothing to dispose
            GC.SuppressFinalize(this);
		}
	}
}
