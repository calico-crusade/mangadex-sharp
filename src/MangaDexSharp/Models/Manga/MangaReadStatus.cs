namespace MangaDexSharp;

public class MangaReadStatus : MangaDexRoot
{
	[JsonPropertyName("status")]
	public ReadStatus Status { get; set; }
}

public class MangaReadStatuses : MangaDexRoot
{
	[JsonPropertyName("statuses")]
	public Dictionary<string, ReadStatus> Statuses { get; set; } = new();
}

public class MangaReadStatusPush
{
	[JsonPropertyName("status")]
	public ReadStatus? Status { get; set; }
}