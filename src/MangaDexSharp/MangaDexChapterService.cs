namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests on the /chapter endpoints
/// </summary>
public interface IMangaDexChapterService
{
	/// <summary>
	/// Requests a paginated list of all chapters matching the given filter
	/// </summary>
	/// <param name="filter">How to filter the chapters</param>
	/// <returns>A list of chapters</returns>
	Task<ChapterList> List(ChaptersFilter? filter = null);

	/// <summary>
	/// Fetches a specific chapter by ID
	/// </summary>
	/// <param name="id">The ID of the chapter to fetch</param>
	/// <param name="includes">What relationship objects to include in the request (default: scanlation_group, manga, and user)</param>
	/// <returns>A chapter object</returns>
	Task<MangaDexRoot<Chapter>> Get(string id, string[]? includes = null);

	/// <summary>
	/// Updates a specific chapter
	/// </summary>
	/// <param name="id">The ID of the chapter to update</param>
	/// <param name="update">The updated chapter data</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The updated chapter object</returns>
	Task<MangaDexRoot<Chapter>> Update(string id, ChapterUpdate update, string? token = null);

	/// <summary>
	/// Deletes a specific chapter
	/// </summary>
	/// <param name="id">The ID of the chapter to delete</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Delete(string id, string? token = null);

	/// <summary>
	/// Requests all of the <see cref="Chapter"/>s that match the filter
	/// </summary>
	/// <param name="filter">How to filter the chapters</param>
	/// <param name="delay">How many milliseconds to wait after hitting the rate-cap</param>
	/// <param name="rateCap">How many requests to do before waiting</param>
	/// <returns>A list of all chapters</returns>
	IAsyncEnumerable<Chapter> ListAll(ChaptersFilter? filter = null, int? delay = null, int? rateCap = null);
}

internal class MangaDexChapterService : IMangaDexChapterService
{
	private readonly IMdApiService _api;

	public string Root => $"chapter";

	public MangaDexChapterService(IMdApiService api)
	{
		_api = api;
	}

	public async Task<ChapterList> List(ChaptersFilter? filter = null)
	{
		filter ??= new ChaptersFilter();
		var url = $"{Root}?{filter.BuildQuery()}";
		return await _api.Get<ChapterList>(url) ?? new() { Result = "error" };
	}

	public IAsyncEnumerable<Chapter> ListAll(ChaptersFilter? filter = null, int? delay = null, int? rateCap = null)
	{
		filter ??= new ChaptersFilter();
		var util = new PaginationUtility<ChapterList, Chapter, ChaptersFilter>(filter, (t, _) => List(t));
		if (delay != null) util.Delay = delay.Value;
		if (rateCap != null) util.Cap = rateCap.Value;
		return util.All();
	}

	public async Task<MangaDexRoot<Chapter>> Get(string id, string[]? includes = null)
	{
		includes ??= ["scanlation_group", "manga", "user"];
		var pars = string.Join("&", includes.Select(t => $"includes[]={t}"));
		var url = $"{Root}/{id}?{pars}";
		return await _api.Get<MangaDexRoot<Chapter>>(url) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Chapter>> Update(string id, ChapterUpdate update, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Put<MangaDexRoot<Chapter>, ChapterUpdate>($"{Root}/{id}", update, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}", c) ?? new() { Result = "error" };
	}
}
