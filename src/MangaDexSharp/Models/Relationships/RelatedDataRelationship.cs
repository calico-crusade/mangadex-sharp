namespace MangaDexSharp;

public class RelatedDataRelationship : Manga, IRelationship
{
	[JsonPropertyName("related")]
	public Relationships Related { get; set; }
}
