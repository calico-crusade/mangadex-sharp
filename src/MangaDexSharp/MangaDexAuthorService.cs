namespace MangaDexSharp;

public interface IMangaDexAuthorService
{
	Task<PersonList> List(AuthorFilter? filter = null);
	Task<MangaDexRoot<PersonRelationship>> Create(AuthorCreate author, string? token = null);
	Task<MangaDexRoot<PersonRelationship>> Get(string id);
	Task<MangaDexRoot<PersonRelationship>> Update(string id, AuthorCreate author, string? token = null);
	Task<MangaDexRoot> Delete(string id, string? token = null);
}

public class MangaDexAuthorService : IMangaDexAuthorService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{API_ROOT}/author";

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
