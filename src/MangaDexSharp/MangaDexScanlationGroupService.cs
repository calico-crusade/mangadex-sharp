namespace MangaDexSharp;

public interface IMangaDexScanlationGroupService
{
	Task<ScanlationGroupList> List(ScanlationGroupFilter? filter = null);

	Task<MangaDexRoot<ScanlationGroup>> Create(ScanlationGroupCreate group, string? token = null);

	Task<MangaDexRoot<ScanlationGroup>> Get(string id);

	Task<MangaDexRoot<ScanlationGroup>> Update(string id, ScanlationGroupUpdate group, string? token = null);

	Task<MangaDexRoot> Delete(string id, string? token = null);

	Task<MangaDexRoot> Follow(string id, string? token = null);

	Task<MangaDexRoot> Unfollow(string id, string? token = null);
}

public class MangaDexScanlationGroupService : IMangaDexScanlationGroupService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{_creds.ApiUrl}/group";

	public MangaDexScanlationGroupService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<ScanlationGroupList> List(ScanlationGroupFilter? filter = null)
	{
		var bob = (filter ?? new()).BuildQuery();
		var url = $"{Root}?{bob}";
		return await _api.Get<ScanlationGroupList>(url) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<ScanlationGroup>> Create(ScanlationGroupCreate group, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<ScanlationGroup>, ScanlationGroupCreate>(Root, group, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<ScanlationGroup>> Get(string id)
	{
		var url = $"{Root}/{id}?includes[]=leader&includes[]=member";
		return await _api.Get<MangaDexRoot<ScanlationGroup>>(url) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<ScanlationGroup>> Update(string id, ScanlationGroupUpdate group, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/{id}";
		return await _api.Put<MangaDexRoot<ScanlationGroup>, ScanlationGroupUpdate>(url, group, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/{id}";
		return await _api.Delete<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Follow(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/{id}/follow";
		return await _api.Post<MangaDexRoot, MangaDexEmpty>(url, new MangaDexEmpty(), c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Unfollow(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/{id}/follow";
		return await _api.Delete<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}
}
