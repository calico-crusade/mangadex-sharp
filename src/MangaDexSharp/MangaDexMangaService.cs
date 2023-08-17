namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests related to manga
/// </summary>
public interface IMangaDexMangaService
{
	/// <summary>
	/// Requests a paginated list of manga matching the given filter
	/// </summary>
	/// <param name="filter">How to filter the manga</param>
	/// <returns>A list of manga</returns>
	Task<MangaList> List(MangaFilter? filter = null);

	/// <summary>
	/// Creates a manga object
	/// </summary>
	/// <param name="manga">The manga to create</param>
	/// <param name="bearer">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The created manga object</returns>
	Task<MangaDexRoot<Manga>> Create(MangaCreate manga, string? bearer = null);

	/// <summary>
	/// Requests a layout of volumes and chapters for a specific manga
	/// </summary>
	/// <param name="id">The ID of the manga to fetch for</param>
	/// <param name="languages">The optional list of languages to limit the results to</param>
	/// <param name="groups">The optional list of groups to limit the results to</param>
	/// <returns>A list of volume and chapter layouts</returns>
	Task<MangaAggregate> Aggregate(string id, string[]? languages = null, string[]? groups = null);

	/// <summary>
	/// Gets a specific manga by ID
	/// </summary>
	/// <param name="id">The ID of the manga</param>
	/// <param name="includes">What relationship data to include in the results</param>
	/// <returns>The manga object</returns>
	Task<MangaDexRoot<Manga>> Get(string id, MangaIncludes[]? includes = null);

	/// <summary>
	/// Updates a specific manga
	/// </summary>
	/// <param name="id">The ID of the manga to update</param>
	/// <param name="manga">The manga data</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The manga object that was updated</returns>
	Task<MangaDexRoot<Manga>> Update(string id, MangaCreate manga, string? token = null);

	/// <summary>
	/// Deletes a specific manga
	/// </summary>
	/// <param name="id">The ID of the manga to delete</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Delete(string id, string? token = null);

	/// <summary>
	/// Requests that the current user unfollow the given manga ID
	/// </summary>
	/// <param name="id">The ID of the manga to unfollow</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Unfollow(string id, string? token = null);

	/// <summary>
	/// Requests that the current user follows the given manga ID
	/// </summary>
	/// <param name="id">The ID of the manga to follow</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Follow(string id, string? token = null);

	/// <summary>
	/// Requestions a paginated list of all of filtered chapters for the given manga 
	/// </summary>
	/// <param name="id">The ID of the manga</param>
	/// <param name="filter">How to filter the chapters</param>
	/// <returns>A list of chapters</returns>
	Task<ChapterList> Feed(string id, MangaFeedFilter? filter = null);

	/// <summary>
	/// Fetches a random manga based on the filters
	/// </summary>
	/// <param name="filter">How to filter the random manga</param>
	/// <returns>A random manga</returns>
	Task<MangaDexRoot<Manga>> Random(MangaRandomFilter? filter = null);
	
	/// <summary>
	/// Fetchs a list of all of the manga tags. This should be cached within your application as it hardly changes.
	/// </summary>
	/// <returns>All of the manga tags</returns>
	Task<TagList> Tags();

	/// <summary>
	/// Returns a collection of the current users read chapters based on the given status
	/// </summary>
	/// <param name="status">Filters the collection based on this status</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A collection of read manga chapters</returns>
	Task<MangaReadStatuses> Status(ReadStatus? status = null, string? token = null);

	/// <summary>
	/// Returns a collection of the current users read chapters for the given manga
	/// </summary>
	/// <param name="id">The ID of the manga</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A collection of read manga chapters</returns>
	Task<MangaReadStatuses> Status(string id, string? token = null);

	/// <summary>
	/// Sets the read status of a specific chapter
	/// </summary>
	/// <param name="id">The ID of the chapter</param>
	/// <param name="status">The read status of the chapter</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Status(string id, ReadStatus? status, string? token = null);

	/// <summary>
	/// Fetches the draft of a manga by ID
	/// </summary>
	/// <param name="id">The ID of the draft</param>
	/// <param name="includes">What relationship data to include in the results</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The manga draft</returns>
	Task<MangaDexRoot<Manga>> Draft(string id, MangaIncludes[]? includes = null, string? token = null);

	/// <summary>
	/// Commits a manga draft
	/// </summary>
	/// <param name="id">The ID of the draft to commit</param>
	/// <param name="version">The version of the request</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The committed draft of the manga</returns>
	Task<MangaDexRoot<Manga>> DraftCommit(string id, int version = 1, string? token = null);

	/// <summary>
	/// Requests a paginated list of drafts for the current user
	/// </summary>
	/// <param name="filter">How to filter the drafts</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of manga drafts</returns>
	Task<MangaList> Drafts(MangaDraftFilter? filter = null, string? token = null);

	/// <summary>
	/// Fetches a list of all of the relationships the given manga has
	/// </summary>
	/// <param name="id">The ID of the manga</param>
	/// <returns>A lsit of relationships</returns>
	Task<MangaRelationships> Relations(string id);

	/// <summary>
	/// Sets the relationship between the given manga and the target manga
	/// </summary>
	/// <param name="id">The ID of the manga to add the relationship to</param>
	/// <param name="relation">The type of relationship with the target</param>
	/// <param name="target">The ID of the related manga</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The created relationship</returns>
	Task<MangaDexRoot<MangaRelationship>> Relation(string id, Relationships relation, string target, string? token = null);

