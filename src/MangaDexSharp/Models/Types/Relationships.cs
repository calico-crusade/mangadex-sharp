namespace MangaDexSharp;

/// <summary>
/// Represents the varying relationships between manga titles
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<Relationships>))]
public enum Relationships
{
	/// <summary>
	/// A black and white version of the manga
	/// </summary>
	monochrome,
	/// <summary>
	/// The original narrative this manga is based on
	/// </summary>
	main_story,
	/// <summary>
	/// The original work this spin-off manga has been adapted from
	/// </summary>
	adapted_from,
	/// <summary>
	/// The original work this self-published derivative manga is based on
	/// </summary>
	based_on,
	/// <summary>
	/// The previous entry in the same series
	/// </summary>
	prequel,
	/// <summary>
	/// A side work contemporaneous with the narrative of this manga
	/// </summary>
	side_story,
	/// <summary>
	/// A self-published derivative work based on this manga
	/// </summary>
	doujinshi,
	/// <summary>
	/// A manga based on the same intellectual property as this manga
	/// </summary>
	same_franchise,
	/// <summary>
	/// A manga taking place in the same fictional world as this manga
	/// </summary>
	shared_universe,
	/// <summary>
	/// The next entry in the same series
	/// </summary>
	sequel,
	/// <summary>
	/// An official derivative work based on this manga
	/// </summary>
	spin_off,
	/// <summary>
	/// An alternative take of the story in this manga
	/// </summary>
	alternate_story,
	/// <summary>
	/// A different version of this manga with no other specific distinction
	/// </summary>
	alternate_version,
	/// <summary>
	/// The original version of this manga before its official serialization
	/// </summary>
	preserialization,
	/// <summary>
	/// A colored variant of this manga
	/// </summary>
	colored,
	/// <summary>
	/// The official serialization of this manga
	/// </summary>
	serialization
}
