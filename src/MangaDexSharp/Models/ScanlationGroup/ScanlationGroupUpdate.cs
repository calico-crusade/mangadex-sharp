namespace MangaDexSharp;

public class ScanlationGroupUpdate
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("leader")]
	public string? Leader { get; set; } = string.Empty;

	[JsonPropertyName("members")]
	public string[] Members { get; set; } = Array.Empty<string>();

	[JsonPropertyName("focusedLanguages")]
	public string[] FocusedLanguages { get; set; } = Array.Empty<string>();

	[JsonPropertyName("website")]
	public string? Website { get; set; }

	[JsonPropertyName("ircServer")]
	public string? IrcServer { get; set; }

	[JsonPropertyName("ircChannel")]
	public string? IrcChannel { get; set; }

	[JsonPropertyName("discord")]
	public string? Discord { get; set; }

	[JsonPropertyName("contactEmail")]
	public string? ContactEmail { get; set; }

	[JsonPropertyName("Description")]
	public string? Description { get; set; }

	[JsonPropertyName("twitter")]
	public string? Twitter { get; set; }

	[JsonPropertyName("mangaUpdates")]
	public string? MangaUpdates { get; set; }

	[JsonPropertyName("inactive")]
	public bool Inactive { get; set; }

	[JsonPropertyName("publishDelay")]
	public string? PublishDelay { get; set; }

	[JsonPropertyName("locked")]
	public bool Locked { get; set; }

	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
