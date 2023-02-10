namespace MangaDexSharp;

public class ReadMarkerList : MangaDexRoot
{
	[JsonPropertyName("data")]
	public string[] Data { get; set; } = Array.Empty<string>();
}
