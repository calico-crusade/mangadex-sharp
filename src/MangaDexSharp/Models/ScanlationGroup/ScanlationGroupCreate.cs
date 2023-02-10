namespace MangaDexSharp;

public class ScanlationGroupCreate
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

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
}
