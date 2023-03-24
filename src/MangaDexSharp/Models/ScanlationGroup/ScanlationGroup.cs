namespace MangaDexSharp;

/// <summary>
/// Represents a scanlation group in the MD api
/// </summary>
public class ScanlationGroup : MangaDexModel<ScanlationGroup.AttributesModel>, IRelationship, IRelationshipModel
{
	/// <summary>
	/// All of the related items to this group
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

	/// <summary>
	/// The properties of the scanlation group
	/// </summary>
	public class AttributesModel
	{
		/// <summary>
		/// The name of the group
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Any alternate names in varying languages
		/// </summary>
		[JsonPropertyName("altNames")]
		public Localization[] AltNames { get; set; } = Array.Empty<Localization>();

		/// <summary>
		/// Whether or not the group is locked for changes
		/// </summary>
		[JsonPropertyName("locked")]
		public bool Locked { get; set; }

		/// <summary>
		/// The groups Website URL
		/// </summary>
		[JsonPropertyName("website")]
		public string? Website { get; set; }

		/// <summary>
		/// The groups IRC server
		/// </summary>
		[JsonPropertyName("ircServer")]
		public string? IrcServer { get; set; }

		/// <summary>
		/// The groups IRC channel
		/// </summary>
		[JsonPropertyName("ircChannel")]
		public string? IrcChannel { get; set; }

		/// <summary>
		/// The groups discord server
		/// </summary>
		[JsonPropertyName("discord")]
		public string? Discord { get; set; }

		/// <summary>
		/// The groups contact email
		/// </summary>
		[JsonPropertyName("contactEmail")]
		public string? ContactEmail { get; set; }

		/// <summary>
		/// A description of the group
		/// </summary>
		[JsonPropertyName("Description")]
		public string? Description { get; set; }

		/// <summary>
		/// The groups twitter 
		/// </summary>
		[JsonPropertyName("twitter")]
		public string? Twitter { get; set; }
		
		/// <summary>
		/// The groups manga updates url
		/// </summary>
		[JsonPropertyName("mangaUpdates")]
		public string? MangaUpdates { get; set; }

		/// <summary>
		/// The languages the group translates
		/// </summary>
		[JsonPropertyName("focusedLanguages")]
		public string[] FocusedLanguages { get; set; } = Array.Empty<string>();

		/// <summary>
		/// Whether or not the group is an official publisher
		/// </summary>
		[JsonPropertyName("official")]
		public bool Official { get; set; }

		/// <summary>
		/// Whether or not the group is verified
		/// </summary>
		[JsonPropertyName("verified")]
		public bool Verified { get; set; }

		/// <summary>
		/// Whether or not the group is inactive
		/// </summary>
		[JsonPropertyName("inactive")]
		public bool Inactive { get; set; }

		/// <summary>
		/// When the group was created
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// When the group was updated
		/// </summary>
		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a collection of scanlation groups
/// </summary>
public class ScanlationGroupList : MangaDexCollection<ScanlationGroup> { }