namespace MangaDexSharp;

/// <summary>
/// Represents a takedown notice returned from the MD api
/// </summary>
public class Takedown : MangaDexModel<Takedown.TakedownAttributes>, IRelationshipModel
{
	/// <summary>
	/// All of the relationship items related to this takedown notice
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = [];

	/// <summary>
	/// All of the properties on this takedown notice
	/// </summary>
	public class TakedownAttributes
	{
		/// <summary>
		/// Where to find the original work that was taken down
		/// </summary>
		[JsonPropertyName("originalWork")]
		public string? OriginalWork { get; set; }

		/// <summary>
		/// The owner of the copyright work
		/// </summary>
		[JsonPropertyName("publicCopyrightOwnerInfo")]
		public CopyrightOwner? Owner { get; set; }

		/// <summary>
		/// The preferred action for the takedown notice.
		/// </summary>
		[JsonPropertyName("preferredAction")]
		public string? PreferredAction { get; set; }

		/// <summary>
		/// The date the takedown notice was first created on MD
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// The date the takedown notice was last updated on MD (not including chapter updates)
		/// </summary>
		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }

		/// <summary>
		/// Whether or not the takedown notice is permanent.
		/// </summary>
		[JsonPropertyName("isPermanent")]
		public bool IsPermanent { get; set; }
	}
}

/// <summary>
/// Represents a collection of takedown requests returned by the MD api
/// </summary>
public class TakedownList : MangaDexCollection<Takedown> { }
