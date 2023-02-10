namespace MangaDexSharp;

public class ReportReason : MangaDexModel<ReportReason.AttributesModel>
{
	public class AttributesModel
	{
		[JsonPropertyName("reason")]
		public Localization Reason { get; set; } = new();

		[JsonPropertyName("detailsRequired")]
		public bool DetailsRequired { get; set; }

		[JsonPropertyName("category")]
		public ReportCategory Category { get; set; }

		public int Version { get; set; }
	}
}

public class ReportReasonList : MangaDexCollection<ReportReason> { }