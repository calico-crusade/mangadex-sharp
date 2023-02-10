namespace MangaDexSharp;

public class ReadMarkerBatchUpdate
{
	[JsonPropertyName("chapterIdsRead")]
	public string[] ChapterIdsRead { get; set; } = Array.Empty<string>();

	[JsonPropertyName("chapterIdsUnread")]
	public string[] ChapterIdsUnread { get; set; } = Array.Empty<string>();
}
