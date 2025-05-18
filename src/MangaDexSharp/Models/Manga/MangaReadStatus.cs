namespace MangaDexSharp;

/// <summary>
/// Represents the read status of a manga in the MD api
/// </summary>
public class MangaReadStatus : MangaDexRoot
{
	/// <summary>
	/// The read status
	/// </summary>
	[JsonPropertyName("status")]
	public ReadStatus Status { get; set; }
}

/// <summary>
/// Represents a collection of read statuses of manga in the MD api
/// </summary>
public class MangaReadStatuses : MangaDexRoot
{
	/// <summary>
	/// The read status
	/// </summary>
	[JsonPropertyName("statuses")]
	public Dictionary<string, ReadStatus> Statuses { get; set; } = [];
}

/// <summary>
/// Represents a request to update the read status of a manga
/// </summary>
public class MangaReadStatusPush
{
	/// <summary>
	/// The read status
	/// </summary>
	[JsonPropertyName("status")]
	public ReadStatus? Status { get; set; }
}