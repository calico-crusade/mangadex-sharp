namespace MangaDexSharp;

/// <summary>
/// The visibility of a custom list
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<Visibility>))]
public enum Visibility
{
	/// <summary>
	/// The visibility is available for everyone to see
	/// </summary>
	Public,
	/// <summary>
	/// Only the owner of this list can see it.
	/// </summary>
	Private
}
