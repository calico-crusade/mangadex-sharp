namespace MangaDexSharp;

/// <summary>
/// Represents the different types of reports
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<ReportCategory>))]
public enum ReportCategory
{
	/// <summary>
	/// The report is for a manga
	/// </summary>
	manga,
	/// <summary>
	/// The report is for a chapter
	/// </summary>
	chapter,
	/// <summary>
	/// The report is for a scanlation group
	/// </summary>
	scanlation_group,
	/// <summary>
	/// The report is for a user
	/// </summary>
	user,
	/// <summary>
	/// The report is for an author
	/// </summary>
	author
}
