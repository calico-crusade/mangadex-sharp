namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<DraftStatus>))]
public enum DraftStatus
{
	draft,
	submitted,
	rejected
}
