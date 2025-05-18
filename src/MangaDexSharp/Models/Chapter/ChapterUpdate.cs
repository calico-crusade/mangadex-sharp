namespace MangaDexSharp;

/// <summary>
/// Represents a request to update a chapter in the MD api
/// </summary>
public class ChapterUpdate
{
	/// <summary>
	/// The title of the chapter
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// The volume ordinal of the chapter
	/// </summary>
	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	/// <summary>
	/// The chapter ordinal of the chapter
	/// </summary>
	[JsonPropertyName("chapter")]
	public string? Chapter { get; set; }

	/// <summary>
	/// The language the chapter is in
	/// </summary>
	[JsonPropertyName("translatedLanguage")]
	public string Language { get; set; } = string.Empty;

	/// <summary>
	/// The groups the chapter was translated by
	/// </summary>
	[JsonPropertyName("groups")]
	public string[] Groups { get; set; } = [];

	/// <summary>
	/// The version of the request
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
