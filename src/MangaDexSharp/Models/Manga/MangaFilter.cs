namespace MangaDexSharp;

public class MangaFilter : IFilter
{
	public string Title { get; set; } = string.Empty;

	public int Limit { get; set; } = 100;

	public int Offset { get; set; } = 0;

	public string AuthorOrArtist { get; set; } = string.Empty;

	public ContentRating[] ContentRating { get; set; } = ContentRatingsAll;

	public MangaIncludes[] Includes { get; set; } = new[] { MangaIncludes.cover_art };

	public string[] Authors { get; set; } = Array.Empty<string>();

	public string[] Artists { get; set; } = Array.Empty<string>();

	public int? Year { get; set; }

	public string[] IncludedTags { get; set; } = Array.Empty<string>();

	public Mode? IncludeTagsMode { get; set; }

	public string[] ExcludedTags { get; set; } = Array.Empty<string>();

	public Mode? ExcludedTagsMode { get; set; }

	public Status[] Status { get; set; } = Array.Empty<Status>();

	public string[] OriginalLanguage { get; set; } = Array.Empty<string>();

	public string[] ExcludedOriginalLanguage { get; set; } = Array.Empty<string>();

	public string[] AvailableTranslatedLanguage { get; set; } = Array.Empty<string>();

	public Demographic[] PublicationDemographic { get; set; } = Array.Empty<Demographic>();

	public string[] Ids { get; set; } = Array.Empty<string>();

	public DateTime? CreatedAtSince { get; set; }

	public DateTime? UpdatedAtSince { get; set; }

	public bool? HasAvailableChapters { get; set; }

	public string Group { get; set; } = string.Empty;

	public Dictionary<OrderKey, OrderValue> Order { get; set; } = new();

	public string BuildQuery()
	{
		return new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("title", Title)
			.Add("authorOrArtist", AuthorOrArtist)
			.Add("authors", Authors)
			.Add("artists", Artists)
			.Add("year", Year)
			.Add("includedTags", IncludedTags)
			.Add("includedTagsMode", IncludeTagsMode)
			.Add("excludedTags", ExcludedTags)
			.Add("excludedTagsMode", ExcludedTagsMode)
			.Add("status", Status)
			.Add("originalLanguage", OriginalLanguage)
			.Add("excludedOriginalLanguage", ExcludedOriginalLanguage)
			.Add("availableTranslatedLanguage", AvailableTranslatedLanguage)
			.Add("publicationDemographic", PublicationDemographic)
			.Add("ids", Ids)
			.Add("contentRating", ContentRating)
			.Add("createdAtSince", CreatedAtSince)
			.Add("updatedAtSince", UpdatedAtSince)
			.Add("order", Order)
			.Add("includes", Includes)
			.Add("hasAvailableChapters", HasAvailableChapters)
			.Add("group", Group)
			.Build();
	}

	public enum OrderKey
	{
		title,
		year,
		createdAt,
		updatedAt,
		latestUploadedChapter,
		followedCount,
		relevance,
		rating
	}
}
