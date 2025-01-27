namespace MangaDexSharp;

/// <summary>
/// Represents a view of the volumes and chapters available for this manga
/// </summary>
public class MangaAggregate
{
	/// <summary>
	/// Whether or not the request was successful
	/// </summary>
	[JsonPropertyName("result")]
	public string Result { get; set; } = string.Empty;

	/// <summary>
	/// All of the volumes on the manga
	/// </summary>
	[JsonPropertyName("volumes")]
	[JsonConverter(typeof(MangaDexDictionaryParser<string, VolumeData>))]
	public Dictionary<string, VolumeData> Volumes { get; set; } = new();

	/// <summary>
	/// Represents a volume of a manga
	/// </summary>
	public class VolumeData
	{
		/// <summary>
		/// The ordinal of the volume
		/// </summary>
		[JsonPropertyName("volume")]
		public string Volume { get; set; } = string.Empty;

		/// <summary>
		/// How many chapters are in the volume
		/// </summary>
		[JsonPropertyName("count")]
		public int Count { get; set; }

		/// <summary>
		/// The chapters within the volume
		/// </summary>
		[JsonPropertyName("chapters")]
        [JsonConverter(typeof(MangaDexAggregateChapterParser))]
        public Dictionary<string, ChapterData> Chapters { get; set; } = new();
    }

	/// <summary>
	/// Represents a chapter of the manga
	/// </summary>
	public class ChapterData
	{
		/// <summary>
		/// The ordinal of the chapter
		/// </summary>
		[JsonPropertyName("chapter")]
		public string Chapter { get; set; } = string.Empty;

		/// <summary>
		/// The ID of the chapter
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// IDs of the other versions of this same chapter
		/// </summary>
		[JsonPropertyName("others")]
		public string[] Others { get; set; } = Array.Empty<string>();

		/// <summary>
		/// How many `others` there are
		/// </summary>
		[JsonPropertyName("count")]
		public int Count { get; set; }
	}
}
