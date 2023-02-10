namespace MangaDexSharp;

public interface IMangaDexCustomListService
{
	Task<MangaDexRoot<CustomList>> Create(CustomListCreate create, string? token = null);

	Task<MangaDexRoot<CustomList>> Get(string id);

	Task<MangaDexRoot<CustomList>> Update(string id, CustomListCreate create, string? token = null);

	Task<MangaDexRoot> Delete(string id, string? token = null);

	Task<MangaDexRoot> Follow(string id, string? token = null);

	Task<MangaDexRoot> Unfollow(string id, string? token = null);

	Task<MangaDexRoot> MangaAdd(string mangaId, string listId, string? token = null);

	Task<MangaDexRoot> MangaRemove(string mangaId, string listId, string? token = null);

	Task<CustomListList> List(int limit = 100, int offset = 0, string? token = null);

	Task<CustomListList> List(string userId, int limit = 100, int offset = 0);
}

public class MangaDexCustomListService : IMangaDexCustomListService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{API_ROOT}/list";

	public MangaDexCustomListService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<MangaDexRoot<CustomList>> Create(CustomListCreate create, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<CustomList>, CustomListCreate>(Root, create, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<CustomList>> Get(string id)
	{
		return await _api.Get<MangaDexRoot<CustomList>>($"{Root}/{id}") ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<CustomList>> Update(string id, CustomListCreate create, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<CustomList>, CustomListCreate>($"{Root}/{id}", create, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot<CustomList>>($"{Root}/{id}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Follow(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot, MangaDexEmpty>($"{Root}/{id}/follow", new MangaDexEmpty { }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Unfollow(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}/follow", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> MangaAdd(string mangaId, string listId, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot, MangaDexEmpty>($"{API_ROOT}/manga/{mangaId}/list/{listId}", new MangaDexEmpty { }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> MangaRemove(string mangaId, string listId, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{API_ROOT}/manga/{mangaId}/list/{listId}", c) ?? new() { Result = "error" };
	}

	public async Task<CustomListList> List(int limit = 100, int offset = 0, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Get<CustomListList>($"{API_ROOT}/user/list?limit={limit}&offset={offset}", c) ?? new() { Result = "error" };
	}

	public async Task<CustomListList> List(string userId, int limit = 100, int offset = 0)
	{
		return await _api.Get<CustomListList>($"{API_ROOT}/user/{userId}/list?limit={limit}&offset={offset}") ?? new() { Result = "error" };
	}
}
