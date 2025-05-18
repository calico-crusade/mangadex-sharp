namespace MangaDexSharp;

/// <summary>
/// Represents a manga read marker batch update request
/// </summary>
public class ReadMarkerBatchUpdate
{
	/// <summary>
	/// The IDs of the chapters that have been read
	/// </summary>
	[JsonPropertyName("chapterIdsRead")]
	public string[] ChapterIdsRead { get; set; } = [];

	/// <summary>
	/// The IDs of the chapters that have not been read
	/// </summary>
	[JsonPropertyName("chapterIdsUnread")]
	public string[] ChapterIdsUnread { get; set; } = [];
}
