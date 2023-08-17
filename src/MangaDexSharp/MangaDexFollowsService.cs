namespace MangaDexSharp;

/// <summary>
/// Represents all of the different requests relating to objects the current user follows
/// </summary>
public interface IMangaDexFollowsService
{
	/// <summary>
	/// Requests a paginated list of all of the scanlation groups the current user follows 
	/// </summary>
	/// <param name="offset">How many items to skip when requestin groups</param>
	/// <param name="limit">How many items to limit one request to</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of scanlation groups</returns>
	Task<ScanlationGroupList> Groups(int offset = 0, int limit = 100, string? token = null);

	/// <summary>
	/// Requests a paginated list of all of the users the current user follows
	/// </summary>
	/// <param name="offset">How many items to skip when requesting users</param>
	/// <param name="limit">How many items to limit one request to</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of users</returns>
	Task<UserList> Users(int offset = 0, int limit = 100, string? token = null);

	/// <summary>
	/// Checks if the current user follows a specific user
	/// </summary>
	/// <param name="userId">The ID of the user</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> User(string userId, string? token = null);

	/// <summary>
	/// Requests a paginated list of all of the manga the current user follows
	/// </summary>
	/// <param name="offset">How many items to skip when requesting users</param>
	/// <param name="limit">How many items to limit one request to</param>
	/// <param name="includes">What relationship data to include</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of manga</returns>
	Task<MangaList> Manga(int offset = 0, int limit = 100, MangaIncludes[]? includes = null, string? token = null);

	/// <summary>
	/// Checks if the current user follows the given manga
	/// </summary>
	/// <param name="mangaId">The ID of the manga</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Manga(string mangaId, string? token = null);

	/// <summary>
	/// Requests a paginated list of the custom lists that the current user follows
	/// </summary>
	/// <param name="offset">How many items to skip when requesting custom lists</param>
	/// <param name="limit">How many items to limit one request to</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of custom lists</returns>
	Task<CustomListList> Lists(int offset = 0, int limit = 100, string? token = null);

	/// <summary>
	/// Checks if the current users follows the specified custom list
	/// </summary>
	/// <param name="listId">The ID of the custom list</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> List(string listId, string? token = null);
}

internal class MangaDexFollowsService : IMangaDexFollowsService
{
	private readonly IMdApiService _api;

	public MangaDexFollowsService(IMdApiService api)
	{
		_api = api;
	}

	public async Task<ScanlationGroupList> Groups(int offset = 0, int limit = 100, string? token = null)
	{
		var c = await _api.Auth(token);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Add("includes", new[] { "leader", "member" })
			.Build();
		var url = $"user/follows/group?{bob}";
		return await _api.Get<ScanlationGroupList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<UserList> Users(int offset = 0, int limit = 100, string? token = null)
	{
		var c = await _api.Auth(token);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Build();
		var url = $"user/follows/user?{bob}";
		return await _api.Get<UserList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> User(string userId, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"user/follows/user/{userId}";
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
		var c = await _api.Auth(token);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Add("includes", includes)
			.Build();
		var url = $"user/follows/manga?{bob}";
		return await _api.Get<MangaList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Manga(string mangaId, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"user/follows/manga/{mangaId}";
		return await _api.Get<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}

	public async Task<CustomListList> Lists(int offset = 0, int limit = 100, string? token = null)
	{
		var c = await _api.Auth(token);
		var bob = new FilterBuilder()
			.Add("limit", limit)
			.Add("offset", offset)
			.Build();
		var url = $"user/follows/list?{bob}";
		return await _api.Get<CustomListList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> List(string listId, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"user/follows/list/{listId}";
		return await _api.Get<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}
}
