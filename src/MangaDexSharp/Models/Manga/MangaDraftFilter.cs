namespace MangaDexSharp;

/// <summary>
/// Represents a query paramter filter for the drafts endpoints
/// </summary>
public class MangaDraftFilter : IPaginateFilter
{
	/// <summary>
	/// The number of records to return for this filter (MAX 100, MIN 1, DEFAULT 100)
	/// </summary>
	public int Limit { get; set; } = 100;

	/// <summary>
	/// The number of records to skip when using this filter (MIN 0, DEFAULT 0)
	/// </summary>
	public int Offset { get; set; } = 0;

	/// <summary>
	/// The state of the draft
	/// </summary>
	public DraftStatus? State { get; set; }

	/// <summary>
	/// How to roder the results of the query
	/// </summary>
	public OrderValue? CreatedAtOrder { get; set; }

	/// <summary>
	/// What relationships to include in the results
	/// </summary>
	public MangaIncludes[] Includes { get; set; } = new[]
	{
		MangaIncludes.manga,
		MangaIncludes.cover_art,
		MangaIncludes.author,
		MangaIncludes.artist,
		MangaIncludes.tag
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
			.Add("state", State)
			.Add("includes", Includes);

		if (CreatedAtOrder != null)
			bob.Add("order", new Dictionary<MangaFilter.OrderKey, OrderValue> 
			{
				[MangaFilter.OrderKey.createdAt] = CreatedAtOrder ?? OrderValue.desc
			});

		return bob.Build();
	}
}
