namespace MangaDexSharp;

/// <summary>
/// Represents a relationship between two manga
/// </summary>
public class MangaRelationship : MangaDexModel<MangaRelationship.AttributesModel>, IRelationshipModel
{
	/// <summary>
	/// The related items
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// The properties of the relationship
	/// </summary>
	public class AttributesModel
	{
		/// <summary>
		/// The type of relationship
		/// </summary>
		[JsonPropertyName("relation")]
		public Relationships Relation { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a collection of manga relationships
/// </summary>
public class MangaRelationships : MangaDexCollection<MangaRelationship> { }
