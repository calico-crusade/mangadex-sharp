namespace MangaDexSharp;

/// <summary>
/// The data for creating a new API client
/// </summary>
public class ApiClientData : ApiClientUpdateData
{
    /// <summary>
    /// The name of the API client
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;


    /// <summary>
    /// The type of API client
    /// </summary>
    [JsonPropertyName("profile")]
    public string Profile { get; set; } = "personal";
}
