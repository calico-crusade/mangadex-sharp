namespace MangaDexSharp;

/// <summary>
/// Represents a request to create a manga in the MD api
/// </summary>
public class MangaCreate
{
	/// <summary>
	/// The title of the manga in varying languages
	/// </summary>
	[JsonPropertyName("title")]
	public Localization Title { get; set; } = [];

	/// <summary>
	/// Any alternative titles of the manga in varying languages
	/// </summary>
	[JsonPropertyName("altTitles")]
	public Localization[] AltTitles { get; set; } = [];

	/// <summary>
	/// The description of the manga in varying languages
	/// </summary>
	[JsonPropertyName("description")]
	public Localization Description { get; set; } = [];

	/// <summary>
	/// The IDs of the authors of this manga
	/// </summary>
	[JsonPropertyName("authors")]
	public string[] Authors { get; set; } = [];

	/// <summary>
	/// The IDs of the artists of this manga
	/// </summary>
	[JsonPropertyName("artists")]
	public string[] Artists { get; set; } = [];

	/// <summary>
	/// A collection of external links for this manga
	/// </summary>
	[JsonPropertyName("links")]
	public Localization Links { get; set; } = [];

	/// <summary>
	/// The original language the manga was written in
	/// </summary>
	[JsonPropertyName("originalLanguage")]
	public string OriginalLanguage { get; set; } = string.Empty;

	/// <summary>
	/// The ordinal of the last volume in the manga
	/// </summary>
	[JsonPropertyName("lastVolume")]
	public string? LastVolume { get; set; }

	/// <summary>
	/// The ordinal of the last chapter in the manga
	/// </summary>
	[JsonPropertyName("lastChapter")]
	public string? LastChapter { get; set; }

	/// <summary>
	/// What demographic the manga is targetting
	/// </summary>
	[JsonPropertyName("publicationDemographic")]
	public Demographic? Demographic { get; set; }

	/// <summary>
	/// The publication status of the manga
	/// </summary>
	[JsonPropertyName("status")]
	public Status Status { get; set; } = Status.ongoing;

	/// <summary>
	/// The year the manga was made
	/// </summary>
	[JsonPropertyName("year")]
	public int? Year { get; set; }

	/// <summary>
	/// The content rating of the manga 
	/// </summary>
	[JsonPropertyName("contentRating")]
	public ContentRating ContentRating { get; set; } = ContentRating.safe;

	/// <summary>
	/// Whether or not the chapter ordinals reset when a new volume is released
	/// </summary>
	[JsonPropertyName("chapterNumbersResetOnNewVolume")]
	public bool ChapterNumbersResetOnNewVolume { get; set; } = false;

	/// <summary>
	/// The IDs of the tags associated with this manga
	/// </summary>
	[JsonPropertyName("tags")]
	public string[] Tags { get; set; } = [];

	/// <summary>
	/// The ID of the primary cover art for this manga
	/// </summary>
	[JsonPropertyName("primaryCover")]
	public string? PrimaryCover { get; set; }

	/// <summary>
	/// The version of the request
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
