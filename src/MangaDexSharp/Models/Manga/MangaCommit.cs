namespace MangaDexSharp;

/// <summary>
/// Represens a request to MD to commit a manga change / draft
/// </summary>
public class MangaCommit
{
	/// <summary>
	/// The version of the request
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
