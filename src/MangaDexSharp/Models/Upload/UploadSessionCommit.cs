namespace MangaDexSharp;

public class UploadSessionCommit
{
	[JsonPropertyName("chapterDraft")]
	public ChapterDraft Chapter { get; set; } = new();

	[JsonPropertyName("pageOrder")]
	public string[] PageOrder { get; set; } = Array.Empty<string>();
}

public class ChapterDraft
{
	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	[JsonPropertyName("chapter")]
	public string? Chapter { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("translatedLanguage")]
	public string TranslatedLanguage { get; set; } = string.Empty;

	[JsonPropertyName("externalUrl")]
	public string? ExternalUrl { get; set; }

	[JsonPropertyName("publishAt")]
	public DateTime? PublishAt { get; set; }
}
