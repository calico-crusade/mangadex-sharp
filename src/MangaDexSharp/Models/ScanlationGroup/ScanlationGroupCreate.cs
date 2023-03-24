namespace MangaDexSharp;

/// <summary>
/// Represents a request to create a scanlation group
/// </summary>
public class ScanlationGroupCreate
{
	/// <summary>
	/// The name of the group
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// The groups website URL
	/// </summary>
	[JsonPropertyName("website")]
	public string? Website { get; set; }

	/// <summary>
	/// The groups IRC server
	/// </summary>
	[JsonPropertyName("ircServer")]
	public string? IrcServer { get; set; }

	/// <summary>
	/// The groups IRC channel
	/// </summary>
	[JsonPropertyName("ircChannel")]
	public string? IrcChannel { get; set; }

	/// <summary>
	/// The groups discord server
	/// </summary>
	[JsonPropertyName("discord")]
	public string? Discord { get; set; }

	/// <summary>
	/// The groups contact email
	/// </summary>
	[JsonPropertyName("contactEmail")]
	public string? ContactEmail { get; set; }

	/// <summary>
	/// The groups description
	/// </summary>
	[JsonPropertyName("description")]
	public string? Description { get; set; }

	/// <summary>
	/// The groups twitter handle
	/// </summary>
	[JsonPropertyName("twitter")]
	public string? Twitter { get; set; }

	/// <summary>
	/// The groups mangaupdates url
	/// </summary>
	[JsonPropertyName("mangaUpdates")]
	public string? MangaUpdates { get; set; }

	/// <summary>
	/// Whether the group is inactive or not
	/// </summary>
	[JsonPropertyName("inactive")]
	public bool Inactive { get; set; }

	/// <summary>
	/// The groups publication delay
	/// </summary>
	[JsonPropertyName("publishDelay")]
	public string? PublishDelay { get; set; }
}
