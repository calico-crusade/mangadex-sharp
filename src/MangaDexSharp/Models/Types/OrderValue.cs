namespace MangaDexSharp;

/// <summary>
/// How to order return results
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<OrderValue>))]
public enum OrderValue
{
	/// <summary>
	/// A-Z / 0-9
	/// </summary>
	asc,
	/// <summary>
	/// Z-A / 9-0
	/// </summary>
	desc
}
