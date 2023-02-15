namespace MangaDexSharp;

public interface IMangaDexFeedService
{
	Task<ChapterList> User(ChaptersFilter? filter = null, string? token = null);
	Task<ChapterList> List(string listId, ChaptersFilter? filter = null);
}

public class MangaDexFeedService : IMangaDexFeedService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public MangaDexFeedService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<ChapterList> User(ChaptersFilter? filter = null, string? token = null)
	{
		var c = await Auth(token, _creds);
		filter ??= new ChaptersFilter();
		var url = $"{_creds.ApiUrl}/user/follows/manga/feed?{filter.BuildQuery()}";
		return await _api.Get<ChapterList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<ChapterList> List(string listId, ChaptersFilter? filter = null)
	{
		filter ??= new ChaptersFilter();
		var url = $"{_creds.ApiUrl}/list/{listId}/feed?{filter.BuildQuery()}";
		return await _api.Get<ChapterList>(url) ?? new() { Result = "error" };
	}
}
