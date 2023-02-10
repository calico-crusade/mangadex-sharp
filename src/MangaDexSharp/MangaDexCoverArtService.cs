namespace MangaDexSharp;

public interface IMangaDexCoverArtService
{
	Task<CoverArtList> List(CoverArtFilter? filter = null);

	Task<MangaDexRoot<CoverArtRelationship>> Upload(string mangaOrCoverId, CoverArtCreate cover, string? token = null);

	Task<MangaDexRoot<CoverArtRelationship>> Get(string mangaOrCoverId);

	Task<MangaDexRoot<CoverArtRelationship>> Update(string mangaOrCoverId, CoverArtUpdate cover, string? token = null);

	Task<MangaDexRoot> Delete(string mangaOrCoverId, string? token = null);
}

public class MangaDexCoverArtService : IMangaDexCoverArtService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{API_ROOT}/cover";

	public MangaDexCoverArtService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<CoverArtList> List(CoverArtFilter? filter = null)
	{
		filter ??= new();
		return await _api.Get<CoverArtList>($"{Root}?{filter.BuildQuery()}") ?? new();
	}

	public async Task<MangaDexRoot<CoverArtRelationship>> Upload(string mangaOrCoverId, CoverArtCreate cover, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<CoverArtRelationship>, CoverArtCreate>($"{Root}/{mangaOrCoverId}", cover, c) ??
			new() { Result = "error" };
	}

	public async Task<MangaDexRoot<CoverArtRelationship>> Get(string mangaOrCoverId)
	{
		return await _api.Get<MangaDexRoot<CoverArtRelationship>>($"{Root}/{mangaOrCoverId}?includes[]=manga&includes[]=user") ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<CoverArtRelationship>> Update(string mangaOrCoverId, CoverArtUpdate cover, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Put<MangaDexRoot<CoverArtRelationship>, CoverArtUpdate>($"{Root}/{mangaOrCoverId}", cover, c) ??
			new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string mangaOrCoverId, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{mangaOrCoverId}", c) ?? new() { Result = "error" };
	}
}
