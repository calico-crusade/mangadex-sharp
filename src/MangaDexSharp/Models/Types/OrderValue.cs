namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexEnumParser<OrderValue>))]
public enum OrderValue
{
	asc,
	desc
}
