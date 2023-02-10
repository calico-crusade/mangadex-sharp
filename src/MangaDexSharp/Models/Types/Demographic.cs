namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<Demographic>))]
public enum Demographic
{
	shounen,
	shoujo,
	josei,
	seinen,
	none
}
