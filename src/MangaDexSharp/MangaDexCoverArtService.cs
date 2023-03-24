namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests on the /cover endpoints
/// </summary>
public interface IMangaDexCoverArtService
{
	/// <summary>
	/// Requests a paginated list of all cover art matching the given filter
	/// </summary>
	/// <param name="filter">How to filter the cover art</param>
	/// <returns>A list of cover arts</returns>
	Task<CoverArtList> List(CoverArtFilter? filter = null);

	/// <summary>
	/// Uploads a cover art object
	/// </summary>
	/// <param name="mangaOrCoverId">The manga or cover ID</param>
	/// <param name="cover">The cover object to upload</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The uploaded cover art object</returns>
	Task<MangaDexRoot<CoverArtRelationship>> Upload(string mangaOrCoverId, CoverArtCreate cover, string? token = null);

	/// <summary>
	/// Fetches cover art by manga ID or cover art ID
	/// </summary>
	/// <param name="mangaOrCoverId">The manga or cover art ID</param>
	/// <returns>The cover art object</returns>
	Task<MangaDexRoot<CoverArtRelationship>> Get(string mangaOrCoverId);

	/// <summary>
	/// Updates a cover art object
	/// </summary>
	/// <param name="mangaOrCoverId">The manga or cover art ID</param>
	/// <param name="cover">The cover object to update</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The updated cover art object</returns>
	Task<MangaDexRoot<CoverArtRelationship>> Update(string mangaOrCoverId, CoverArtUpdate cover, string? token = null);

	/// <summary>
	/// Deletes a cover art object
	/// </summary>
	/// <param name="mangaOrCoverId">The manga or cover art ID</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Delete(string mangaOrCoverId, string? token = null);

	/// <summary>
	/// Requests all of the <see cref="CoverArtRelationship"/>s that match the filter
	/// </summary>
	/// <param name="filter">How to filter the cover arts</param>
	/// <param name="delay">How many milliseconds to wait after hitting the rate-cap</param>
	/// <param name="rateCap">How many requests to do before waiting</param>
	/// <returns>A list of all cover arts</returns>
	IAsyncEnumerable<CoverArtRelationship> ListAll(CoverArtFilter? filter = null, int? delay = null, int? rateCap = null);
}

internal class MangaDexCoverArtService : IMangaDexCoverArtService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{_creds.ApiUrl}/cover";

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

	public IAsyncEnumerable<CoverArtRelationship> ListAll(CoverArtFilter? filter = null, int? delay = null, int? rateCap = null)
	{
		filter ??= new CoverArtFilter();
		var util = new PaginationUtility<CoverArtList, CoverArtRelationship, CoverArtFilter>(filter, (t, _) => List(t));
		if (delay != null) util.Delay = delay.Value;
		if (rateCap != null) util.Cap = rateCap.Value;
		return util.All();
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
