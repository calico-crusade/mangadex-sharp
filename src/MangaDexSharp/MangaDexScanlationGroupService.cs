namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests related to scanlation groups
/// </summary>
public interface IMangaDexScanlationGroupService
{
	/// <summary>
	/// Requests a paginated list of scanlation groups
	/// </summary>
	/// <param name="filter">How to filter the groups</param>
	/// <returns>A list of scanlation groups</returns>
	Task<ScanlationGroupList> List(ScanlationGroupFilter? filter = null);

	/// <summary>
	/// Creates a scanlation group
	/// </summary>
	/// <param name="group">The group to create</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The created scanlation group</returns>
	Task<MangaDexRoot<ScanlationGroup>> Create(ScanlationGroupCreate group, string? token = null);

	/// <summary>
	/// Fetches a scanlation group by ID
	/// </summary>
	/// <param name="id">The ID of the group</param>
	/// <returns>The scanlation group</returns>
	Task<MangaDexRoot<ScanlationGroup>> Get(string id);

	/// <summary>
	/// Updates a scanlation group
	/// </summary>
	/// <param name="id">The ID of the group</param>
	/// <param name="group">The updates</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The created scanlation group</returns>
	Task<MangaDexRoot<ScanlationGroup>> Update(string id, ScanlationGroupUpdate group, string? token = null);

	/// <summary>
	/// Deletes a scanlation group
	/// </summary>
	/// <param name="id">The ID of the group to delete</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Delete(string id, string? token = null);

	/// <summary>
	/// Requests that the current user follows the group
	/// </summary>
	/// <param name="id">The ID of the group to follow</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Follow(string id, string? token = null);

	/// <summary>
	/// Requests that the current user unfollows the group
	/// </summary>
	/// <param name="id">The ID of the group to unfollow</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Unfollow(string id, string? token = null);
}

internal class MangaDexScanlationGroupService : IMangaDexScanlationGroupService
{
	private readonly IMdApiService _api;

	public string Root => $"group";

	public MangaDexScanlationGroupService(IMdApiService api)
	{
		_api = api;
	}

	public async Task<ScanlationGroupList> List(ScanlationGroupFilter? filter = null)
	{
		var bob = (filter ?? new()).BuildQuery();
		var url = $"{Root}?{bob}";
		return await _api.Get<ScanlationGroupList>(url) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<ScanlationGroup>> Create(ScanlationGroupCreate group, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<MangaDexRoot<ScanlationGroup>, ScanlationGroupCreate>(Root, group, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<ScanlationGroup>> Get(string id)
	{
		var url = $"{Root}/{id}?includes[]=leader&includes[]=member";
		return await _api.Get<MangaDexRoot<ScanlationGroup>>(url) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<ScanlationGroup>> Update(string id, ScanlationGroupUpdate group, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"{Root}/{id}";
		return await _api.Put<MangaDexRoot<ScanlationGroup>, ScanlationGroupUpdate>(url, group, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"{Root}/{id}";
		return await _api.Delete<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Follow(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"{Root}/{id}/follow";
		return await _api.Post<MangaDexRoot, MangaDexEmpty>(url, new MangaDexEmpty(), c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Unfollow(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"{Root}/{id}/follow";
		return await _api.Delete<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}
}
