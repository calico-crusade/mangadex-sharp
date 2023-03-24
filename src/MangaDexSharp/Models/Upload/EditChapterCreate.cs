namespace MangaDexSharp;

/// <summary>
/// Represents a request to start editing a chapter
/// </summary>
public class EditChapterCreate
{
	/// <summary>
	/// The version of the request
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
