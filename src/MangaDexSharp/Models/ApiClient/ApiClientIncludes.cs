namespace MangaDexSharp;

/// <summary>
/// Relationships that can be included in the API client return results
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<ApiClientIncludes>))]
public enum ApiClientIncludes
{
    /// <summary>
    /// The user that created the client
    /// </summary>
    creator,
}
