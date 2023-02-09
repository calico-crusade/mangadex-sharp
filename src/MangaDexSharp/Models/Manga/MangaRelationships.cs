namespace MangaDexSharp;

public class MangaRelationship : MangaDexModel<MangaRelationship.AttributesModel>, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("relation")]
		public Relationships Relation { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

public class MangaRelationships : MangaDexCollection<MangaRelationship> { }
