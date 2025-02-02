namespace MangaDexSharp;

/// <summary>
/// Represents the available query parameters for the random manga endpoint
/// </summary>
public class MangaRandomFilter : IFilter
{
	/// <summary>
	/// Determine what to include in the return results of the manga
	/// </summary>
	public MangaIncludes[] Includes { get; set; } = new[] { MangaIncludes.manga, MangaIncludes.cover_art, MangaIncludes.author, MangaIncludes.author, MangaIncludes.tag, MangaIncludes.artist };

	/// <summary>
	/// Only include manga with these content ratings (NSFW vs SFW)
	/// </summary>
	public ContentRating[] Rating { get; set; } = ContentRatingsAll;

	/// <summary>
	/// Only include manga with these tags.
	/// </summary>
	public string[] IncludedTags { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Whether or not to require ALL tags or just some of them
	/// </summary>
	public Mode? IncludedTagsMode { get; set; }

	/// <summary>
	/// Only include manga that don't have these tags
	/// </summary>
	public string[] ExcludedTags { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Whether or not to require ALL excluded tags or some of them
	/// </summary>
	public Mode? ExcludedTagsMode { get; set; }

	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns></returns>
	public string BuildQuery()
	{
		return new FilterBuilder()
			.Add("includes", Includes)
			.Add("contentRating", Rating)
			.Add("includedTags", IncludedTags)
			.Add("includedTagsMode", IncludedTagsMode)
			.Add("excludedTags", ExcludedTags)
			.Add("excludedTagsMode", ExcludedTagsMode)
			.Build();
	}
}
