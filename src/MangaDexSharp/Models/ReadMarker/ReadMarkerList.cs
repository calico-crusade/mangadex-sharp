namespace MangaDexSharp;

/// <summary>
/// Represents a collection of read chapter IDs
/// </summary>
public class ReadMarkerList : MangaDexRoot
{
	/// <summary>
	/// All of the chapter IDs that have been read
	/// </summary>
	[JsonPropertyName("data")]
	public string[] Data { get; set; } = Array.Empty<string>();
}
