namespace MangaDexSharp;

/// <summary>
/// Represents a comment thread on the MD forums
/// </summary>
public class Thread
{
	/// <summary>
	/// The ID of the forum thread
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// The type of forum thread
	/// </summary>
	[JsonPropertyName("type")]
	public string Type { get; set; } = string.Empty;

	/// <summary>
	/// Any properties of the thread
	/// </summary>
	[JsonPropertyName("attributes")]
	public ThreadAttributesModel Attributes { get; set; } = new();

	/// <summary>
	/// The properties of the thread
	/// </summary>
	public class ThreadAttributesModel
	{
		/// <summary>
		/// How many replies the thread has
		/// </summary>
		[JsonPropertyName("repliesCount")]
		public int RepliesCount { get; set; }
	}
}
