namespace MangaDexSharp;

/// <summary>
/// Represents an item returned by the MD api
/// </summary>
public abstract class MangaDexModel
{
	/// <summary>
	/// The ID of the item
	/// </summary>
	[JsonPropertyName("id")]
	public virtual string Id { get; set; } = string.Empty;

	/// <summary>
	/// The type of item
	/// </summary>
	[JsonPropertyName("type")]
	public virtual string Type { get; set; } = string.Empty;
}

/// <summary>
/// Represents an item returned by the MD API that contains the `attributes` property
/// </summary>
/// <typeparam name="T">The type of attributes</typeparam>
public abstract class MangaDexModel<T> : MangaDexModel where T : new()
{
	/// <summary>
	/// The attributes the item contains
	/// </summary>
	[JsonPropertyName("attributes")]
	public virtual T Attributes { get; set; } = new();
}
