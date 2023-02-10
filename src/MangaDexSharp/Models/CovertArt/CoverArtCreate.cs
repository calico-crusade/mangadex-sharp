namespace MangaDexSharp;

public class CoverArtCreate
{
	[JsonPropertyName("file")]
	public string File { get; set; } = string.Empty;

	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;

	[JsonPropertyName("locale")]
	public string Locale { get; set; } = string.Empty;
}
