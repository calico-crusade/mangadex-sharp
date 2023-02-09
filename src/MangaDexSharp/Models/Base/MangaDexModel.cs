namespace MangaDexSharp;

public abstract class MangaDexModel
{
	[JsonPropertyName("id")]
	public virtual string Id { get; set; } = string.Empty;

	[JsonPropertyName("type")]
	public virtual string Type { get; set; } = string.Empty;
}

public abstract class MangaDexModel<T> : MangaDexModel where T : new()
{
	[JsonPropertyName("attributes")]
	public virtual T Attributes { get; set; } = new();
}
