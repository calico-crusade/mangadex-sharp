namespace MangaDexSharp;

/// <summary>
/// Represents a request to update a cover art object in the MD api
/// </summary>
public class CoverArtUpdate
{
	/// <summary>
	/// The volume ordinal for this cover art
	/// </summary>
	[JsonPropertyName("volume")]
	public string? Volume { get; set; }

	/// <summary>
	/// The description of the cover art
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// The language / locale code for this cover art
	/// </summary>
	[JsonPropertyName("locale")]
	public string Locale { get; set; } = string.Empty;

	/// <summary>
	/// The request version
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
