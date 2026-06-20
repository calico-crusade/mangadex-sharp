namespace MangaDexSharp;

/// <summary>
/// Represents the type of legacy ID mapping to request
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<LegacyMappingType>))]
public enum LegacyMappingType
{
	/// <summary>
	/// Maps legacy scanlation group IDs
	/// </summary>
	group,
	/// <summary>
	/// Maps legacy manga IDs
	/// </summary>
	manga,
	/// <summary>
	/// Maps legacy chapter IDs
	/// </summary>
	chapter,
	/// <summary>
	/// Maps legacy tag IDs
	/// </summary>
	tag
}

/// <summary>
/// Represents a request to map legacy MangaDex IDs to current UUIDs
/// </summary>
public class LegacyMappingRequest
{
	/// <summary>
	/// The type of resource to map
	/// </summary>
	[JsonPropertyName("type")]
	public LegacyMappingType Type { get; set; }

	/// <summary>
	/// The legacy IDs to map
	/// </summary>
	[JsonPropertyName("ids")]
	public int[] Ids { get; set; } = [];
}

/// <summary>
/// Represents a legacy ID mapping result
/// </summary>
public class LegacyMapping : MangaDexModel<LegacyMapping.LegacyMappingAttributes>, IRelationshipModel
{
	/// <summary>
	/// The relationships included with the mapping
	/// </summary>
	[JsonPropertyName("relationships")]
	public IRelationship[] Relationships { get; set; } = [];

	/// <summary>
	/// The attributes of a legacy ID mapping
	/// </summary>
	public class LegacyMappingAttributes
	{
		/// <summary>
		/// The type of resource that was mapped
		/// </summary>
		[JsonPropertyName("type")]
		public LegacyMappingType Type { get; set; }

		/// <summary>
		/// The original legacy ID
		/// </summary>
		[JsonPropertyName("legacyId")]
		public int LegacyId { get; set; }

		/// <summary>
		/// The current MangaDex UUID
		/// </summary>
		[JsonPropertyName("newId")]
		public string NewId { get; set; } = string.Empty;
	}
}

/// <summary>
/// Represents a collection of legacy ID mapping results
/// </summary>
public class LegacyMappingList : MangaDexCollection<LegacyMapping> { }
