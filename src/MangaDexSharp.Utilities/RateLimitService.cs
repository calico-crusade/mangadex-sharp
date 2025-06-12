using System.Runtime.CompilerServices;

namespace MangaDexSharp.Utilities;

/// <summary>
/// Service for handling rate-limits for the MD API
/// </summary>
public interface IRateLimitService : IMdUtil
{
    /// <summary>
    /// Makes a request to MD while making sure rate-limits are observed
    /// </summary>
    /// <typeparam name="T">The return result of the API</typeparam>
    /// <param name="request">The request to make</param>
    /// <param name="token">The cancellation token for the rate-limits check</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <returns>The result of the request</returns>
    Task<T> Request<T>(Func<IMangaDex, Task<T>> request, CancellationToken token, RateLimitSettings? settings = null)
        where T : MangaDexRoot, new();

    /// <summary>
    /// Makes a paginated request to MD while making sure rate-limits are observed
    /// </summary>
    /// <typeparam name="T">The return type of the request</typeparam>
    /// <typeparam name="TFilter">The pagination filter to use</typeparam>
    /// <param name="request">The request to make</param>
    /// <param name="start">The starting pagination filter to use</param>
    /// <param name="filter">An optional filter for excluding items</param>
    /// <param name="settings">The optional settings for the requests</param>
    /// <param name="token">The cancellation token for the request</param>
    /// <returns>All of the entities requested</returns>
    IAsyncEnumerable<T> Request<T, TFilter>(
        Func<IMangaDex, TFilter, Task<MangaDexCollection<T>>> request,
        TFilter? start = default,
        Func<T, bool>? filter = null,
        RateLimitSettings? settings = null,
        CancellationToken token = default)
        where T : new()
        where TFilter : IPaginateFilter, new();
}

