namespace MangaDexSharp;

/// <summary>
/// Represents a reason a report can be made for
/// </summary>
public class ReportReason : MangaDexModel<ReportReason.ReportReasonAttributesModel>
{
	/// <summary>
	/// The properties of the reason
	/// </summary>
	public class ReportReasonAttributesModel
	{
		/// <summary>
		/// A description of the reason in varying languages
		/// </summary>
		[JsonPropertyName("reason")]
		public Localization Reason { get; set; } = new();

		/// <summary>
		/// Whether or not the details are required for the report
		/// </summary>
		[JsonPropertyName("detailsRequired")]
		public bool DetailsRequired { get; set; }

		/// <summary>
		/// The type of report this reason is for
		/// </summary>
		[JsonPropertyName("category")]
		public ReportCategory Category { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a collection of report reasons
/// </summary>
public class ReportReasonList : MangaDexCollection<ReportReason> { }