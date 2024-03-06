namespace MangaDexSharp;

/// <summary>
/// Represents the available query parameters for the manga endpoint
/// </summary>
public class MangaFilter : IPaginateFilter
{
	/// <summary>
	/// The title of the manga
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The number of records to return for this filter (MAX 100, MIN 1, DEFAULT 100)
	/// </summary>
	public int Limit { get; set; } = 100;

	/// <summary>
	/// The number of records to skip when using this filter (MIN 0, DEFAULT 0)
	/// </summary>
	public int Offset { get; set; } = 0;

	/// <summary>
	/// The author or artist ID
	/// </summary>
	public string AuthorOrArtist { get; set; } = string.Empty;

	/// <summary>
	/// Only include manga with these content ratings (NSFW vs SFW)
	/// </summary>
	public ContentRating[] ContentRating { get; set; } = ContentRatingsAll;

	/// <summary>
	/// Determine what to include in the return results of the manga
	/// </summary>
	public MangaIncludes[] Includes { get; set; } = new[] { MangaIncludes.cover_art };

	/// <summary>
	/// Only include manga with these author IDs
	/// </summary>
	public string[] Authors { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include manga with these artist IDs
	/// </summary>
	public string[] Artists { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include manga made in this year
	/// </summary>
	public int? Year { get; set; }

	/// <summary>
	/// Only include manga with these tags.
	/// </summary>
	public string[] IncludedTags { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Whether or not to require ALL tags or just some of them
	/// </summary>
	public Mode? IncludeTagsMode { get; set; }

	/// <summary>
	/// Only include manga that don't have these tags
	/// </summary>
	public string[] ExcludedTags { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Whether or not to require ALL excluded tags or some of them
	/// </summary>
	public Mode? ExcludedTagsMode { get; set; }

	/// <summary>
	/// Only include manga with these publication statuses
	/// </summary>
	public Status[] Status { get; set; } = Array.Empty<Status>();

	/// <summary>
	/// Only include manga with these original language codes
	/// </summary>
	public string[] OriginalLanguage { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include manga that weren't written in these languages originally
	/// </summary>
	public string[] ExcludedOriginalLanguage { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include manga with these available language codes
	/// </summary>
	public string[] AvailableTranslatedLanguage { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include manga that fall in these publication demographics
	/// </summary>
	public Demographic[] PublicationDemographic { get; set; } = Array.Empty<Demographic>();

	/// <summary>
	/// Only include manga with these IDs
	/// </summary>
	public string[] Ids { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include manga made since this date
	/// </summary>
	public DateTime? CreatedAtSince { get; set; }

	/// <summary>
	/// Only include manga updated since this date
	/// </summary>
	public DateTime? UpdatedAtSince { get; set; }

	/// <summary>
	/// Whether or not to include manga that have available chapters
	/// </summary>
	public bool? HasAvailableChapters { get; set; }

	/// <summary>
	/// Only include manga from this group ID
	/// </summary>
	public string Group { get; set; } = string.Empty;

	/// <summary>
	/// Determine how to order the returned results
	/// </summary>
	public Dictionary<OrderKey, OrderValue> Order { get; set; } = new();

	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// The available fields the can be ordered by
	/// </summary>
	public enum OrderKey
	{
		/// <summary>
		/// Order by the title field
		/// </summary>
		title,
		/// <summary>
		/// Order by the year field
		/// </summary>
		year,
		/// <summary>
		/// Order by the created at field
		/// </summary>
		createdAt,
		/// <summary>
		/// Order by the updated at field
		/// </summary>
		updatedAt,
		/// <summary>
		/// Order by the latest uploaded chapter field
		/// </summary>
		latestUploadedChapter,
		/// <summary>
		/// Order by the followed count field
		/// </summary>
		followedCount,
		/// <summary>
		/// Order by the relevance field
		/// </summary>
		relevance,
		/// <summary>
		/// Order by the rating field 
		/// </summary>
		rating
	}
}
