namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<ReadStatus>))]
public enum ReadStatus
{
	reading,
	on_hold,
	plan_to_read,
	dropped,
	re_reading,
	completed
}
