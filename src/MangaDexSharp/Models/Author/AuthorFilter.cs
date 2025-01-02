namespace MangaDexSharp;

/// <summary>
/// Represents a query parameter filter for the authors endpoints
/// </summary>
public class AuthorFilter : IPaginateFilter
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
	/// An optional list of author IDs
	/// </summary>
	public string[] Ids { get; set; } = Array.Empty<string>();

	/// <summary>
	/// How to order the results of the query (DEFAULT none)
	/// </summary>
	public OrderValue? NameOrder { get; set; }

	/// <summary>
	/// What relationships to include in the resutls (DEFAULT manga)
	/// </summary>
	public MangaIncludes[] Includes { get; set; } = new[]
	{
		MangaIncludes.manga
	};

	/// <summary>
	/// Builds the query into 1 string
	/// </summary>
	/// <returns></returns>
	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("ids", Ids)
			.Add("includes", Includes);

		if (NameOrder != null)
			bob.Add("order", new Dictionary<OrderKey, OrderValue>
			{
				[OrderKey.name] = NameOrder ?? OrderValue.desc
			});

		return bob.Build();
	}

	/// <summary>
	/// [INTERNAL] Used as the name of the name-order key
	/// </summary>
	public enum OrderKey
	{
		/// <summary>
		/// Ordering on the `name` property
		/// </summary>
		name
	}
}