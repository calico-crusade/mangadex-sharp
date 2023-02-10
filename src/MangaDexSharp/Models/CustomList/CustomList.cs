namespace MangaDexSharp;

public class CustomList : MangaDexModel<CustomList.AttributesModel>, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("visibility")]
		public Visibility Visibility { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

public class CustomListList : MangaDexCollection<CustomList> { }
