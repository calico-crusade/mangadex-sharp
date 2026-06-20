namespace MangaDexSharp;

/// <summary>
/// Represents a user's reading history response
/// </summary>
public class ReadingHistory : MangaDexRoot
{
	/// <summary>
	/// The reading history entries returned by MangaDex
	/// </summary>
	[JsonPropertyName("ratings")]
	public List<ReadingHistoryItem> Ratings { get; set; } = [];
}

/// <summary>
/// Represents an entry in a user's reading history
/// </summary>
public class ReadingHistoryItem
{
	/// <summary>
	/// The chapter ID that was read
	/// </summary>
	[JsonPropertyName("chapterId")]
	public string ChapterId { get; set; } = string.Empty;

	/// <summary>
	/// When the chapter was read
	/// </summary>
	[JsonPropertyName("readDate")]
	public DateTime? ReadDate { get; set; }
}
