namespace MangaDexSharp;

/// <summary>
/// Represents the available query parameters for the API client endpoint
/// </summary>
public class ApiClientFilter : IPaginateFilter
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
    /// The state of the API client
    /// </summary>
    public ApiClientState? State { get; set; }

    /// <summary>
    /// The name of the API client
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Determine what to include in the return results of the API client
    /// </summary>
    public ApiClientIncludes[] Includes { get; set; } = new[] { ApiClientIncludes.creator };

    /// <summary>
    /// Determine how to order the returned results
    /// </summary>
    public Dictionary<OrderKey, OrderValue> Order { get; set; } = new();

    /// <summary>
    /// Builds the query parameters for the URL
    /// </summary>
    /// <returns></returns>
    public string BuildQuery()
    {
        return new FilterBuilder()
            .Add("limit", Limit)
            .Add("offset", Offset)
            .Add("state", State)
            .Add("name", Name)
            .Add("includes", Includes)
            .Add("order", Order)
            .Build();
    }

    /// <summary>
    /// The available fields the can be ordered by
    /// </summary>
    public enum OrderKey
    {
        /// <summary>
        /// Order by the created at field
        /// </summary>
        createdAt,
        /// <summary>
        /// Order by the updated at field
        /// </summary>
        updatedAt,
        /// <summary>
        /// Order by the name field
        /// </summary>
        name,
    }
}
