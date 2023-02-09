namespace MangaDexSharp;

public interface IMangaDexPagesService
{
	Task<Pages> Pages(string chapterId);
}

public class MangaDexPagesService : IMangaDexPagesService
{
	private readonly IApiService _api;

	public string Root => $"{API_ROOT}/at-home/server/";
	
	public MangaDexPagesService(IApiService api)
	{
		_api = api;
	}

	public async Task<Pages> Pages(string chapterId)
	{
		return await _api.Get<Pages>($"{Root}/{chapterId}?forcePort443=false") ?? new();
	}

}
