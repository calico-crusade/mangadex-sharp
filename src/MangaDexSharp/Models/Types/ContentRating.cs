namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<ContentRating>))]
public enum ContentRating
{
	safe,
	suggestive,
	erotica,
	pornographic
}
