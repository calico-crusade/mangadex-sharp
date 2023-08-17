namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests for pages and md-at-home
/// </summary>
public interface IMangaDexPageService
{
	/// <summary>
	/// Gets all of the page images for the given chapter
	/// </summary>
	/// <param name="chapterId">The ID of the chapter</param>
	/// <returns>All of the page images for the given chapter</returns>
	Task<Pages> Pages(string chapterId);

	/// <summary>
	/// Reports a page for metrics
	/// </summary>
	/// <param name="report">The report to make</param>
	/// <returns></returns>
	Task ReportPage(PageReport report);
}

/// <summary>
/// Represents all of the requests relating to rating manga
/// </summary>
public interface IMangaDexRatingService
{
	/// <summary>
	/// Requests a list of all of the current users ratings for the given manga
	/// </summary>
	/// <param name="mangaIds">The manga IDs</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of ratings</returns>
	Task<RatingList> Ratings(string[] mangaIds, string? token = null);

	/// <summary>
	/// Rates the given manga for the current user
	/// </summary>
	/// <param name="mangaId">The ID of the manga to rate</param>
	/// <param name="rating">The rating to apply</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Rate(string mangaId, int rating, string? token = null);

	/// <summary>
	/// Deletes a rating for the current user
	/// </summary>
	/// <param name="mangaId">The ID of the manga to delete the rating for</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> RatingDelete(string mangaId, string? token = null);
}

/// <summary>
/// Represents all of the requests for threads
/// </summary>
public interface IMangaDexThreadsService
{
	/// <summary>
	/// Creates a thread
	/// </summary>
	/// <param name="type">The type of thread to create</param>
	/// <param name="id">The ID of the object to create the thread for</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The created thread</returns>
	Task<MangaDexRoot<Thread>> CreateThread(ThreadType type, string id, string? token = null);
}

/// <summary>
/// Represents all of the requests related to captchas
/// </summary>
public interface IMangaDexCaptchaService
{
	/// <summary>
	/// Solves a captcha challenge
	/// </summary>
	/// <param name="challenge">The result of the captcha</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The result of the requests</returns>
	Task<MangaDexRoot> Captcha(string challenge, string? token = null);
}

/// <summary>
/// Represents a collection of miscellaneous services
/// </summary>
public interface IMangaDexMiscService : 
	IMangaDexPageService, IMangaDexRatingService, 
	IMangaDexThreadsService, IMangaDexCaptchaService { }

internal class MangaDexMiscService : IMangaDexMiscService
{
	private readonly IMdApiService _api;

	public MangaDexMiscService(IMdApiService api)
	{
		_api = api;
	}

	public Task ReportPage(PageReport report)
	{
		return _api.Post<MangaDexEmpty, PageReport>("https://api.mangadex.network/report", report);
	}

	public async Task<Pages> Pages(string chapterId)
	{
		return await _api.Get<Pages>($"at-home/server/{chapterId}?forcePort443=false") ?? new();
	}

	public async Task<MangaDexRoot> Captcha(string challenge, string? token = null)
	{
		var c = await _api.Auth(token, true);
		return await _api.Post<MangaDexRoot, CaptchaChallenge>($"captcha/solve", new CaptchaChallenge { Challenge = challenge }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Thread>> CreateThread(ThreadType type, string id, string? token = null)
	{
		var c = await _api.Auth(token);
		var thread = new ThreadCreate
		{
			Type = type,
			Id = id
		};
		return await _api.Post<MangaDexRoot<Thread>, ThreadCreate>($"forums/thread", thread, c) ?? new() { Result = "error" };
	}

	public async Task<RatingList> Ratings(string[] mangaIds, string? token = null)
	{
		var c = await _api.Auth(token);
		var bob = new FilterBuilder()
			.Add("manga", mangaIds)
			.Build();
		return await _api.Get<RatingList>($"rating?{bob}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Rate(string mangaId, int rating, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"rating/{mangaId}";
		var data = new RatingCreate { Rating = rating };
		return await _api.Post<MangaDexRoot, RatingCreate>(url, data, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> RatingDelete(string mangaId, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"rating/{mangaId}";
		return await _api.Delete<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}
}
