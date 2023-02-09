namespace MangaDexSharp;

public class MangaCreate
{
	[JsonPropertyName("title")]
	public Localization Title { get; set; } = new();

	[JsonPropertyName("altTitles")]
	public Localization[] AltTitles { get; set; } = Array.Empty<Localization>();

	[JsonPropertyName("description")]
	public Localization Description { get; set; } = new();

	[JsonPropertyName("authors")]
	public string[] Authors { get; set; } = Array.Empty<string>();

	[JsonPropertyName("artists")]
	public string[] Artists { get; set; } = Array.Empty<string>();

	[JsonPropertyName("links")]
	public Localization Links { get; set; } = new();

	[JsonPropertyName("originalLanguage")]
	public string OriginalLanguage { get; set; } = string.Empty;

	[JsonPropertyName("lastVolume")]
	public string? LastVolume { get; set; }

	[JsonPropertyName("lastChapter")]
	public string? LastChapter { get; set; }

	[JsonPropertyName("publicationDemographic")]
	public Demographic? Demographic { get; set; }

	[JsonPropertyName("status")]
	public Status Status { get; set; } = Status.ongoing;

	[JsonPropertyName("year")]
	public int? Year { get; set; }

	[JsonPropertyName("contentRating")]
	public ContentRating ContentRating { get; set; } = ContentRating.safe;

	[JsonPropertyName("chapterNumbersResetOnNewVolume")]
	public bool ChapterNumbersResetOnNewVolume { get; set; } = false;

	[JsonPropertyName("tags")]
	public string[] Tags { get; set; } = Array.Empty<string>();

	[JsonPropertyName("primaryCover")]
	public string? PrimaryCover { get; set; }

	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
