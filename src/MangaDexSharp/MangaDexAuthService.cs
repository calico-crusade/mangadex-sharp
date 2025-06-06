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

/// <summary>
/// This is mostly still here to maintain compatibility with the old service.
/// Most everything was moved to <see cref="OIDCService"/>.
/// </summary>
internal class MangaDexAuthService(IOIDCService _auth) : IMangaDexAuthService
{
    public Task<TokenResult> Request(TokenRequest request)
    {
        return _auth.Request(request);
    }

    public Task<TokenResult> Personal(
        string? id = null, string? secret = null, 
        string? username = null, string? password = null)
    {
        return _auth.Personal(id, secret, username, password);
    }

    public Task<TokenResult> Refresh(string refreshToken, string? id = null, string? secret = null)
    {
        return _auth.Refresh(refreshToken, id, secret);
    }
}
