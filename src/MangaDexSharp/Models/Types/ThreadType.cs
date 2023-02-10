namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<ThreadType>))]
public enum ThreadType
{
	manga,
	group,
	chapter
}
