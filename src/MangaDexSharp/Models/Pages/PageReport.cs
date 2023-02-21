namespace MangaDexSharp;

public class PageReport
{
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;

	[JsonPropertyName("success")]
	public bool Success { get; set; }

	[JsonPropertyName("bytes")]
	public int Bytes { get; set; }

	[JsonPropertyName("duration")]
	public int Duration { get; set; }

	[JsonPropertyName("cached")]
	public bool Cached { get; set; }
}
