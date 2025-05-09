namespace MangaDexSharp;

/// <summary>
/// A service for caching the token and the time it was last executed
/// </summary>
public interface ITokenCacheService
{
    /// <summary>
    /// Gets the token and the time it was last executed
    /// </summary>
    /// <returns>The cache data</returns>
    Task<(TokenResult? result, DateTime? executed)> GetCache();

    /// <summary>
    /// Sets the token and the time it was last executed
    /// </summary>
    /// <param name="result">The token data</param>
    /// <param name="executed">The date the token was fetched</param>
    Task SetCache(TokenResult? result, DateTime? executed);
}