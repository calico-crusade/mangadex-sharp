namespace MangaDexSharp;

public class Pages : MangaDexRoot
{
	[JsonPropertyName("baseUrl")]
	public string BaseUrl { get; set; } = string.Empty;

	[JsonPropertyName("chapter")]
	public ChapterData Chapter { get; set; } = new();

	[JsonIgnore]
	public string[] Images => GenerateImageLinks();

	public string[] GenerateImageLinks()
	{
		return Chapter
			.Data
			.Select(t => $"{BaseUrl}/data/{Chapter.Hash}/{t}")
			.ToArray();
	}

	public class ChapterData
	{
		[JsonPropertyName("hash")]
		public string Hash { get; set; } = string.Empty;

		[JsonPropertyName("data")]
		public string[] Data { get; set; } = Array.Empty<string>();

		[JsonPropertyName("dataSaver")]
		public string[] DataSaver { get; set; } = Array.Empty<string>();
	}
}
