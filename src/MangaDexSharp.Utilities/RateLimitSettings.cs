namespace MangaDexSharp.Utilities;

/// <summary>
/// Optional settings for rate limits
/// </summary>
public class RateLimitSettings
{
    /// <summary>
    /// Triggered when a rate limit timeout is required
    /// </summary>
    public Action<RateLimit, TimeSpan> TimeoutRequired = (_, _) => { };

    /// <summary>
    /// Triggered when the rate limit timeout has passed
    /// </summary>
    public Action<RateLimit> TimeoutPassed = (_) => { };

    /// <summary>
    /// Triggered when a response is received from the API
    /// </summary>
    public Action<MangaDexRoot> ResponseReceived = (_) => { };
}
