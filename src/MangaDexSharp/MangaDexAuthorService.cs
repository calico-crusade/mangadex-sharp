namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests on the /author endpoints
/// </summary>
public interface IMangaDexAuthorService
{
	/// <summary>
	/// Requests a paginated list of all authors matching the given filter
	/// </summary>
	/// <param name="filter">How to filter the authors</param>
	/// <returns>A list of authors</returns>
	Task<PersonList> List(AuthorFilter? filter = null);

	/// <summary>
	/// Creates an author
	/// </summary>
	/// <param name="author">The author to create</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The instance of the author that was created</returns>
	Task<MangaDexRoot<PersonRelationship>> Create(AuthorCreate author, string? token = null);

	/// <summary>
	/// Fetches an author by ID
	/// </summary>
	/// <param name="id">The ID of the author</param>
	/// <returns>The author object</returns>
	Task<MangaDexRoot<PersonRelationship>> Get(string id);

	/// <summary>
	/// Updates an author by ID
	/// </summary>
	/// <param name="id">The ID of the author</param>
	/// <param name="author">The author object to update</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The author that was updated</returns>
	Task<MangaDexRoot<PersonRelationship>> Update(string id, AuthorCreate author, string? token = null);

	/// <summary>
	/// Deletes an author by ID
	/// </summary>
	/// <param name="id">The ID of the author</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Delete(string id, string? token = null);

	/// <summary>
	/// Requests all of the <see cref="PersonRelationship"/>s that match the filter
	/// </summary>
	/// <param name="filter">How to filter the authors</param>
	/// <param name="delay">How many milliseconds to wait after hitting the rate-cap</param>
	/// <param name="rateCap">How many requests to do before waiting</param>
	/// <returns>A list of all authors</returns>
	IAsyncEnumerable<PersonRelationship> ListAll(AuthorFilter? filter = null, int? delay = null, int? rateCap = null);
}

internal class MangaDexAuthorService : IMangaDexAuthorService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{_creds.ApiUrl}/author";

	public MangaDexAuthorService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<PersonList> List(AuthorFilter? filter = null)
	{
		filter ??= new();
		return await _api.Get<PersonList>($"{Root}?{filter.BuildQuery()}") ?? new() { Result = "error" };
	}

	public IAsyncEnumerable<PersonRelationship> ListAll(AuthorFilter? filter = null, int? delay = null, int? rateCap = null)
	{
		filter ??= new AuthorFilter();
		var util = new PaginationUtility<PersonList, PersonRelationship, AuthorFilter>(filter, (t, _) => List(t));
		if (delay != null) util.Delay = delay.Value;
		if (rateCap != null) util.Cap = rateCap.Value;
		return util.All();
	}

	public async Task<MangaDexRoot<PersonRelationship>> Create(AuthorCreate author, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<PersonRelationship>, AuthorCreate>(Root, author, c) ?? new() {  Result = "error" };
	}

	public async Task<MangaDexRoot<PersonRelationship>> Get(string id)
	{
		return await _api.Get<MangaDexRoot<PersonRelationship>>($"{Root}/{id}?includes[]=manga") ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<PersonRelationship>> Update(string id, AuthorCreate author, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Put<MangaDexRoot<PersonRelationship>, AuthorCreate>($"{Root}/{id}", author, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}", c) ?? new() { Result = "error" };
	}
}
