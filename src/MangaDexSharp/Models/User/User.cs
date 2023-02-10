namespace MangaDexSharp;

public class User : MangaDexModel<User.AttributesModel>, IRelationship, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("username")]
		public string Username { get; set; } = string.Empty;

		[JsonPropertyName("roles")]
		public string[] Roles { get; set; } = Array.Empty<string>();

		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

public class UserList : MangaDexCollection<User> { }