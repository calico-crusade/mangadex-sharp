namespace MangaDexSharp;

public class UserRelationship : MangaDexModel<UserRelationship.AttributesModel>, IRelationship
{
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
