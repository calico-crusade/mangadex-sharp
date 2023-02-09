namespace MangaDexSharp;

public class ChapterUpdate
{
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	[JsonPropertyName("chapter")]
	public string? Chapter { get; set; }

	[JsonPropertyName("translatedLanguage")]
	public string Language { get; set; } = string.Empty;

	[JsonPropertyName("groups")]
	public string[] Groups { get; set; } = Array.Empty<string>();

	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
