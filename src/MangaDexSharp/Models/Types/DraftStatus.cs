namespace MangaDexSharp;

/// <summary>
/// Represents the status of a manga draft
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<DraftStatus>))]
public enum DraftStatus
{
	/// <summary>
	/// It is indeed a draft of the manga
	/// </summary>
	draft,
	/// <summary>
	/// It has been submitted and not yet published
	/// </summary>
	submitted,
	/// <summary>
	/// It was rejected because it contains references to Boku no pico 
	/// </summary>
	rejected
}
