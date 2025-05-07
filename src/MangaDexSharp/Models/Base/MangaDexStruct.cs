namespace MangaDexSharp;

/// <summary>
/// Represents the return results of the MD api endpoints
/// </summary>
/// <typeparam name="T"></typeparam>
public class MangaDexStruct<T> : MangaDexRoot
{
    /// <summary>
    /// A response code for the results
    /// </summary>
    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;

    /// <summary>
    /// The return results of the endpoint
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }
}
