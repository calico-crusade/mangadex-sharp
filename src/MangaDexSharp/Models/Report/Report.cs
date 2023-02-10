namespace MangaDexSharp;

public class Report : MangaDexModel<Report.AttributesModel>, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("details")]
		public string Details { get; set; } = string.Empty;

		[JsonPropertyName("objectId")]
		public string ObjectId { get; set; } = string.Empty;

		[JsonPropertyName("status")]
		public ReportState Status { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime? CreatedAt { get; set; }
	}
}

public class ReportList : MangaDexCollection<Report> { }