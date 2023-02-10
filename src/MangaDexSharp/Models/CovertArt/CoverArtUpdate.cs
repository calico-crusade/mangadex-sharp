namespace MangaDexSharp;

public class CoverArtUpdate
{
	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;

	[JsonPropertyName("locale")]
	public string Locale { get; set; } = string.Empty;

	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
