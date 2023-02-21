namespace MangaDexSharp;

public interface IMangaDexPageService
{
	Task<Pages> Pages(string chapterId);
	Task ReportPage(PageReport report);
}

public interface IMangaDexRatingService
{
	Task<RatingList> Ratings(string[] mangaIds, string? token = null);
	Task<MangaDexRoot> Rate(string mangaId, int rating, string? token = null);
	Task<MangaDexRoot> RatingDelete(string mangaId, string? token = null);
}

public interface IMangaDexThreadsService
{
	Task<MangaDexRoot<Thread>> CreateThread(ThreadType type, string id, string? token = null);
}

public interface IMangaDexCaptchaService
{
	Task<MangaDexRoot> Captcha(string challenge, string? token = null);
}

public interface IMangaDexMiscService : 
	IMangaDexPageService, IMangaDexRatingService, 
	IMangaDexThreadsService, IMangaDexCaptchaService { }

public class MangaDexMiscService : IMangaDexMiscService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => _creds.ApiUrl;

	public MangaDexMiscService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public Task ReportPage(PageReport report)
	{
		return _api.Post<MangaDexEmpty, PageReport>("https://api.mangadex.network/report", report);
	}

	public async Task<Pages> Pages(string chapterId)
	{
		return await _api.Get<Pages>($"{Root}/at-home/server/{chapterId}?forcePort443=false") ?? new();
	}

	public async Task<MangaDexRoot> Captcha(string challenge, string? token = null)
	{
		var c = await Auth(token, _creds, true);
		return await _api.Post<MangaDexRoot, CaptchaChallenge>($"{Root}/captcha/solve", new CaptchaChallenge { Challenge = challenge }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Thread>> CreateThread(ThreadType type, string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		var thread = new ThreadCreate
		{
			Type = type,
			Id = id
		};
		return await _api.Post<MangaDexRoot<Thread>, ThreadCreate>($"{Root}/forums/thread", thread, c) ?? new() { Result = "error" };
	}

	public async Task<RatingList> Ratings(string[] mangaIds, string? token = null)
	{
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("manga", mangaIds)
			.Build();
		return await _api.Get<RatingList>($"{Root}/rating?{bob}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Rate(string mangaId, int rating, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/rating/{mangaId}";
		var data = new RatingCreate { Rating = rating };
		return await _api.Post<MangaDexRoot, RatingCreate>(url, data, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> RatingDelete(string mangaId, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}/rating/{mangaId}";
		return await _api.Delete<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}
}
