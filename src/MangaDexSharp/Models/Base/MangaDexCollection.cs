namespace MangaDexSharp;

public class MangaDexCollection<T> : MangaDexRoot<List<T>>
{
	[JsonPropertyName("limit")]
	public int Limit { get; set; }

	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	[JsonPropertyName("total")]
	public int Total { get; set; }
}
