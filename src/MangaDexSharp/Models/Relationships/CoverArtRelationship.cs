namespace MangaDexSharp;

/// <summary>
/// Represents the relationship between cover art and the parent manga
/// </summary>
public class CoverArtRelationship : MangaDexModel<CoverArtRelationship.CoverArtAttributesModel>, IRelationship, IRelationshipModel
{
	/// <summary>
	/// All of the related items for the cover art
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// All of the properties the cover art has
	/// </summary>
	public class CoverArtAttributesModel
	{
		/// <summary>
		/// The description of the cover art
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// The volume the cover art belongs to
		/// </summary>
		[JsonPropertyName("volume")]
		public string Volume { get; set; } = string.Empty;

		/// <summary>
		/// The file name of the cover art
		/// </summary>
		[JsonPropertyName("fileName")]
		public string FileName { get; set; } = string.Empty;

		/// <summary>
		/// The language / locale code the cover art belongs to
		/// </summary>
		[JsonPropertyName("locale")]
		public string Locale { get; set; } = string.Empty;
		
		/// <summary>
		/// When the cover art item was created
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// When the cover art was last updated
		/// </summary>
		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// The request version
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a list of cover arts
/// </summary>
public class CoverArtList : MangaDexCollection<CoverArtRelationship> { }