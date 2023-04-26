namespace MangaDexSharp;

/// <summary>
/// Represents a person in the MD api
/// </summary>
public class PersonRelationship : MangaDexModel<PersonRelationship.PersonRelationshipAttributesModel>, IRelationship, IRelationshipModel
{
	/// <summary>
	/// Any relationships this person has
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// The properties of the person
	/// </summary>
	public class PersonRelationshipAttributesModel : AuthorCreate
	{
		/// <summary>
		/// The persons avatar
		/// </summary>
		[JsonPropertyName("imageUrl")]
		public string? ImageUrl { get; set; }

		/// <summary>
		/// The date the person was created
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The date the person was updated
		/// </summary>
		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }
	}
}

/// <summary>
/// A collection of people
/// </summary>
public class PersonList : MangaDexCollection<PersonRelationship> { }