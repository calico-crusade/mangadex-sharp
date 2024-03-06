namespace MangaDexSharp.Cli.Services;

internal class PersonalCredentialsService(
    IConfiguration config,
    ITokenCacheService _token,
    ILogger<PersonalCredentialsService> _logger) : ConfigurationCredentialsService(config)
{
    private IMangaDex? _client;
    private TokenData? _data;

    public IMangaDex Client => _client ??= MangaDex.Create();

    public async Task<TokenData?> GetCachedToken()
    {
        return _data ??= await _token.Get();
    }

    public async Task<TokenData> SetCacheToken(TokenData data)
    {
        await _token.Set(data);
        return _data = data;
    }

    public async Task<TokenResult> Fetch()
    {
        var token = await Client.Auth.Personal(
            ClientId,
            ClientSecret,
            Username,
            Password);
        return await SetCacheToken(TokenData.From(token));
    }

    public async Task<TokenResult> Refresh(TokenResult old)
    {
        var token = await Client.Auth.Refresh(old.RefreshToken, ClientId, ClientSecret);
        return await SetCacheToken(TokenData.From(token));
    }

    public async Task<TokenResult> GetTokenData()
    {
        var data = await GetCachedToken();
        if (data is null)
        {
            _logger.LogDebug("No cached token found, fetching new token");
            return await Fetch();
        }

        if (!data.AccessExpired)
        {
            _logger.LogDebug("Token is valid, using it");
            return data;
        }

        if (data.RefreshExpired)
        {
            _logger.LogDebug("Refresh token is expired, fetching new token set");
            return await Fetch();
        }

        _logger.LogDebug("Token is expired, refreshing token set");
        return await Refresh(data);
    }

    public override async Task<string?> GetToken()
    {
        return (await GetTokenData())?.AccessToken;
    }
}
