namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<Visibility>))]
public enum Visibility
{
	Public,
	Private
}
