namespace MangaDexSharp;

/// <summary>
/// The state of the API client
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<ApiClientState>))]
public enum ApiClientState
{
    /// <summary>
    /// The client is pending approval
    /// </summary>
    requested,
    /// <summary>
    /// The client was manually approved
    /// </summary>
    approved,
    /// <summary>
    /// The client was rejected
    /// </summary>
    rejected,
    /// <summary>
    /// The client was automatically approved
    /// </summary>
    autoapproved
}
