namespace MangaDexSharp;

/// <summary>
/// How to group manga tags
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<Group>))]
public enum Group
{
	/// <summary>
	/// Potential NSFW content (gore, sexual violence, etc.)
	/// </summary>
	content,
	/// <summary>
	/// What style of manga it is (4-Koma, adaption, oneshot, colored etc.)
	/// </summary>
	format,
	/// <summary>
	/// The genre of the manga (Isekai, Horror, Comedy, Romance, etc.)
	/// </summary>
	genre,
	/// <summary>
	/// The theme of the manga (Animals, Police, Monster Girls, Loli, etc.)
	/// </summary>
	theme
}
