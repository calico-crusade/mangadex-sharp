namespace MangaDexSharp;

public class Chapter : MangaDexModel<Chapter.AttributesModel>, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("volume")]
		public string Volume { get; set; } = string.Empty;

		[JsonPropertyName("chapter")]
		public string Chapter { get; set; } = string.Empty;

		[JsonPropertyName("title")]
		public string Title { get; set; } = string.Empty;

		[JsonPropertyName("translatedLanguage")]
		public string TranslatedLanguage { get; set; } = string.Empty;

		[JsonPropertyName("externalUrl")]
		public string? ExternalUrl { get; set; }

		[JsonPropertyName("publishAt")]
		public DateTime PublishAt { get; set; }

		[JsonPropertyName("readableAt")]
		public DateTime ReadableAt { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		[JsonPropertyName("pages")]
		public int Pages { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }

		[JsonPropertyName("uploader")]
		public string? Uploader { get; set; }
	}
}

public class ChapterList : MangaDexCollection<Chapter> { }