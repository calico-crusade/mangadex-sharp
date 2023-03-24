namespace MangaDexSharp;

/// <summary>
/// Represents the report of a broken page link
/// </summary>
public class PageReport
{
	/// <summary>
	/// The full URL of the image
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;

	/// <summary>
	/// Whether or not the request was successful
	/// </summary>
	[JsonPropertyName("success")]
	public bool Success { get; set; }

	/// <summary>
	/// The length of the returned image
	/// </summary>
	[JsonPropertyName("bytes")]
	public int Bytes { get; set; }

	/// <summary>
	/// The time it took to receive the image
	/// </summary>
	[JsonPropertyName("duration")]
	public int Duration { get; set; }

	/// <summary>
	/// Whether or not the image was cached
	/// </summary>
	[JsonPropertyName("cached")]
	public bool Cached { get; set; }
}
