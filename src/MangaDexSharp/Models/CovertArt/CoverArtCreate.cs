namespace MangaDexSharp;

/// <summary>
/// Represents a create cover art request to the MD api
/// </summary>
public class CoverArtCreate
{
	/// <summary>
	/// The cover image file to upload
	/// </summary>
	[JsonIgnore]
	public IFileUpload? File { get; set; }

	/// <summary>
	/// The content type to use for the uploaded cover image
	/// </summary>
	[JsonIgnore]
	public string ContentType { get; set; } = "application/octet-stream";

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
