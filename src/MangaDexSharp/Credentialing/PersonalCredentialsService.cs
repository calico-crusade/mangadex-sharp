using System.Threading;

namespace MangaDexSharp;

/// <summary>
/// An instance of the <see cref="ICredentialsService"/> that automatically fetches and refreshes the access token
/// </summary>
/// <param name="_config">The configuration for the OIDC endpoints</param>
/// <param name="_auth">The auth service for requesting token</param>
/// <param name="_cache">The optional caching service for the tokens</param>
public class PersonalCredentialsService(
    IConfigurationOIDC _config,
    IOIDCService _auth,
    ITokenCacheService? _cache = null) : ICredentialsService
{
    private (TokenResult? token, DateTime? executed) _last = (null, null);
    private readonly SemaphoreSlim _lock = new(1);

    /// <summary>
    /// Whether or not the access token is expired
    /// </summary>
    public bool AccessExpired => Expired(_last.token?.ExpiresIn);

    /// <summary>
    /// Whether or not the refresh token is expired
    /// </summary>
    public bool RefreshExpired => Expired(_last.token?.RefreshExpiresIn);

    /// <summary>
    /// Whether or not this service is enabled
    /// </summary>
    public bool Enabled =>
        !string.IsNullOrEmpty(_config.ClientId) &&
        !string.IsNullOrEmpty(_config.ClientSecret) &&
        !string.IsNullOrEmpty(_config.Username) &&
        !string.IsNullOrEmpty(_config.Password);

    /// <summary>
    /// Whether or not the given number of seconds has expired
    /// </summary>
    /// <param name="seconds">The number of seconds</param>
    /// <returns>Whether or not it's expired</returns>
    public bool Expired(double? seconds)
    {
        if (!_last.executed.HasValue) return true;
        if (!seconds.HasValue) return true;
        return _last.executed.Value.AddSeconds(seconds.Value) < DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the last request made to the auth service
    /// </summary>
    /// <returns>The last request made</returns>
    public async Task<TokenResult?> GetCache()
    {
        if (_last.token is not null &&
            _last.executed.HasValue) 
            return _last.token;

        if (_cache is null) return _last.token;

        return (_last = await _cache.GetCache()).token;
    }

    /// <summary>
    /// Sets the cache for the access token and the time it was last executed
    /// </summary>
    /// <param name="token">The token data</param>
    public async Task SetCache(TokenResult? token)
    {
        _last = (token, DateTime.UtcNow);
        if (_cache is not null) 
            await _cache.SetCache(token, DateTime.UtcNow);
    }

    /// <summary>
    /// Fetch and cache the access token
    /// </summary>
    /// <returns>The access token details</returns>
    public async Task<TokenResult?> Fetch()
    {
        try
        {
            await _lock.WaitAsync();
            var token = await _auth.Personal();
            if (token is null) return null;

            await SetCache(token);
            return token;
        }
        finally
        {
            _lock.Release();
        }
    }

    /// <summary>
    /// Refresh and cache the access token
    /// </summary>
    /// <param name="refresh">The refresh token to use</param>
    /// <returns>The access token details</returns>
    public async Task<TokenResult?> Refresh(string refresh)
    {
        try
        {
            await _lock.WaitAsync();
            var token = await _auth.Refresh(refresh);
            if (token is null) return null;

            await SetCache(token);
            return token;
        }
        finally
        {
            _lock.Release();
        }
    }

    /// <summary>
    /// Fetches the access token if it is expired, or returns the cached token if it is still valid
    /// </summary>
    /// <returns>The access token</returns>
    public virtual async Task<string?> GetToken()
    {
        //Ensure the service is enabled
        if (!Enabled) return null;
        //Lazily fetch the access token
        var last = await GetCache() ?? await Fetch();
        if (last is null) return null;
        //If it's not expired, just return it
        if (!AccessExpired) return last.AccessToken;
        //If the refresh token is expired, fetch a new access token
        if (RefreshExpired) return (await Fetch())?.AccessToken;
        //Refresh the access token, if the refresh token is valid
        var refresh = await Refresh(last.RefreshToken);
        return refresh?.AccessToken;
    }
}
