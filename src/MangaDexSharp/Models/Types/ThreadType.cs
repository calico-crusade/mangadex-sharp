namespace MangaDexSharp;

/// <summary>
/// The type of a thread
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<ThreadType>))]
public enum ThreadType
{
	/// <summary>
	/// The thread is about a manga
	/// </summary>
	manga,
	/// <summary>
	/// The thread is about a scanlation group
	/// </summary>
	group,
	/// <summary>
	/// The thread is about a specific manga chapter
	/// </summary>
	chapter
}
