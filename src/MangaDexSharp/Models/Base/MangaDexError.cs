namespace MangaDexSharp;

/// <summary>
/// Represents an error returned by the MD api
/// </summary>
public class MangaDexError
{
    /// <summary>
    /// The ID of the error message (Can be used for debugging within #dev-talk-api)
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The status code for the error
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// A brief description of the error
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// A more detailed description of the error
    /// </summary>
    [JsonPropertyName("detail")]
    public string Detail { get; set; } = string.Empty;
}
