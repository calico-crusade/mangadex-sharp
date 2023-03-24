namespace MangaDexSharp;

/// <summary>
/// Represents a request to create an upload session
/// </summary>
public class UploadSessionCreate
{
	/// <summary>
	/// The groups the upload session is for
	/// </summary>
	[JsonPropertyName("groups")]
	public string[] Groups { get; set; } = Array.Empty<string>();

	/// <summary>
	/// The manga the upload session is for
	/// </summary>
	[JsonPropertyName("manga")]
	public string Manga { get; set; } = string.Empty;
}
