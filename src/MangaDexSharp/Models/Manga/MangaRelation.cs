namespace MangaDexSharp;

public class MangaRelation
{
	[JsonPropertyName("targetManga")]
	public string TargetManga { get; set; } = string.Empty;

	[JsonPropertyName("relation")]
	public Relationships Relation { get; set; }
}
