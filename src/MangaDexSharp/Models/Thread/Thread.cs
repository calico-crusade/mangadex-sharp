namespace MangaDexSharp;

public class Thread
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("type")]
	public string Type { get; set; } = string.Empty;

	[JsonPropertyName("attributes")]
	public AttributesModel Attributes { get; set; } = new();

	public class AttributesModel
	{
		[JsonPropertyName("repliesCount")]
		public int RepliesCount { get; set; }
	}
}
