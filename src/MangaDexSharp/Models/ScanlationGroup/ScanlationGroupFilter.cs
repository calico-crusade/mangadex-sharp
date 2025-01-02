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
	public string[] Ids { get; set; } = Array.Empty<string>();

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
	public OrderValue? LatestUploadedChapterOrder { get; set; }

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
			.Add("includes", new[] { "leader", "member" });

		if (LatestUploadedChapterOrder != null)
			bob.Add("order", new Dictionary<MangaFilter.OrderKey, OrderValue>
			{
				[MangaFilter.OrderKey.latestUploadedChapter] = LatestUploadedChapterOrder ?? OrderValue.desc
			});

		return bob.Build();
	}
}
