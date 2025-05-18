namespace MangaDexSharp;

/// <summary>
/// Represents the response of a pages request
/// </summary>
public class Pages : MangaDexRoot
{
	/// <summary>
	/// The base URL of the image CDN
	/// </summary>
	[JsonPropertyName("baseUrl")]
	public string BaseUrl { get; set; } = string.Empty;

	/// <summary>
	/// All of the page data within the chapter
	/// </summary>
	[JsonPropertyName("chapter")]
	public ChapterData Chapter { get; set; } = new();

	/// <summary>
	/// All of the full resolution image URLs
	/// </summary>
	[JsonIgnore]
	public string[] Images => GenerateImageLinks();

	/// <summary>
	/// All of the data-saver image URLs
	/// </summary>
	[JsonIgnore]
	public string[] DataSaverImages => GenerateImageLinks(true);

	/// <summary>
	/// Generates the full URL of the images
	/// </summary>
	/// <param name="dataSaver">Whether to generate the data-saver images URLs or the full resolution ones</param>
	/// <returns>All of the page URLs</returns>
	public string[] GenerateImageLinks(bool dataSaver = false)
	{
		var names = dataSaver ? Chapter.DataSaver : Chapter.Data;

		return names
			.Select(t => $"{BaseUrl}/data/{Chapter.Hash}/{t}")
			.ToArray();
	}

	/// <summary>
	/// Represents the pages of a chapter
	/// </summary>
	public class ChapterData
	{
		/// <summary>
		/// The chapter's unique hash on the CDN
		/// </summary>
		[JsonPropertyName("hash")]
		public string Hash { get; set; } = string.Empty;

		/// <summary>
		/// The full-resolution image names
		/// </summary>
		[JsonPropertyName("data")]
		public string[] Data { get; set; } = [];

		/// <summary>
		/// The data-saver image names
		/// </summary>
		[JsonPropertyName("dataSaver")]
		public string[] DataSaver { get; set; } = [];
	}
}
