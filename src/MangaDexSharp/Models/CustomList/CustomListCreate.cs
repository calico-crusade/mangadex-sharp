namespace MangaDexSharp;

/// <summary>
/// Represents a request to create a custom list
/// </summary>
public class CustomListCreate
{
	/// <summary>
	/// The name of the list
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// The visibility of the list (public / private)
	/// </summary>
	[JsonPropertyName("visibility")]
	public Visibility Visibility { get; set; }

	/// <summary>
	/// The version of the request
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; }

	/// <summary>
	/// The related manga IDs
	/// </summary>
	[JsonPropertyName("manga")]
	public string[] Manga { get; set; } = Array.Empty<string>();
}
