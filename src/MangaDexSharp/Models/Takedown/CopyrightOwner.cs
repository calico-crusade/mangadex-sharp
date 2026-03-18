namespace MangaDexSharp;

/// <summary>
/// The copyright owner of a takedown notice.
/// </summary>
public class CopyrightOwner
{
	/// <summary>
	/// The country of origin for the copyright owner.
	/// </summary>
	[JsonPropertyName("country")]
	public string? Country { get; set; }

	/// <summary>
	/// The full name of the copyright owner.
	/// </summary>
	[JsonPropertyName("fullName")]
	public string? FullName { get; set; }

	/// <summary>
	/// The type of copyright owner.
	/// </summary>
	[JsonPropertyName("ownerType")]
	public string? OwnerType { get; set; }

	/// <summary>
	/// The relationship between this entity and the copyright owner.
	/// </summary>
	[JsonPropertyName("relationship")]
	public string? Relationship { get; set; }
}
