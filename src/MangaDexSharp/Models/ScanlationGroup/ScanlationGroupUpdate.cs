namespace MangaDexSharp;

/// <summary>
/// Represents a request to update a scanlation group
/// </summary>
public class ScanlationGroupUpdate
{
	/// <summary>
	/// The name of the group
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// The owner of the group
	/// </summary>
	[JsonPropertyName("leader")]
	public string? Leader { get; set; } = string.Empty;

	/// <summary>
	/// The members of the group
	/// </summary>
	[JsonPropertyName("members")]
	public string[] Members { get; set; } = Array.Empty<string>();

	/// <summary>
	/// The languages the group focuses on translating
	/// </summary>
	[JsonPropertyName("focusedLanguages")]
	public string[] FocusedLanguages { get; set; } = Array.Empty<string>();

	/// <summary>
	/// The website of the group
	/// </summary>
	[JsonPropertyName("website")]
	public string? Website { get; set; }

	/// <summary>
	/// The IRC server for the group
	/// </summary>
	[JsonPropertyName("ircServer")]
	public string? IrcServer { get; set; }

	/// <summary>
	/// The IRC channel for the group
	/// </summary>
	[JsonPropertyName("ircChannel")]
	public string? IrcChannel { get; set; }

	/// <summary>
	/// The discord server for the group
	/// </summary>
	[JsonPropertyName("discord")]
	public string? Discord { get; set; }

	/// <summary>
	/// The contact email for the group
	/// </summary>
	[JsonPropertyName("contactEmail")]
	public string? ContactEmail { get; set; }

	/// <summary>
	/// The description of the group
	/// </summary>
	[JsonPropertyName("Description")]
	public string? Description { get; set; }

	/// <summary>
	/// The twitter handle for the group
	/// </summary>
	[JsonPropertyName("twitter")]
	public string? Twitter { get; set; }

	/// <summary>
	/// The manga updates URL for the group
	/// </summary>
	[JsonPropertyName("mangaUpdates")]
	public string? MangaUpdates { get; set; }

	/// <summary>
	/// Whether or not the group is inactive
	/// </summary>
	[JsonPropertyName("inactive")]
	public bool Inactive { get; set; }

	/// <summary>
	/// The groups publish delay
	/// </summary>
	[JsonPropertyName("publishDelay")]
	public string? PublishDelay { get; set; }

	/// <summary>
	/// Whether or not the group is locked for changes
	/// </summary>
	[JsonPropertyName("locked")]
	public bool Locked { get; set; }

	/// <summary>
	/// The version of the request
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
