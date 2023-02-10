namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<MangaIncludes>))]
public enum MangaIncludes
{
	manga,
	chapter,
	cover_art,
	author,
	artist,
	scanlation_group,
	tag,
	user,
	custom_list
}
