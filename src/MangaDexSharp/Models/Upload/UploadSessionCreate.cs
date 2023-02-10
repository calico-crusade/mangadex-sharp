namespace MangaDexSharp;

public class UploadSessionCreate
{
	[JsonPropertyName("groups")]
	public string[] Groups { get; set; } = Array.Empty<string>();

	[JsonPropertyName("manga")]
	public string Manga { get; set; } = string.Empty;
}
