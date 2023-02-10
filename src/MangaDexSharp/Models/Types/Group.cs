namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<Group>))]
public enum Group
{
	content,
	format,
	genre,
	theme
}
