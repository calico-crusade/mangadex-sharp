namespace MangaDexSharp;

/// <summary>
/// Represents a request to commit an upload sessions
/// </summary>
public class UploadSessionCommit
{
	/// <summary>
	/// The draft of the chapter data
	/// </summary>
	[JsonPropertyName("chapterDraft")]
	public ChapterDraft Chapter { get; set; } = new();

	/// <summary>
	/// The order of the pages
	/// </summary>
	[JsonPropertyName("pageOrder")]
	public string[] PageOrder { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Represents a draft of the chapter
/// </summary>
public class ChapterDraft
{
	/// <summary>
	/// The volume ordinal
	/// </summary>
	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	/// <summary>
	/// The chapter ordinal
	/// </summary>
	[JsonPropertyName("chapter")]
	public string? Chapter { get; set; }

	/// <summary>
	/// The title of the chapter
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// The language this chapter is translated into
	/// </summary>
	[JsonPropertyName("translatedLanguage")]
	public string TranslatedLanguage { get; set; } = string.Empty;

	/// <summary>
	/// Whether or not the chapter is hosted externally
	/// </summary>
	[JsonPropertyName("externalUrl")]
	public string? ExternalUrl { get; set; }

	/// <summary>
	/// When the chapter is to be published
	/// </summary>
	[JsonPropertyName("publishAt")]
	public DateTime? PublishAt { get; set; }
}
