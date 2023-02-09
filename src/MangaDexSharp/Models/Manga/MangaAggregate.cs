namespace MangaDexSharp;

public class MangaAggregate
{
	[JsonPropertyName("result")]
	public string Result { get; set; } = string.Empty;

	[JsonPropertyName("volumes")]
	public VolumeData[] Volumes { get; set; } = Array.Empty<VolumeData>();

	public class VolumeData
	{
		[JsonPropertyName("volume")]
		public string Volume { get; set; } = string.Empty;

		[JsonPropertyName("count")]
		public int Count { get; set; }

		[JsonPropertyName("chapters")]
		public ChapterData[] Chapters { get; set; } = Array.Empty<ChapterData>();
	}

	public class ChapterData
	{
		[JsonPropertyName("chapter")]
		public string Chapter { get; set; } = string.Empty;

		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		[JsonPropertyName("others")]
		public string[] Others { get; set; } = Array.Empty<string>();

		[JsonPropertyName("count")]
		public int Count { get; set; }
	}
}
