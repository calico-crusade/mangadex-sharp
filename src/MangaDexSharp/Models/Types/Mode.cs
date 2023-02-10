namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<Mode>))]
public enum Mode
{
	and,
	or
}
