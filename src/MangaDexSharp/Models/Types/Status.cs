namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<Status>))]
public enum Status
{
	ongoing,
	completed,
	hiatus,
	cancelled
}
