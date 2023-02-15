namespace MangaDexSharp;

public interface IMangaDexUserService
{
	Task<UserList> List(UserFilter? filter = null, string? token = null);

	Task<MangaDexRoot<User>> Get(string id);

	Task<MangaDexRoot<User>> Me(string? token = null);

	[Obsolete]
	Task<LoginResult> Login(LoginRequest request);

	[Obsolete]
	Task<LoginResult> Login(string username, string password);

	[Obsolete]
	Task<LoginResult> Refresh(string token);
}

public class MangaDexUserService : IMangaDexUserService
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

public class LoginRequest
{
	[JsonPropertyName("username")]
	public string Username { get; set; } = string.Empty;

	[JsonPropertyName("email")]
	public string Email { get; set; } = string.Empty;

	[JsonPropertyName("password")]
	public string Password { get; set; } = string.Empty;
}

public class LoginResult
{
	[JsonPropertyName("result")]
	public string Result { get; set; } = string.Empty;

	[JsonPropertyName("message")]
	public string Message { get; set; } = string.Empty;

	[JsonPropertyName("token")]
	public Token Data { get; set; } = new();

	public class Token
	{
		[JsonPropertyName("session")]
		public string Session { get; set; } = string.Empty;

		[JsonPropertyName("refresh")]
		public string Refresh { get; set; } = string.Empty;
	}
}

public class RefreshRequest
{
	[JsonPropertyName("token")]
	public string Refresh { get; set; } = string.Empty;
}