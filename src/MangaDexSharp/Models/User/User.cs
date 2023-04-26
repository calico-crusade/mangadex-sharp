namespace MangaDexSharp;

/// <summary>
/// Represents a user in the MD api
/// </summary>
public class User : MangaDexModel<User.UserAttributesModel>, IRelationship, IRelationshipModel
{
	/// <summary>
	/// All of the related items 
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// The properties of the user
	/// </summary>
	public class UserAttributesModel
	{
		/// <summary>
		/// The user's name
		/// </summary>
		[JsonPropertyName("username")]
		public string Username { get; set; } = string.Empty;

		/// <summary>
		/// The roles the user has
		/// </summary>
		[JsonPropertyName("roles")]
		public string[] Roles { get; set; } = Array.Empty<string>();

		/// <summary>
		/// The version of this request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a list of users
/// </summary>
public class UserList : MangaDexCollection<User> { }