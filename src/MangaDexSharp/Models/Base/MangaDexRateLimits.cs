namespace MangaDexSharp;

/// <summary>
/// Represents a return result from the MangaDex API that has rate limit headers
/// </summary>
public abstract class MangaDexRateLimits
{
    /// <summary>
    /// Contains the rate limit header values from the response if they are present
    /// </summary>
    /// <remarks>This is not included in json serialized strings</remarks>
    [JsonIgnore]
    public RateLimit RateLimit { get; set; } = new();
}

/// <summary>
/// Represents the rate limit headers from the response
/// </summary>
public class RateLimit
{
    /// <summary>
    /// The value of the X-RateLimit-Limit header if it is present in the response
    /// </summary>
    /// <remarks>This is not included in json serialized strings</remarks>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// The value of the X-RateLimit-Remaining header if it is present in the response
    /// </summary>
    /// <remarks>This is not included in json serialized strings</remarks>
    [JsonIgnore]
    public int? Remaining { get; set; }

    /// <summary>
    /// The value of the X-RateLimit-Retry-After header if it is present in the response
    /// </summary>
    /// <remarks>This is not included in json serialized strings</remarks>
    [JsonIgnore]
    public DateTime? RetryAfter { get; set; }

    /// <summary>
    /// Whether or not any of the rate-limit headers were set in the response
    /// </summary>
    /// <remarks>This is a computed value and not included in json serialized strings</remarks>
    [JsonIgnore]
    public bool HasRateLimits => Limit.HasValue || Remaining.HasValue || RetryAfter.HasValue;

    /// <summary>
    /// Whether or not the rate limit has been reached
    /// </summary>
    /// <remarks>This is a computed value and not included in json serialized strings</remarks>
    [JsonIgnore]
    public bool IsLimited => Limit.HasValue && Remaining.HasValue && Remaining.Value == 0;
}