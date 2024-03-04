namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests for the auth.mangadex.org service
/// </summary>
public interface IMangaDexAuthService
{
    /// <summary>
    /// Executes the auth service's token request
    /// </summary>
    /// <param name="request">The request to execute</param>
    /// <returns>The returned token result</returns>
    Task<TokenResult> Request(TokenRequest request);

    /// <summary>
    /// Executes the "password" OAuth 2.0 grant type.
    /// </summary>
    /// <param name="id">The optional client ID</param>
    /// <param name="secret">The optional client secret</param>
    /// <param name="username">The optional username</param>
    /// <param name="password">The optional password</param>
    /// <returns>The returned token result</returns>
    /// <remarks>Any of the optional parameters will be sourced from the <see cref="ICredentialsService"/> if applicable</remarks>
    Task<TokenResult> Personal(
        string? id = null, string? secret = null, 
        string? username = null, string? password = null);

    /// <summary>
    /// Executes the "refresh_token" OAuth 2.0 grant type.
    /// </summary>
    /// <param name="refreshToken">The refresh token that needs to be refreshed</param>
    /// <param name="id">The optional client ID</param>
    /// <param name="secret">The optional client secret</param>
    /// <returns>The returned token result</returns>
    /// <remarks>Any of the optional parameters will be sourced from the <see cref="ICredentialsService"/> if applicable</remarks>
    Task<TokenResult> Refresh(string refreshToken, string? id = null, string? secret = null);
}

internal class MangaDexAuthService : IMangaDexAuthService
{
    private readonly IMdApiService _api;
    private readonly ICredentialsService _creds;

    public MangaDexAuthService(
        IMdApiService api, 
        ICredentialsService creds)
    {
        _api = api;
        _creds = creds;
    }

    public IEnumerable<(string key, string value)> Parameters(TokenRequest request)
    {
        var validGrantTypes = new[] { TokenRequest.GRANT_TYPE_PASSWORD, TokenRequest.GRANT_TYPE_REFRESH };

        if (!validGrantTypes.Contains(request.GrantType))
            throw new ArgumentException($"Invalid grant type: {request.GrantType}", nameof(request.GrantType));

        yield return ("grant_type", request.GrantType);
        yield return ("client_id", request.ClientId);
        yield return ("client_secret", request.ClientSecret);

        if (request.GrantType == TokenRequest.GRANT_TYPE_PASSWORD)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                throw new ArgumentException("Username is required for password grant type", nameof(request.Username));

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Password is required for password grant type", nameof(request.Password));

            yield return ("username", request.Username);
            yield return ("password", request.Password);
        }

        if (request.GrantType == TokenRequest.GRANT_TYPE_REFRESH)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new ArgumentException("Refresh token is required for refresh grant type", nameof(request.RefreshToken));

            yield return ("refresh_token", request.RefreshToken);
        }
    }

    public string Validate(string? value, string? creds, string name)
    {
        return value ?? creds 
            ?? throw new ArgumentNullException(name, 
            $"{name} is required - You can specify it in either the credentials service or the parameters");
    }

    public string GetUrl(TokenRequest request)
    {
        return request.GrantType switch
        {
            TokenRequest.GRANT_TYPE_REFRESH => WrapUrl("realms/mangadex/protocol/openid-connect/token"),
            TokenRequest.GRANT_TYPE_PASSWORD => WrapUrl("realms/mangadex/protocol/openid-connect/token"),
            _ => throw new ArgumentException($"Invalid grant type: {request.GrantType}", nameof(request.GrantType))
        };
    }

    public string WrapUrl(string endpoint)
    {
        var auth = _creds.AuthUrl.Trim('/');
        var url = endpoint.TrimStart('/');

        return $"{auth}/{url}";
    }

    public async Task<TokenResult> Request(TokenRequest request)
    {
        var url = GetUrl(request);
        var parameters = Parameters(request).ToArray();
        return await _api.Post<TokenResult>(url, parameters) ?? throw new Exception("Failed to get token - Result is null");
    }

    public Task<TokenResult> Personal(
        string? id = null, string? secret = null, 
        string? username = null, string? password = null)
    {
        var clientId = Validate(id, _creds.ClientId, "Client ID");
        var clientSecret = Validate(secret, _creds.ClientSecret, "Client Secret");
        var un = Validate(username, _creds.Username, "Username");
        var pw = Validate(password, _creds.Password, "Password");
        var request = TokenRequest.PasswordFlow(clientId, clientSecret, un, pw);
        return Request(request);
    }

    public Task<TokenResult> Refresh(string refreshToken, string? id = null, string? secret = null)
    {
        var clientId = Validate(id, _creds.ClientId, "Client ID");
        var clientSecret = Validate(secret, _creds.ClientSecret, "Client Secret");
        var request = TokenRequest.RefreshFlow(clientId, clientSecret, refreshToken);
        return Request(request);
    }
}
