namespace MangaDexSharp;

public interface IMangaDexPageService
{
	Task<Pages> Pages(string chapterId);
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

	public string Root => $"{API_ROOT}/at-home/server/";

	public MangaDexMiscService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<Pages> Pages(string chapterId)
	{
		return await _api.Get<Pages>($"{Root}/{chapterId}?forcePort443=false") ?? new();
	}

	public async Task<MangaDexRoot> Captcha(string challenge, string? token = null)
	{
		var c = await Auth(token, _creds, true);
		return await _api.Post<MangaDexRoot, CaptchaChallenge>($"{API_ROOT}/captcha/solve", new CaptchaChallenge { Challenge = challenge }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Thread>> CreateThread(ThreadType type, string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		var thread = new ThreadCreate
		{
			Type = type,
			Id = id
		};
		return await _api.Post<MangaDexRoot<Thread>, ThreadCreate>($"{API_ROOT}/forums/thread", thread, c) ?? new() { Result = "error" };
	}

	public async Task<RatingList> Ratings(string[] mangaIds, string? token = null)
	{
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("manga", mangaIds)
			.Build();
		return await _api.Get<RatingList>($"{API_ROOT}/rating?{bob}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Rate(string mangaId, int rating, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{API_ROOT}/rating/{mangaId}";
		var data = new RatingCreate { Rating = rating };
		return await _api.Post<MangaDexRoot, RatingCreate>(url, data, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> RatingDelete(string mangaId, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{API_ROOT}/rating/{mangaId}";
		return await _api.Delete<MangaDexRoot>(url, c) ?? new() { Result = "error" };
	}
}
