namespace MangaDexSharp;

public class CustomListCreate
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("visibility")]
	public Visibility Visibility { get; set; }

	[JsonPropertyName("version")]
	public int Version { get; set; }

	[JsonPropertyName("manga")]
	public string[] Manga { get; set; } = Array.Empty<string>();
}
