namespace MangaDexSharp;

/// <summary>
/// Represents the state of a report
/// </summary>
public enum ReportState
{
	/// <summary>
	/// The report has been submitted and is pending investigation
	/// </summary>
	waiting,
	/// <summary>
	/// The report has been deemed valid and action has been taken
	/// </summary>
	accepted,
	/// <summary>
	/// The report has been deemed invalid and no action has been taken
	/// </summary>
	refused,
	/// <summary>
	/// The action was automatically resolved
	/// </summary>
	autoresolved
}
