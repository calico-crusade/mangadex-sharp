namespace MangaDexSharp;

public interface IMangaDexFollowsService
{
	Task<ScanlationGroupList> Groups(int offset = 0, int limit = 100, string? token = null);
	Task<UserList> Users(int offset = 0, int limit = 100, string? token = null);
	Task<MangaDexRoot> User(string userId, string? token = null);
	Task<MangaList> Manga(int offset = 0, int limit = 100, MangaIncludes[]? includes = null, string? token = null);
	Task<MangaDexRoot> Manga(string mangaId, string? token = null);
	Task<CustomListList> Lists(int offset = 0, int limit = 100, string? token = null);
	Task<CustomListList> List(string listId, string? token = null);
}

public class MangaDexFollowsService : IMangaDexFollowsService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => _creds.ApiUrl;

	public MangaDexFollowsService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<ScanlationGroupList> Groups(int offset = 0, int limit = 100, string? token = null)
	{
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Add("includes", new[] { "leader", "member" })
			.Build();
		var url = $"{Root}/user/follows/group?{bob}";
		return await _api.Get<ScanlationGroupList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<UserList> Users(int offset = 0, int limit = 100, string? token = null)
	{
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Build();
		var url = $"{Root}/user/follows/user?{bob}";
		return await _api.Get<UserList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> User(string userId, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/user/follows/user/{userId}";
		return await _api.Get<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaList> Manga(int offset = 0, int limit = 100, MangaIncludes[]? includes = null, string? token = null)
	{
		includes ??= new[]
		{
			MangaIncludes.manga,
			MangaIncludes.cover_art,
			MangaIncludes.author,
			MangaIncludes.artist,
			MangaIncludes.tag
		};
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Add("includes", includes)
			.Build();
		var url = $"{Root}/user/follows/manga?{bob}";
		return await _api.Get<MangaList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Manga(string mangaId, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/user/follows/manga/{mangaId}";
		return await _api.Get<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}

	public async Task<CustomListList> Lists(int offset = 0, int limit = 100, string? token = null)
	{
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Build();
		var url = $"{Root}/user/follows/list?{bob}";
		return await _api.Get<CustomListList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<CustomListList> List(string listId, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/user/follows/list/{listId}";
		return await _api.Get<CustomListList>(url, c) ?? new() { Result = "error" };
	}
}
