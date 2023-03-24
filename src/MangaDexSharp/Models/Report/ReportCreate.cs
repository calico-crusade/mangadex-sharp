namespace MangaDexSharp;

/// <summary>
/// Represents a request to create a report within the MD api
/// </summary>
public class ReportCreate
{
	/// <summary>
	/// The type of report to create
	/// </summary>
	[JsonPropertyName("category")]
	public ReportCategory Category { get; set; }

	/// <summary>
	/// The reason the report is being created
	/// </summary>
	[JsonPropertyName("reason")]
	public string ReasonId { get; set; } = string.Empty;

	/// <summary>
	/// The ID of the object the report is for
	/// </summary>
	[JsonPropertyName("objectId")]
	public string ObjectId { get; set; } = string.Empty;

	/// <summary>
	/// The details of the report
	/// </summary>
	[JsonPropertyName("details")]
	public string Details { get; set; } = string.Empty;
}
