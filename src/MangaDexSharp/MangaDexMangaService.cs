namespace MangaDexSharp;

public interface IMangaDexMangaService
{
	Task<MangaList> List(MangaFilter? filter = null);
	Task<MangaDexRoot<Manga>> Create(MangaCreate manga, string? bearer = null);
	Task<MangaAggregate> Aggregate(string id, string[]? languages = null, string[]? groups = null);
	Task<MangaDexRoot<Manga>> Get(string id, MangaIncludes[]? includes = null);
	Task<MangaDexRoot<Manga>> Update(string id, MangaCreate manga, string? token = null);
	Task<MangaDexRoot> Delete(string id, string? token = null);
	Task<MangaDexRoot> Unfollow(string id, string? token = null);
	Task<MangaDexRoot> Follow(string id, string? token = null);
	Task<ChapterList> Feed(string id, MangaFeedFilter? filter = null);
	Task<MangaDexRoot<Manga>> Random(MangaRandomFilter? filter = null);
	Task<TagList> Tags();

	Task<MangaReadStatuses> Status(ReadStatus? status = null, string? token = null);
	Task<MangaReadStatuses> Status(string id, string? token = null);
	Task<MangaDexRoot> Status(string id, ReadStatus? status, string? token = null);

	Task<MangaDexRoot<Manga>> Draft(string id, MangaIncludes[]? includes = null, string? token = null);
	Task<MangaDexRoot<Manga>> DraftCommit(string id, int version = 1, string? token = null);
	Task<MangaList> Drafts(MangaDraftFilter? filter = null, string? token = null);

	Task<MangaRelationships> Relations(string id);
	Task<MangaDexRoot<MangaRelationship>> Relation(string id, Relationships relation, string target, string? token = null);
	Task<MangaDexRoot> RelationDelete(string mangaId, string id, string? token = null);
}

public class MangaDexMangaService : IMangaDexMangaService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{API_ROOT}/manga";

	public MangaDexMangaService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<MangaList> List(MangaFilter? filter = null)
	{
		filter ??= new();
		return await _api.Get<MangaList>($"{Root}?{filter.BuildQuery()}") ?? new();
	}

	public async Task<MangaDexRoot<Manga>> Create(MangaCreate manga, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<Manga>, MangaCreate>(Root, manga, c) ??
			new()
			{
				Result = "error",
				Response = "No return result"
			};
	}

	public async Task<MangaAggregate> Aggregate(string id, string[]? languages = null, string[]? groups = null)
	{
		languages ??= Array.Empty<string>();
		groups ??= Array.Empty<string>();
		var bob = new FilterBuilder()
			.Add("translatedLanguage", languages)
			.Add("groups", groups)
			.Build();
		return await _api.Get<MangaAggregate>($"{Root}/{id}/aggregate?{bob}") ?? new();
	}

	public async Task<MangaDexRoot<Manga>> Get(string id, MangaIncludes[]? includes = null)
	{
		includes ??= new[] { MangaIncludes.manga, MangaIncludes.cover_art, MangaIncludes.author, MangaIncludes.artist, MangaIncludes.tag };
		var bob = new FilterBuilder()
			.Add("includes", includes)
			.Build();
		return await _api.Get<MangaDexRoot<Manga>>($"{Root}/{id}?{bob}") ?? new();
	}

	public async Task<MangaDexRoot<Manga>> Update(string id, MangaCreate manga, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Put<MangaDexRoot<Manga>, MangaCreate>($"{Root}/{id}", manga, c) ??
			new()
			{
				Result = "error",
				Response = "No return result"
			};
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Unfollow(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}/follow", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Follow(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot, MangaDexEmpty>($"{Root}/{id}/follow", new MangaDexEmpty(), c) ?? new() { Result = "error" };
	}

	public async Task<ChapterList> Feed(string id, MangaFeedFilter? filter = null)
	{
		filter ??= new();
		return await _api.Get<ChapterList>($"{Root}/{id}/feed?{filter.BuildQuery()}") ?? new();
	}

	public async Task<MangaDexRoot<Manga>> Random(MangaRandomFilter? filter = null)
	{
		filter ??= new();
		return await _api.Get<MangaDexRoot<Manga>>($"{Root}/random?{filter.BuildQuery()}") ?? new();
	}

	public async Task<TagList> Tags()
	{
		return await _api.Get<TagList>($"{Root}/tag") ?? new();
	}

	public async Task<MangaReadStatuses> Status(ReadStatus? status = null, string? token = null)
	{
		var c = await Auth(token, _creds);

		var url = $"{Root}/status";
		if (status != null)
			url += $"?status={status}";

		return await _api.Get<MangaReadStatuses>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaReadStatuses> Status(string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Get<MangaReadStatuses>($"{Root}/{id}/status", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Status(string id, ReadStatus? status, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot, MangaReadStatusPush>($"{Root}/{id}/status", new MangaReadStatusPush
		{
			Status = status
		}, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Manga>> Draft(string id, MangaIncludes[]? includes = null, string? token = null)
	{
		includes ??= new[] { MangaIncludes.manga, MangaIncludes.cover_art, MangaIncludes.author, MangaIncludes.artist, MangaIncludes.tag };
		var c = await Auth(token, _creds);
		var bob = new FilterBuilder()
			.Add("includes", includes)
			.Build();
		return await _api.Get<MangaDexRoot<Manga>>($"{Root}/draft/{id}?{bob}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Manga>> DraftCommit(string id, int version = 1, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<Manga>, MangaCommit>($"{Root}/draft/{id}/commit", 
			new MangaCommit {  Version = version }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaList> Drafts(MangaDraftFilter? filter = null, string? token = null)
	{
		filter ??= new();
		var c = await Auth(token, _creds);
		return await _api.Get<MangaList>($"{Root}/draft?{filter.BuildQuery()}", c) ?? new();
	}

	public async Task<MangaRelationships> Relations(string id)
	{
		return await _api.Get<MangaRelationships>($"{Root}/{id}/relation?includes[]=manga") ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<MangaRelationship>> Relation(string id, Relationships relation, string target, string? token = null)
	{
		var c = await Auth(token, _creds);
		var rel = new MangaRelation
		{
			TargetManga = target,
			Relation = relation
		};
		return await _api.Post<MangaDexRoot<MangaRelationship>, MangaRelation>($"{Root}/{id}/relation", rel, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> RelationDelete(string mangaId, string id, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{mangaId}/relation/{id}", c) ?? new() { Result = "error" };
	}
}
