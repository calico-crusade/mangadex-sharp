namespace MangaDexSharp;

/// <summary>
/// Represents a report in the MD api
/// </summary>
public class Report : MangaDexModel<Report.AttributesModel>, IRelationshipModel
{
	/// <summary>
	/// All of the related items to the report
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// Any properties the report has
	/// </summary>
	public class AttributesModel
	{
		/// <summary>
		/// The details of the report
		/// </summary>
		[JsonPropertyName("details")]
		public string Details { get; set; } = string.Empty;

		/// <summary>
		/// The ID of the item being reported
		/// </summary>
		[JsonPropertyName("objectId")]
		public string ObjectId { get; set; } = string.Empty;

		/// <summary>
		/// The status of the report
		/// </summary>
		[JsonPropertyName("status")]
		public ReportState Status { get; set; }

		/// <summary>
		/// When the report was created
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime? CreatedAt { get; set; }
	}
}

/// <summary>
/// Represents a collection of reports
/// </summary>
public class ReportList : MangaDexCollection<Report> { }