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
	/// Fetches the current user's profile
	/// </summary>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The current users profile</returns>
	Task<MangaDexRoot<User>> Me(string? token = null);

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

internal class MangaDexUserService : IMangaDexUserService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{_creds.ApiUrl}/user";

	public MangaDexUserService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<UserList> List(UserFilter? filter = null, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}?{(filter ?? new()).BuildQuery()}";
		return await _api.Get<UserList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<User>> Get(string id)
	{
		return await _api.Get<MangaDexRoot<User>>($"{Root}/{id}") ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<User>> Me(string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Get<MangaDexRoot<User>>($"{Root}/me", c) ?? new() { Result = "error" };
	}

	[Obsolete]
	public async Task<LoginResult> Login(LoginRequest request)
	{
		return await _api.Post<LoginResult, LoginRequest>($"{_creds.ApiUrl}/auth/login", request) ?? new() { Result = "error" };
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
		return await _api.Post<LoginResult, RefreshRequest>($"{_creds.ApiUrl}/auth/refresh", request) ?? new() { Result = "error" };
	}
}

/// <summary>
/// Represents the request to login to MD using the old username/password combination
/// </summary>
public class LoginRequest
{
	/// <summary>
	/// The username of the account
	/// </summary>
	[JsonPropertyName("username")]
	public string Username { get; set; } = string.Empty;

	/// <summary>
	/// The email address of the account
	/// </summary>
	[JsonPropertyName("email")]
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// The password of the account
	/// </summary>
	[JsonPropertyName("password")]
	public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Represents the result of a login request
/// </summary>
public class LoginResult
{
	/// <summary>
	/// Whether or not the result was successful
	/// </summary>
	[JsonPropertyName("result")]
	public string Result { get; set; } = string.Empty;

	/// <summary>
	/// The error message if the request failed
	/// </summary>
	[JsonPropertyName("message")]
	public string Message { get; set; } = string.Empty;

	/// <summary>
	/// The token data for the login session
	/// </summary>
	[JsonPropertyName("token")]
	public Token Data { get; set; } = new();

	/// <summary>
	/// Represents an authentication token for a login request
	/// </summary>
	public class Token
	{
		/// <summary>
		/// The authentication token for the session
		/// </summary>
		[JsonPropertyName("session")]
		public string Session { get; set; } = string.Empty;

		/// <summary>
		/// The refresh token for the session
		/// </summary>
		[JsonPropertyName("refresh")]
		public string Refresh { get; set; } = string.Empty;
	}
}

/// <summary>
/// Represents a request to refresh a token
/// </summary>
public class RefreshRequest
{
	/// <summary>
	/// The refresh token
	/// </summary>
	[JsonPropertyName("token")]
	public string Refresh { get; set; } = string.Empty;
}