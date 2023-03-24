namespace MangaDexSharp;

/// <summary>
/// Represents a related item
/// </summary>
public class RelatedDataRelationship : Manga, IRelationship
{
	/// <summary>
	/// How the item is related 
	/// </summary>
	[JsonPropertyName("related")]
	public Relationships Related { get; set; }
}
