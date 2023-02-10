namespace MangaDexSharp;

public class ReportCreate
{
	[JsonPropertyName("category")]
	public ReportCategory Category { get; set; }

	[JsonPropertyName("reason")]
	public string ReasonId { get; set; } = string.Empty;

	[JsonPropertyName("objectId")]
	public string ObjectId { get; set; } = string.Empty;

	[JsonPropertyName("details")]
	public string Details { get; set; } = string.Empty;
}
