namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests regarding the current user and the obsoleted login methods
/// </summary>
public interface IMangaDexUserService
{
	/// <summary>
	/// Requests a paginated list of users
	/// </summary>
	/// <param name="filter">How to filter the users</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of users</returns>
	Task<UserList> List(UserFilter? filter = null, string? token = null);

	/// <summary>
	/// Fetches the profile of a specific user
	/// </summary>
	/// <param name="id">The ID of the user</param>
	/// <returns>The requested user</returns>
	Task<MangaDexRoot<User>> Get(string id);

	/// <summary>
	/// Deletes a user by ID
	/// </summary>
	/// <param name="id">The ID of the user to delete</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Delete(string id, string? token = null);

	/// <summary>
	/// Approves user deletion using the given deletion code
	/// </summary>
	/// <param name="code">The deletion approval code</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> ApproveDelete(string code, string? token = null);

	/// <summary>
	/// Fetches the current user's profile
	/// </summary>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The current users profile</returns>
	Task<MangaDexRoot<User>> Me(string? token = null);

	/// <summary>
	/// Fetches the current user's reading history
	/// </summary>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The current user's reading history</returns>
	Task<ReadingHistory> History(string? token = null);

	/// <summary>
	/// Logs into MD using the given email/username and password combination
	/// </summary>
	/// <param name="request">The login request</param>
	/// <returns>The results of the request</returns>
	[Obsolete("Email/Password logins are now obsolete. Prioritize using the new OAuth2 login method")]
	Task<LoginResult> Login(LoginRequest request);

	/// <summary>
	/// Logs into MD using the given username and password combination
	/// </summary>
	/// <param name="username">The username of the account</param>
	/// <param name="password">The password of the account</param>
	/// <returns>The results of the request</returns>
	[Obsolete("Email/Password logins are now obsolete. Prioritize using the new OAuth2 login method")]
	Task<LoginResult> Login(string username, string password);

	/// <summary>
	/// Attempts to refresh the given token
	/// </summary>
	/// <param name="token">The refresh token</param>
	/// <returns>The results of the request</returns>
	[Obsolete("Email/Password logins are now obsolete. Prioritize using the new OAuth2 login method")]
	Task<LoginResult> Refresh(string token);
}

internal class MangaDexUserService(IMdApiService _api) : IMangaDexUserService
{
	public string Root => $"user";

	public async Task<UserList> List(UserFilter? filter = null, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"{Root}?{(filter ?? new()).BuildQuery()}";
		return await _api.Get<UserList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<User>> Get(string id)
	{
		return await _api.Get<MangaDexRoot<User>>($"{Root}/{id}") ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> ApproveDelete(string code, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<MangaDexRoot, MangaDexEmpty>($"{Root}/delete/{code}", new MangaDexEmpty(), c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<User>> Me(string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Get<MangaDexRoot<User>>($"{Root}/me", c) ?? new() { Result = "error" };
	}

	public async Task<ReadingHistory> History(string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Get<ReadingHistory>($"{Root}/history", c) ?? new() { Result = "error" };
	}

	[Obsolete]
	public async Task<LoginResult> Login(LoginRequest request)
	{
		return await _api.Post<LoginResult, LoginRequest>($"auth/login", request) ?? new() { Result = "error" };
	}

	[Obsolete]
	public Task<LoginResult> Login(string username, string password)
	{
		return Login(new() { Username = username, Password = password });
	}

	[Obsolete]
	public async Task<LoginResult> Refresh(string token)
	{
		var request = new RefreshRequest { Refresh = token };
		return await _api.Post<LoginResult, RefreshRequest>($"auth/refresh", request) ?? new() { Result = "error" };
	}
}
