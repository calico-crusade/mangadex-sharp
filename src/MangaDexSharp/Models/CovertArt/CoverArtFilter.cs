namespace MangaDexSharp;

/// <summary>
/// Represents the available query parameters for cover art requests
/// </summary>
public class CoverArtFilter : IPaginateFilter
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
	/// An optional list of manga IDs
	/// </summary>
	public string[] MangaIds { get; set; } = Array.Empty<string>();

	/// <summary>
	/// An optional list of cover art IDs
	/// </summary>
	public string[] Ids { get; set; } = Array.Empty<string>();

	/// <summary>
	/// An optional list of user uploader IDs
	/// </summary>
	public string[] Uploaders { get; set; } = Array.Empty<string>();

	/// <summary>
	/// An optional list of language / locale codes
	/// </summary>
	public string[] Locales { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Determines how to order the return results
	/// </summary>
	public Dictionary<OrderKey, OrderValue> Order { get; set; } = new();

	/// <summary>
	/// What relationships to include in the results
	/// </summary>
	public MangaIncludes[] Includes { get; set; } = new[]
	{
		MangaIncludes.manga,
		MangaIncludes.user
	};

	/// <summary>
	/// Builds the query into 1 string
	/// </summary>
	/// <returns></returns>
	public string BuildQuery()
	{
		return new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("ids", Ids)
			.Add("manga", MangaIds)
			.Add("uploaders", Uploaders)
			.Add("locales", Locales)
			.Add("includes", Includes)
			.Add("order", Order)
			.Build();
	}

	/// <summary>
	/// The fields to sort the query by
	/// </summary>
	public enum OrderKey
	{
		/// <summary>
		/// The name field
		/// </summary>
		name,
		/// <summary>
		/// The updated at field
		/// </summary>
		updatedAt,
		/// <summary>
		/// The volume ordinal field
		/// </summary>
		volume
	}
}
