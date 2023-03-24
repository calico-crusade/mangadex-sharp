namespace MangaDexSharp;

/// <summary>
/// Represents the status of a manga
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<ReadStatus>))]
public enum ReadStatus
{
	/// <summary>
	/// Actively reading the manga
	/// </summary>
	reading,
	/// <summary>
	/// Started, but waiting to finish the manga until later
	/// </summary>
	on_hold,
	/// <summary>
	/// Plan on reading the manga later
	/// </summary>
	plan_to_read,
	/// <summary>
	/// Started reading but dropped it because it contains a reference to boku no pico
	/// </summary>
	dropped,
	/// <summary>
	/// Already finished reading the manga, but reading it again because it's so damn good.
	/// </summary>
	re_reading,
	/// <summary>
	/// Finished reading the manga
	/// </summary>
	completed
}
