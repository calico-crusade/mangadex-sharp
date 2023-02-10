namespace MangaDexSharp;

public interface IMangaDexUserService
{
	Task<UserList> List(UserFilter? filter = null, string? token = null);

	Task<MangaDexRoot<User>> Get(string id);

	Task<MangaDexRoot<User>> Me(string? token = null);
}

public class MangaDexUserService : IMangaDexUserService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{API_ROOT}/user";

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
}
