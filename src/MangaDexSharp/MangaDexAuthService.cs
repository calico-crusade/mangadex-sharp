namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests for the auth.mangadex.org service
/// </summary>
public interface IMangaDexAuthService
{
	/// <summary>
	/// Checks the permissions associated with the current access token
	/// </summary>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The permissions associated with the token</returns>
	Task<AuthCheck> Check(string? token = null);

	/// <summary>
	/// Logs the current access token out of the MangaDex API
	/// </summary>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The logout result</returns>
	Task<AuthLogout> Logout(string? token = null);

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
internal class MangaDexAuthService(IOIDCService _auth, IMdApiService _api) : IMangaDexAuthService
{
	public async Task<AuthCheck> Check(string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Get<AuthCheck>("auth/check", c) ?? new() { Result = "error" };
	}

	public async Task<AuthLogout> Logout(string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<AuthLogout, MangaDexEmpty>("auth/logout", new MangaDexEmpty(), c) ?? new() { Result = "error" };
	}

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
