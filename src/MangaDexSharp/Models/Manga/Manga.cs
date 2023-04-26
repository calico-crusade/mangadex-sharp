namespace MangaDexSharp;

/// <summary>
/// Represents a manga item returned from the MD api
/// </summary>
public class Manga : MangaDexModel<Manga.MangaAttributesModel>, IRelationshipModel
{
	/// <summary>
	/// All of the relationship items related to this manga
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// All of the properties on this name
	/// </summary>
	public class MangaAttributesModel
	{
		/// <summary>
		/// The primary title of the manga in varying languages
		/// </summary>
		[JsonPropertyName("title")]
		public Localization Title { get; set; } = new();

		/// <summary>
		/// A collection of alternate titles for the manga in varying languages
		/// </summary>
		[JsonPropertyName("altTitles")]
		public Localization[] AltTitles { get; set; } = Array.Empty<Localization>();

		/// <summary>
		/// The description of this manga in varying languages
		/// </summary>
		[JsonPropertyName("description")]
		public Localization Description { get; set; } = new();

		/// <summary>
		/// Whether or not the manga is locked for changes
		/// </summary>
		[JsonPropertyName("isLocked")]
		public bool IsLocked { get; set; }

		/// <summary>
		/// Any links for this manga
		/// </summary>
		[JsonPropertyName("links")]
		public Localization Links { get; set; } = new();

		/// <summary>
		/// The original language the manga was written in
		/// </summary>
		[JsonPropertyName("originalLanguage")]
		public string OriginalLanguage { get; set; } = string.Empty;

		/// <summary>
		/// The ordinal for the last volume in the manga
		/// </summary>
		[JsonPropertyName("lastVolume")]
		public string LastVolume { get; set; } = string.Empty;

		/// <summary>
		/// The ordinal for the last chapter in the manga
		/// </summary>
		[JsonPropertyName("lastChapter")]
		public string LastChapter { get; set; } = string.Empty;

		/// <summary>
		/// What demographic the manga is targetting
		/// </summary>
		[JsonPropertyName("publicationDemographic")]
		public Demographic? PublicationDemographic { get; set; }

		/// <summary>
		/// The publication status of the manga
		/// </summary>
		[JsonPropertyName("status")]
		public Status? Status { get; set; }

		/// <summary>
		/// The year the manga was released in it's original language
		/// </summary>
		[JsonPropertyName("year")]
		public int? Year { get; set; }

		/// <summary>
		/// The content rating of the manga
		/// </summary>
		[JsonPropertyName("contentRating")]
		public ContentRating? ContentRating { get; set; }

		/// <summary>
		/// Any tags associated with this manga
		/// </summary>
		[JsonPropertyName("tags")]
		public MangaTag[] Tags { get; set; } = Array.Empty<MangaTag>();

		/// <summary>
		/// The publication state of the manga
		/// </summary>
		[JsonPropertyName("state")]
		public string State { get; set; } = string.Empty;

		/// <summary>
		/// Whether or not the chapter ordinals reset when a new volume is released.
		/// </summary>
		[JsonPropertyName("chapterNumbersResetOnNewVolume")]
		public bool ChapterNumbersResetOnNewVolume { get; set; }

		/// <summary>
		/// The date the manga was first created on MD
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The date the manga was last updated on MD (not including chapter updates)
		/// </summary>
		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }

		/// <summary>
		/// All of the languages this manga is available in
		/// </summary>
		[JsonPropertyName("availableTranslatedLanguages")]
		public string[] AvailableTranslatedLanguages { get; set; } = Array.Empty<string>();

		/// <summary>
		/// The ID of the latest chapter that was uploaded
		/// </summary>
		[JsonPropertyName("latestUploadedChapter")]
		public string LatestUploadedChapter { get; set; } = string.Empty;
	}
}

/// <summary>
/// Represents a collection of manga returned by the MD api
/// </summary>
public class MangaList : MangaDexCollection<Manga> { }