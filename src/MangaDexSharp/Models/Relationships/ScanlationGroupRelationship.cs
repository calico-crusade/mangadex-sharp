namespace MangaDexSharp;

public class ScanlationGroupRelationship : MangaDexModel<ScanlationGroupRelationship.AttributesModel>, IRelationship, IRelationshipModel
{
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	public class AttributesModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("locked")]
		public bool Locked { get; set; }

		[JsonPropertyName("website")]
		public string? Website { get; set; }

		[JsonPropertyName("ircServer")]
		public string? IrcServer { get; set; }

		[JsonPropertyName("ircChannel")]
		public string? IrcChannel { get; set; }

		[JsonPropertyName("discord")]
		public string? Discord { get; set; }

		[JsonPropertyName("contactEmail")]
		public string? ContactEmail { get; set; }

		[JsonPropertyName("Description")]
		public string? Description { get; set; }

		[JsonPropertyName("twitter")]
		public string? Twitter { get; set; }

		[JsonPropertyName("mangaUpdates")]
		public string? MangaUpdates { get; set; }

		[JsonPropertyName("focusedLanguages")]
		public string[] FocusedLanguages { get; set; } = Array.Empty<string>();

		[JsonPropertyName("official")]
		public bool Official { get; set; }

		[JsonPropertyName("verified")]
		public bool Verified { get; set; }

		[JsonPropertyName("inactive")]
		public bool Inactive { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

public class ScanlationGroupList : MangaDexCollection<ScanlationGroupRelationship> { }