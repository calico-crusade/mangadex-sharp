namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<ReportCategory>))]
public enum ReportCategory
{
	manga,
	chapter,
	scanlation_group,
	user,
	author
}