internal class RateLimitService(
    IMangaDex _md,
    IConfiguration _config,
    ILogger<RateLimitService> _logger) : IRateLimitService
{
    /// <summary>
    /// Semaphore for observing the global 5 concurrent requests limit
    /// </summary>
    private readonly SemaphoreSlim _globalLimit = new(5);

    /// <summary>
    /// The last rate limit received from the API
    /// </summary>
    private RateLimit? _last;

    /// <inheritdoc cref="MaxRetries"/>
    private int? _maxRetries;

    /// <inheritdoc cref="MinDelay"/>
    private TimeSpan? _minDelay;

    /// <summary>
    /// The max number of retries to make when a rate-limit is hit
    /// </summary>
    public int MaxRetries => _maxRetries ??= int.TryParse(_config["MaxRetries"], out var val) ? val : 3;

    /// <summary>
    /// The minimum number of seconds to wait before retrying a request after a rate-limit is hit
    /// </summary>
    public TimeSpan MinDelay => _minDelay ??= TimeSpan.FromSeconds(double.TryParse(_config["MinDelaySec"], out var val) ? val : 0);

    /// <summary>
    /// Returns the minimum delay for the given time span
    /// </summary>
    /// <param name="span">The span to check</param>
    /// <param name="second">The optional second span</param>
    /// <returns>The higher of the two spans</returns>
    public TimeSpan DetermineDelay(TimeSpan span, TimeSpan? second = null)
    {
        TimeSpan[] options = [span, MinDelay, second ?? TimeSpan.Zero];
        #if NET8_0_OR_GREATER
        return options.MaxBy(t => t.TotalMilliseconds);
        #else
        return options.OrderByDescending(t => t.TotalMilliseconds).First();
        #endif
    }

    /// <summary>
    /// Ensure the rate-limit is not hit before making a request
    /// </summary>
    /// <param name="token">The cancellation token for the wait</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <param name="after429">If the check occurs after receiving a 429 error </param>
    public async Task EnsureNotLimited(RateLimitSettings settings, bool after429, CancellationToken token)
    {
        try
        {
            await _globalLimit.WaitAsync(token);
            //If the rate limits aren't set, skip it.
            if ((_last is null ||
                !_last.HasRateLimits ||
                !_last.IsLimited ||
                _last.RetryPassed()) 
                && !after429) return;
            //Ensure we do actually wait if we received a 429 error
            TimeSpan? second = (_last is null || !_last.HasRateLimits) && after429
                ? TimeSpan.FromSeconds(30)
                : null;
            //Get the retry time
            var retry = _last?.RetryAfter ?? DateTime.UtcNow;
            //Calculate how long we have to wait for it to pass
            var span = DetermineDelay(retry - DateTime.UtcNow, second);
            //No timeout? skip it
            if (span.TotalMilliseconds <= 0) return;
            var last = _last ?? new RateLimit
            {
                Limit = 1,
                Remaining = 0,
                RetryAfter = retry
            };
            //Indicate that a timeout is required
            settings.TimeoutRequired(last, span);
            //Wait for the time to pass
            _logger.LogInformation("Rate limit hit, waiting {Span}ms", span.TotalMilliseconds);
            await Task.Delay(span, token);
            _logger.LogInformation("Rate limit passed, continuing...");
            //Indicate that the timeout has passed
            settings.TimeoutPassed(last);
        }
        finally
        {
            _globalLimit.Release();
        }
    }

    /// <summary>
    /// Creates an instance of the given type with errors
    /// </summary>
    /// <typeparam name="T">The type of object to create</typeparam>
    /// <param name="message">The message to use for the error</param>
    /// <param name="detail">The optional detailed version of the error message</param>
    /// <param name="status">The optional status code of the error</param>
    /// <returns>The errored result</returns>
    internal static T CreateError<T>(string message, string? detail = null, int status = 500)
        where T : MangaDexRoot, new()
    {
        return new T
        {
            Result = MangaDexRoot.RESULT_ERROR,
            Errors = [ new MangaDexError
            {
                Status = status,
                Title = message,
                Detail = detail ?? message
            }]
        };
    }

    /// <summary>
    /// Makes a request to MD while making sure rate-limits are observed
    /// </summary>
    /// <typeparam name="T">The return result of the API</typeparam>
    /// <param name="request">The request to make</param>
    /// <param name="current">The current retry count</param>
    /// <param name="settings">The optional settings for the request</param>
    /// <param name="token">The cancellation token for the rate-limits check</param>
    /// <returns>The result of the request</returns>
    public async Task<T> Request<T>(Func<IMangaDex, Task<T>> request, int current, RateLimitSettings settings, CancellationToken token)
        where T : MangaDexRoot, new()
    {
        if (current > MaxRetries)
            return CreateError<T>("Max retry count exceeded", $"Max retry count exceeded: {current}", 500);

        await EnsureNotLimited(settings, current != 0, token);
        var result = await request(_md);
        settings.ResponseReceived(result);

        if (result.RateLimit.HasRateLimits)
            _last = result.RateLimit;

        if (!result.ErrorOccurred) return result;

        var isTooMany = result.Errors.Any(e => e.Status == 429);

        if (isTooMany)
            return await Request(request, current + 1, settings, token);

        return result;
    }

    /// <inheritdoc/>
    public Task<T> Request<T>(Func<IMangaDex, Task<T>> request, CancellationToken token, RateLimitSettings? settings = null)
        where T : MangaDexRoot, new()
    {
        return Request(request, 0, settings ?? new(), token);
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<T> Request<T, TFilter>(
        Func<IMangaDex, TFilter, Task<MangaDexCollection<T>>> request,
        TFilter? start = default,
        Func<T, bool>? filter = null,
        RateLimitSettings? settings = null,
        [EnumeratorCancellation] CancellationToken token = default)
        where T : new()
        where TFilter : IPaginateFilter, new()
    {
        start ??= new();
        int offset = start.Offset;
        while (true)
        {
            if (token.IsCancellationRequested) yield break;

            start.Offset = offset;
            var items = await Request(t => request(t, start), token, settings);
            items.ThrowIfError();
            offset += start.Limit;

            if (items.Data.Count == 0) yield break;

            foreach (var item in items.Data)
            {
                if (token.IsCancellationRequested) yield break;
                if (filter is null || filter(item))
                    yield return item;
            }

            if (items.Data.Count < start.Limit ||
                start.Offset + start.Limit >= items.Total) yield break;
        }
    }
}
