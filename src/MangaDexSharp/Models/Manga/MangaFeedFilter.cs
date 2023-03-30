namespace MangaDexSharp;

/// <summary>
/// Represents a query parameter filter for the manga feed endpoints
/// </summary>
public class MangaFeedFilter : IPaginateFilter
{
	/// <summary>
	/// The number of records to return for this filter (MAX 100, MIN 1, DEFAULT 100)
	/// </summary>
	public int Limit { get; set; } = 100;

	/// <summary>
	/// The number of records to skip when using this filter (MIN 0, DEFAULT 0)
	/// </summary>
	public int Offset { get; set; } = 0;

	/// <summary>
	/// Filters the chapters to only include chapters available in these languages
	/// </summary>
	public string[] TranslatedLanguage { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include chapters whose original language is in this list
	/// </summary>
	public string[] OriginalLanguage { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Exclude any chapters whose original language is in this list
	/// </summary>
	public string[] ExcludedOriginalLanguage { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Only include chapters with these content ratings (NSFW vs SFW)
	/// </summary>
	public ContentRating[] ContentRating { get; set; } = ContentRatingsAll;

	/// <summary>
	/// Exclude any chapters made by these groups
	/// </summary>
	public string[] ExcludedGroups { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Exclude any chapters uploaded by these users
	/// </summary>
	public string[] ExcludedUploaders { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Include chapters whose changes haven't been published yet
	/// </summary>
	public bool? IncludeFutureUpdates { get; set; }

	/// <summary>
	/// Filter chapters to only those created since this date.
	/// </summary>
	public DateTime? CreatedAtSince { get; set; }

	/// <summary>
	/// Filter chapters to only those updated since this date.
	/// </summary>
	public DateTime? UpdatedAtSince { get; set; }

	/// <summary>
	/// Filter chapters to only those published since this date.
	/// </summary>
	public DateTime? PublishAtSince { get; set; }

	/// <summary>
	/// Determine how to order the returned chapters
	/// </summary>
	public Dictionary<OrderKey, OrderValue> Order { get; set; } = new();

	/// <summary>
	/// Determine what to include in the return results of the chapter
	/// </summary>
	public MangaIncludes[] Includes { get; set; } = new[] { MangaIncludes.manga, MangaIncludes.scanlation_group, MangaIncludes.user };

	/// <summary>
	/// Whether or not to include chapters with 0 pages
	/// </summary>
	public bool? IncludeEmptyPages { get; set; }

	/// <summary>
	/// Whether or not to include chapters that have yet to be published.
	/// </summary>
	public bool? IncludeFuturePublishAt { get; set; }

	/// <summary>
	/// Whether or not to include chapters that are hosted on other sites.
	/// </summary>
	public bool? IncludeExternalUrl { get; set; }

	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns></returns>
	public string BuildQuery()
	{
		return new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("translatedLanguage", TranslatedLanguage)
			.Add("originalLanguage", OriginalLanguage)
			.Add("excludedOriginalLanguage", ExcludedOriginalLanguage)
			.Add("contentRating", ContentRating)
			.Add("excludedGroups", ExcludedGroups)
			.Add("excludedUploaders", ExcludedUploaders)
			.Add("includeFutureUpdates", IncludeFutureUpdates)
			.Add("createdAtSince", CreatedAtSince)
			.Add("updatedAtSince", UpdatedAtSince)
			.Add("publishAtSince", PublishAtSince)
			.Add("order", Order)
			.Add("includes", Includes)
			.Add("includeEmptyPages", IncludeEmptyPages)
			.Add("includeFuturePublishAt", IncludeFuturePublishAt)
			.Add("includeExternalUrl", IncludeExternalUrl)
			.Build();
	}

	/// <summary>
	/// The available fields the can be ordered by
	/// </summary>
	public enum OrderKey
	{
		/// <summary>
		/// Order by the created at field
		/// </summary>
		createdAt,
		/// <summary>
		/// Order by the updated at field
		/// </summary>
		updatedAt,
		/// <summary>
		/// Order by the publish at field
		/// </summary>
		publishAt,
		/// <summary>
		/// Order by the readable at field
		/// </summary>
		readableAt,
		/// <summary>
		/// Order by the volume ordinal field
		/// </summary>
		volume,
		/// <summary>
		/// Order by the chapter ordinal field
		/// </summary>
		chapter
	}
}
