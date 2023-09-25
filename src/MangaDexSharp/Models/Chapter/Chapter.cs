namespace MangaDexSharp;

/// <summary>
/// Represents a chapter returned by the MD api
/// </summary>
public class Chapter : MangaDexModel<Chapter.ChapterAttributesModel>, IRelationshipModel
{
	/// <summary>
	/// All of the related items for the chapter
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// All of the attributes the chapter has
	/// </summary>
	public class ChapterAttributesModel
	{
		/// <summary>
		/// The name / ordinal of the volume the chapter belongs to
		/// </summary>
		[JsonPropertyName("volume")]
		public string Volume { get; set; } = string.Empty;

		/// <summary>
		/// The ordinal of the chapter
		/// </summary>
		[JsonPropertyName("chapter")]
		public string Chapter { get; set; } = string.Empty;

		/// <summary>
		/// The optional title of the chapter
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// The language code the pages are in
		/// </summary>
		[JsonPropertyName("translatedLanguage")]
		public string TranslatedLanguage { get; set; } = string.Empty;

		/// <summary>
		/// The URL to the chapter if it is hosted on an external site.
		/// </summary>
		[JsonPropertyName("externalUrl")]
		public string? ExternalUrl { get; set; }

		/// <summary>
		/// When the chapter was published
		/// </summary>
		[JsonPropertyName("publishAt")]
		public DateTime? PublishAt { get; set; }

		/// <summary>
		/// When the chapter is available to be read.
		/// </summary>
		[JsonPropertyName("readableAt")]
		public DateTime? ReadableAt { get; set; }

		/// <summary>
		/// When the chapter was first created
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// When the chapter was last updated
		/// </summary>
		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// How many pages the chapter contains
		/// </summary>
		[JsonPropertyName("pages")]
		public int Pages { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }

		/// <summary>
		/// The user who uploaded the chapter
		/// </summary>
		[JsonPropertyName("uploader")]
		public string? Uploader { get; set; }
	}
}

/// <summary>
/// Represents a collection of chapters returned by the MD api
/// </summary>
public class ChapterList : MangaDexCollection<Chapter> { }