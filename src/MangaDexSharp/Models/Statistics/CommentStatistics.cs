namespace MangaDexSharp;

/// <summary>
/// Represents statistics returned by the MD api
/// </summary>
public class CommentStatistics : MangaDexRoot
{
    /// <summary>
    /// All of the statistics for the entities
    /// </summary>
    [JsonPropertyName("statistics")]
    public Dictionary<string, Statistics> Statistics { get; set; } = [];
}
