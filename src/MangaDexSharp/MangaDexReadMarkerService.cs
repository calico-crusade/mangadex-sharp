namespace MangaDexSharp;

public interface IMangaDexReadMarkerService
{
	Task<ReadMarkerList> Read(string mangaId, string? token = null);
	Task<ReadMarkerList> Read(string[] mangaIds, bool grouped = true, string? token = null);

	Task<MangaDexRoot> BatchUpdate(string mangaId, string[] chapterIds, bool read, bool updateHistory = true, string? token = null);
	Task<MangaDexRoot> BatchUpdate(string mangaId, ReadMarkerBatchUpdate update, bool updateHistory = true, string? token = null);
}

public class MangaDexReadMarkerService : IMangaDexReadMarkerService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => _creds.ApiUrl;

	public MangaDexReadMarkerService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<ReadMarkerList> Read(string mangaId, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Get<ReadMarkerList>($"{Root}/manga/{mangaId}/read", c) ?? new() { Result = "error" };
	}

	public async Task<ReadMarkerList> Read(string[] mangaIds, bool grouped = true, string? token = null)
	{
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("ids", mangaIds)
			.Add("grouped", grouped)
			.Build();
		var url = $"{Root}/manga/read?{bob}";
		return await _api.Get<ReadMarkerList>(url, c) ?? new() { Result = "error" };
	}

	public Task<MangaDexRoot> BatchUpdate(string mangaId, string[] chapterIds, bool read, bool updateHistory = true, string? token = null)
	{
		var d = new ReadMarkerBatchUpdate
		{
			ChapterIdsRead = read ? chapterIds : Array.Empty<string>(),
			ChapterIdsUnread = read ? Array.Empty<string>() : chapterIds
		};
		return BatchUpdate(mangaId, d, updateHistory, token);
	}

	public async Task<MangaDexRoot> BatchUpdate(string mangaId, ReadMarkerBatchUpdate update, bool updateHistory = true, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/manga/{mangaId}/read?updateHistory={updateHistory}";
		return await _api.Post<MangaDexRoot, ReadMarkerBatchUpdate>(url, update, c) ?? new() { Result = "error" };
	}
}
