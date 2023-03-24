namespace MangaDexSharp;

/// <summary>
/// Represents the publication demographics used within MD
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<Demographic>))]
public enum Demographic
{
	/// <summary>
	/// Targeted towards adolescent boys, but we don't judge.
	/// </summary>
	shounen,
	/// <summary>
	/// Targeted towards adolescent females or your adult women.
	/// </summary>
	shoujo,
	/// <summary>
	/// Targeted towards adult females
	/// </summary>
	josei,
	/// <summary>
	/// Targeted towards adult males
	/// </summary>
	seinen,
	/// <summary>
	/// Targeted towards anyone or no one.
	/// </summary>
	none
}
