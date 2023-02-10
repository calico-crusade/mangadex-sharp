namespace MangaDexSharp;

public class CoverArtRelationship : MangaDexModel<CoverArtRelationship.AttributesModel>, IRelationship, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;

		[JsonPropertyName("volume")]
		public string Volume { get; set; } = string.Empty;

		[JsonPropertyName("fileName")]
		public string FileName { get; set; } = string.Empty;

		[JsonPropertyName("locale")]
		public string Locale { get; set; } = string.Empty;

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

public class CoverArtList : MangaDexCollection<CoverArtRelationship> { }