namespace MangaDexSharp;

/// <summary>
/// Represents the available query parameters for filtering group endpoints
/// </summary>
public class ScanlationGroupFilter : IPaginateFilter
{
    /// <summary>
    /// The default <see cref="Limit"/> used
    /// </summary>
    public static int DefaultLimit { get; set; } = 100;

    /// <summary>
    /// The number of records to return for this filter (MAX 100, MIN 1, DEFAULT 100)
    /// </summary>
    public int Limit { get; set; } = DefaultLimit;

	/// <summary>
	/// The number of records to skip when using this filter (MIN 0, DEFAULT 0)
	/// </summary>
	public int Offset { get; set; } = 0;

	/// <summary>
	/// Only include groups with these Ids
	/// </summary>
	public string[] Ids { get; set; } = [];

	/// <summary>
	/// Only include groups that match this name
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Only include groups that focus on this language
	/// </summary>
	public string? FocusedLanguage { get; set; }

	/// <summary>
	/// Determine how to order the returned groups
	/// </summary>
	public Dictionary<OrderKey, OrderValue> Order { get; set; } = [];

	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns></returns>
	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("ids", Ids)
			.Add("name", Name)
			.Add("focusedLanguage", FocusedLanguage)
			.Add("includes", ["leader", "member"]);

		bob.Add("order", Order);

		return bob.Build();
	}

	/// <summary>
	/// The available fields the can be ordered by
	/// </summary>
	public enum OrderKey
	{
		/// <summary>
		/// Order by the name field
		/// </summary>
		name,
		/// <summary>
		/// Order by the created at field
		/// </summary>
		createdAt,
		/// <summary>
		/// Order by the updated at field
		/// </summary>
		updatedAt,
		/// <summary>
		/// Order by the followed count field
		/// </summary>
		followedCount,
		/// <summary>
		/// Order by the relevance field
		/// </summary>
		relevance
	}
}