	/// <summary>
	/// Deletes the relationship between the given manga and the target manga
	/// </summary>
	/// <param name="mangaId">The ID of the manga to delete the relationship from</param>
	/// <param name="id">The ID of the target manga</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> RelationDelete(string mangaId, string id, string? token = null);

	/// <summary>
	/// Requests all of the <see cref="Manga"/>s that match the filter
	/// </summary>
	/// <param name="filter">How to filter the Manga</param>
	/// <param name="delay">How many milliseconds to wait after hitting the rate-cap</param>
	/// <param name="rateCap">How many requests to do before waiting</param>
	/// <returns>A list of all Manga</returns>
	IAsyncEnumerable<Manga> ListAll(MangaFilter? filter = null, int? delay = null, int? rateCap = null);

	/// <summary>
	/// Requests all of the chapters in the current feed
	/// </summary>
	/// <param name="id">The ID of the manga</param>
	/// <param name="filter">How to filter the feed</param>
	/// <param name="delay">How many milliseconds to wait after hitting the rate-cap</param>
	/// <param name="rateCap">How many requests to do before waiting</param>
	/// <returns>A list of all of the chapters in the feed</returns>
	IAsyncEnumerable<Chapter> FeedAll(string id, MangaFeedFilter? filter = null, int? delay = null, int? rateCap = null);
}

internal class MangaDexMangaService : IMangaDexMangaService
{
	private readonly IMdApiService _api;

	public string Root => $"manga";

	public MangaDexMangaService(IMdApiService api)
	{
		_api = api;
	}

	public async Task<MangaList> List(MangaFilter? filter = null)
	{
		filter ??= new();
		return await _api.Get<MangaList>($"{Root}?{filter.BuildQuery()}") ?? new();
	}

	public IAsyncEnumerable<Manga> ListAll(MangaFilter? filter = null, int? delay = null, int? rateCap = null)
	{
		filter ??= new MangaFilter();
		var util = new PaginationUtility<MangaList, Manga, MangaFilter>(filter, (t, _) => List(t));
		if (delay != null) util.Delay = delay.Value;
		if (rateCap != null) util.Cap = rateCap.Value;
		return util.All();
	}

	public IAsyncEnumerable<Chapter> FeedAll(string id, MangaFeedFilter? filter = null, int? delay = null, int? rateCap = null)
	{
		filter ??= new MangaFeedFilter();
		var util = new PaginationUtility<ChapterList, Chapter, MangaFeedFilter>(filter, (t, _) => Feed(id, t));
		if (delay != null) util.Delay = delay.Value;
		if (rateCap != null) util.Cap = rateCap.Value;
		return util.All();
	}

	public async Task<MangaDexRoot<Manga>> Create(MangaCreate manga, string? token = null)
	{
		var c = await _api.Auth(token);
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
		var c = await _api.Auth(token);
		return await _api.Put<MangaDexRoot<Manga>, MangaCreate>($"{Root}/{id}", manga, c) ??
			new()
			{
				Result = "error",
				Response = "No return result"
			};
	}

	public async Task<MangaDexRoot> Delete(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Unfollow(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Delete<MangaDexRoot>($"{Root}/{id}/follow", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Follow(string id, string? token = null)
	{
		var c = await _api.Auth(token);
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
		var c = await _api.Auth(token);

		var url = $"{Root}/status";
		if (status != null)
			url += $"?status={status}";

		return await _api.Get<MangaReadStatuses>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaReadStatuses> Status(string id, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Get<MangaReadStatuses>($"{Root}/{id}/status", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Status(string id, ReadStatus? status, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<MangaDexRoot, MangaReadStatusPush>($"{Root}/{id}/status", new MangaReadStatusPush
		{
			Status = status
		}, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Manga>> Draft(string id, MangaIncludes[]? includes = null, string? token = null)
	{
		includes ??= new[] { MangaIncludes.manga, MangaIncludes.cover_art, MangaIncludes.author, MangaIncludes.artist, MangaIncludes.tag };
		var c = await _api.Auth(token);
		var bob = new FilterBuilder()
			.Add("includes", includes)
			.Build();
		return await _api.Get<MangaDexRoot<Manga>>($"{Root}/draft/{id}?{bob}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Manga>> DraftCommit(string id, int version = 1, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<MangaDexRoot<Manga>, MangaCommit>($"{Root}/draft/{id}/commit", 
			new MangaCommit {  Version = version }, c) ?? new() { Result = "error" };
	}

	public async Task<MangaList> Drafts(MangaDraftFilter? filter = null, string? token = null)
	{
		filter ??= new();
		var c = await _api.Auth(token);
		return await _api.Get<MangaList>($"{Root}/draft?{filter.BuildQuery()}", c) ?? new();
	}

	public async Task<MangaRelationships> Relations(string id)
	{
		return await _api.Get<MangaRelationships>($"{Root}/{id}/relation?includes[]=manga") ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<MangaRelationship>> Relation(string id, Relationships relation, string target, string? token = null)
	{
		var c = await _api.Auth(token);
		var rel = new MangaRelation
		{
			TargetManga = target,
			Relation = relation
		};
		return await _api.Post<MangaDexRoot<MangaRelationship>, MangaRelation>($"{Root}/{id}/relation", rel, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> RelationDelete(string mangaId, string id, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Delete<MangaDexRoot>($"{Root}/{mangaId}/relation/{id}", c) ?? new() { Result = "error" };
	}
}
