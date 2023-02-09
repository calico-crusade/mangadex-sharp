namespace MangaDexSharp;

public class Manga : MangaDexModel<Manga.AttributesModel>, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("title")]
		public Localization Title { get; set; } = new();

		[JsonPropertyName("altTitles")]
		public Localization[] AltTitles { get; set; } = Array.Empty<Localization>();

		[JsonPropertyName("description")]
		public Localization Description { get; set; } = new();

		[JsonPropertyName("isLocked")]
		public bool IsLocked { get; set; }

		[JsonPropertyName("links")]
		public Localization Links { get; set; } = new();

		[JsonPropertyName("originalLanguage")]
		public string OriginalLanguage { get; set; } = string.Empty;

		[JsonPropertyName("lastVolume")]
		public string LastVolume { get; set; } = string.Empty;

		[JsonPropertyName("lastChapter")]
		public string LastChapter { get; set; } = string.Empty;

		[JsonPropertyName("publicationDemographic")]
		public string PublicationDemographic { get; set; } = string.Empty;

		[JsonPropertyName("status")]
		public string Status { get; set; } = string.Empty;

		[JsonPropertyName("year")]
		public int? Year { get; set; }

		[JsonPropertyName("contentRating")]
		public string ContentRating { get; set; } = string.Empty;

		[JsonPropertyName("tags")]
		public Tag[] Tags { get; set; } = Array.Empty<Tag>();

		[JsonPropertyName("state")]
		public string State { get; set; } = string.Empty;

		[JsonPropertyName("chapterNumbersResetOnNewVolume")]
		public bool ChapterNumbersResetOnNewVolume { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }

		[JsonPropertyName("availableTranslatedLanguages")]
		public string[] AvailableTranslatedLanguages { get; set; } = Array.Empty<string>();

		[JsonPropertyName("latestUploadedChapter")]
		public string LatestUploadedChapter { get; set; } = string.Empty;
	}

	public class Tag : MangaDexModel<Tag.AttributesModel>
	{
		public class AttributesModel
		{
			[JsonPropertyName("name")]
			public Localization Name { get; set; } = new();

			[JsonPropertyName("description")]
			public Localization Description { get; set; } = new();

			[JsonPropertyName("group")]
			public string Group { get; set; } = string.Empty;

			[JsonPropertyName("version")]
			public int Version { get; set; }
		}
	}
}

public class MangaList : MangaDexCollection<Manga> { }