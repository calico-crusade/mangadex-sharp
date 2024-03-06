namespace MangaDexSharp;

/// <summary>
/// The data that can be updated for an API client
/// </summary>
public class ApiClientUpdateData
{
    /// <summary>
    /// The description of the API client
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The version of the API client
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;
}
