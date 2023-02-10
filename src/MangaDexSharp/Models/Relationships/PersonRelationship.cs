namespace MangaDexSharp;

public class PersonRelationship : MangaDexModel<PersonRelationship.AttributesModel>, IRelationship, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel : AuthorCreate
	{
		[JsonPropertyName("imageUrl")]
		public string? ImageUrl { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }
	}
}

public class PersonList : MangaDexCollection<PersonRelationship> { }