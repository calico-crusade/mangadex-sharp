namespace MangaDexSharp;

/// <summary>
/// Represents the available query parameters for the report endpoint
/// </summary>
public class ReportFilter : IPaginateFilter
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
	/// Only include reports of this type
	/// </summary>
	public ReportCategory? Category { get; set; }

	/// <summary>
	/// Only include reports of this reason ID
	/// </summary>
	public string? ReasonId { get; set; }

	/// <summary>
	/// Only include reports for this object
	/// </summary>
	public string? ObjectId { get; set; }

	/// <summary>
	/// Only include reports of this state
	/// </summary>
	public ReportState? State { get; set; }

	/// <summary>
	/// Order the returned reports by the created at date
	/// </summary>
	public OrderValue? CreatedAtOrder { get; set; }

	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns></returns>
	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("category", Category)
			.Add("reasonId", ReasonId)
			.Add("objectId", ObjectId)
			.Add("status", State)
			.Add("includes", new[] { "user", "reason" });

		if (CreatedAtOrder != null)
			bob.Add("order", new Dictionary<MangaFilter.OrderKey, OrderValue>
			{
				[MangaFilter.OrderKey.createdAt] = CreatedAtOrder ?? OrderValue.asc
			});

		return bob.Build();
	}
}
