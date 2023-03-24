namespace MangaDexSharp;

/// <summary>
/// Represents all of the different feed endpoints (expect manga, that's on the <see cref="IMangaDex.Manga"/> service)
/// </summary>
public interface IMangaDexFeedService
{
	/// <summary>
	/// Requests a paginated list of chapters the current user follows
	/// </summary>
	/// <param name="filter">How to filter the chapters</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of chapters</returns>
	Task<ChapterList> User(ChaptersFilter? filter = null, string? token = null);

	/// <summary>
	/// Requests a paginated list of chapters in the given list
	/// </summary>
	/// <param name="listId">The ID of the list to use</param>
	/// <param name="filter">How to filter the chapters</param>
	/// <returns>A list of chapters</returns>
	Task<ChapterList> List(string listId, ChaptersFilter? filter = null);
}

internal class MangaDexFeedService : IMangaDexFeedService
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
