namespace MangaDexSharp;

public interface IMangaDexChapterService
{
	Task<ChapterList> List(ChaptersFilter? filter = null);
	Task<MangaDexRoot<Chapter>> Get(string id, string[]? includes = null);
	Task<MangaDexRoot<Chapter>> Update(string id, ChapterUpdate update, string? token = null);
	Task<MangaDexRoot> Delete(string id, string? token = null);
}

public class MangaDexChapterService : IMangaDexChapterService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{API_ROOT}/chapter";

	public MangaDexChapterService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<ChapterList> List(ChaptersFilter? filter = null)
	{
		filter ??= new ChaptersFilter();
		var url = $"{Root}?{filter.BuildQuery()}";
		return await _api.Get<ChapterList>(url) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Chapter>> Get(string id, string[]? includes = null)
	{
		includes ??= new[] { "scanlation_group", "manga", "user" };
		var pars = string.Join("&", includes.Select(t => $"includes[]={t}"));
		var url = $"{Root}/{id}?{pars}";
		return await _api.Get<MangaDexRoot<Chapter>>(url) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Chapter>> Update(string id, ChapterUpdate update, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Put<MangaDexRoot<Chapter>, ChapterUpdate>($"{Root}/{id}", update, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}", c) ?? new() { Result = "error" };
	}
}
