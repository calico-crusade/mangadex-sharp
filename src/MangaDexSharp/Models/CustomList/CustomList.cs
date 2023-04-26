namespace MangaDexSharp;

/// <summary>
/// Represents a custom list of entities from the MD api
/// </summary>
public class CustomList : MangaDexModel<CustomList.CustomListAttributesModel>, IRelationshipModel
{
	/// <summary>
	/// All of the related items for this list
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// All of the attributes the list has
	/// </summary>
	public class CustomListAttributesModel
	{
		/// <summary>
		/// The name of the list
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// The visibility of the list (public / private)
		/// </summary>
		[JsonPropertyName("visibility")]
		public Visibility Visibility { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a list of custom lists returned by the MD api
/// </summary>
public class CustomListList : MangaDexCollection<CustomList> { }
