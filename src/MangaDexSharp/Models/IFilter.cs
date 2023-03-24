namespace MangaDexSharp;

/// <summary>
/// Represents a set of query parameters
/// </summary>
public interface IFilter
{
	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns></returns>
	string BuildQuery();
}

/// <summary>
/// Represents a set of query parameters for paginated request
/// </summary>
public interface IPaginateFilter : IFilter
{
	/// <summary>
	/// The number of records to return for this filter (MAX 100, MIN 1, DEFAULT 100)
	/// </summary>
	int Limit { get; set; }

	/// <summary>
	/// The number of records to skip when using this filter (MIN 0, DEFAULT 0)
	/// </summary>
	int Offset { get; set; }
}