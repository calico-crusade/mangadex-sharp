namespace MangaDexSharp;

/// <summary>
/// Represents a return result from the MD api that results in a collection of items
/// </summary>
/// <typeparam name="T">The type of items in the colleciton</typeparam>
public class MangaDexCollection<T> : MangaDexRoot<List<T>>
{
	/// <summary>
	/// How many items the return result is limited to
	/// </summary>
	[JsonPropertyName("limit")]
	public int Limit { get; set; }

	/// <summary>
	/// How many items were skipped base on the ordering criteria (pagination stuff)
	/// </summary>
	[JsonPropertyName("offset")]
	public int Offset { get; set; }

	/// <summary>
	/// The total number of records MD has for this collection, scoped to any filters.
	/// </summary>
	[JsonPropertyName("total")]
	public int Total { get; set; }
}
