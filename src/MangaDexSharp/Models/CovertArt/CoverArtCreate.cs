namespace MangaDexSharp;

/// <summary>
/// Represents a create cover art request to the MD api
/// </summary>
public class CoverArtCreate
{
	/// <summary>
	/// The file name/ID for the cover
	/// </summary>
	[JsonPropertyName("file")]
	public string File { get; set; } = string.Empty;

	/// <summary>
	/// The volume ordinal this cover belongs to
	/// </summary>
	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	/// <summary>
	/// A description of the covert art
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// The language / locale code for this cover art
	/// </summary>
	[JsonPropertyName("locale")]
	public string Locale { get; set; } = string.Empty;
}
