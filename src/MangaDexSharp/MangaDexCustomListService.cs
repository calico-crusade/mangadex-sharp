namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests on the /list endpoints
/// </summary>
public interface IMangaDexCustomListService
{
	/// <summary>
	/// Creates a custom list
	/// </summary>
	/// <param name="create">The custom list to create</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The custom list that was created</returns>
	Task<MangaDexRoot<CustomList>> Create(CustomListCreate create, string? token = null);

	/// <summary>
	/// Fetches a custom list by ID
	/// </summary>
	/// <param name="id">The ID of the custom list</param>
	/// <param name="includeManga">Whether or not to include the manga data</param>
	/// <returns>The custom list object</returns>
	Task<MangaDexRoot<CustomList>> Get(string id, bool includeManga = false);

	/// <summary>
	/// Updates a custom list
	/// </summary>
	/// <param name="id">The ID of the custom list to update</param>
	/// <param name="create">The custom list data</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The custom list that was updated</returns>
	Task<MangaDexRoot<CustomList>> Update(string id, CustomListCreate create, string? token = null);

	/// <summary>
	/// Deletes a custom list
	/// </summary>
	/// <param name="id">The ID of the custom list to delete</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The result of the request</returns>
	Task<MangaDexRoot> Delete(string id, string? token = null);

	/// <summary>
	/// Follows a manga or other object
	/// </summary>
	/// <param name="id">The ID of the object to follow</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Follow(string id, string? token = null);

	/// <summary>
	/// Unfollows a manga or other object
	/// </summary>
	/// <param name="id">The ID of the object to unfollow</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Unfollow(string id, string? token = null);

	/// <summary>
	/// Adds a manga to a custom list
	/// </summary>
	/// <param name="mangaId">The ID of the manga to add</param>
	/// <param name="listId">The ID of the custom list to add it to</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> MangaAdd(string mangaId, string listId, string? token = null);

	/// <summary>
	/// Removes a manga from a custom list
	/// </summary>
	/// <param name="mangaId">The ID of the manga to remove</param>
	/// <param name="listId">The ID of the custom list to remove it from</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> MangaRemove(string mangaId, string listId, string? token = null);

	/// <summary>
	/// Fetches a paginated list of custom lists
	/// </summary>
	/// <param name="limit">How many items to limit one request to</param>
	/// <param name="offset">How many items to skip when fetching</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of custom lists</returns>
	Task<CustomListList> List(int limit = 100, int offset = 0, string? token = null);

	/// <summary>
	/// Fetches a paginated list of custom lists owned by a specific user
	/// </summary>
	/// <param name="userId">The user ID of the person who owns the lists</param>
	/// <param name="limit">How many items to a limit one request to</param>
	/// <param name="offset">How many items to skip when fetching</param>
	/// <returns>A list of custom lists</returns>
	Task<CustomListList> List(string userId, int limit = 100, int offset = 0);
}

internal class MangaDexCustomListService : IMangaDexCustomListService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{_creds.ApiUrl}/list";

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

	public async Task<MangaDexRoot<CustomList>> Get(string id, bool includeManga = false)
	{
		var url = $"{Root}/{id}" + (includeManga ? "?includes[]=manga" : "");
		return await _api.Get<MangaDexRoot<CustomList>>(url) ?? new() { Result = "error" };
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
		return await _api.Post<MangaDexRoot, MangaDexEmpty>($"{_creds.ApiUrl}/manga/{mangaId}/list/{listId}", new MangaDexEmpty { }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> MangaRemove(string mangaId, string listId, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{_creds.ApiUrl}/manga/{mangaId}/list/{listId}", c) ?? new() { Result = "error" };
	}

	public async Task<CustomListList> List(int limit = 100, int offset = 0, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Get<CustomListList>($"{_creds.ApiUrl}/user/list?limit={limit}&offset={offset}", c) ?? new() { Result = "error" };
	}

	public async Task<CustomListList> List(string userId, int limit = 100, int offset = 0)
	{
		return await _api.Get<CustomListList>($"{_creds.ApiUrl}/user/{userId}/list?limit={limit}&offset={offset}") ?? new() { Result = "error" };
	}
}
