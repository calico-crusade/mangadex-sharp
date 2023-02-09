namespace MangaDexSharp;

public class MangaRandomFilter : IFilter
{
	public MangaIncludes[] Includes { get; set; } = new[] { MangaIncludes.manga, MangaIncludes.cover_art, MangaIncludes.author, MangaIncludes.author, MangaIncludes.tag };

	public ContentRating[] Rating { get; set; } = ContentRatingsAll;

	public string[] IncludedTags { get; set; } = Array.Empty<string>();

	public Mode? IncludedTagsMode { get; set; }

	public string[] ExcludedTags { get; set; } = Array.Empty<string>();

	public Mode? ExcludedTagsMode { get; set; }
	
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
