namespace MangaDexSharp;

public class ThreadCreate
{
	[JsonPropertyName("type")]
	public ThreadType Type { get; set; }

	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;
}
