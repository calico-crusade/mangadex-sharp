namespace MangaDexSharp;

public class MangaTag : MangaDexModel<MangaTag.AttributesModel>, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("name")]
		public Localization Name { get; set; } = new();

		[JsonPropertyName("description")]
		public Localization Description { get; set; } = new();

		[JsonPropertyName("group")]
		public Group Group { get; set; }

		public int Version { get; set; }
	}
}


public class TagList : MangaDexCollection<MangaTag> { }