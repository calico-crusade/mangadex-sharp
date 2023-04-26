namespace MangaDexSharp;

/// <summary>
/// Represents a tag associated with a manga
/// </summary>
public class MangaTag : MangaDexModel<MangaTag.MangaTagAttributesModel>, IRelationshipModel
{
	/// <summary>
	/// All of the related items for this tag
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// All of the properties on the tag
	/// </summary>
	public class MangaTagAttributesModel
	{
		/// <summary>
		/// The name of the tag in varying languages
		/// </summary>
		[JsonPropertyName("name")]
		public Localization Name { get; set; } = new();

		/// <summary>
		/// A description of the tag in varying languages
		/// </summary>
		[JsonPropertyName("description")]
		public Localization Description { get; set; } = new();

		/// <summary>
		/// How to group the tag
		/// </summary>
		[JsonPropertyName("group")]
		public Group Group { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a collection of tags
/// </summary>
public class TagList : MangaDexCollection<MangaTag> { }