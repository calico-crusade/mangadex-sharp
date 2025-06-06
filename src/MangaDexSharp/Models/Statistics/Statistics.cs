namespace MangaDexSharp;

/// <summary>
/// The statistics for this entity
/// </summary>
public class Statistics
{
    /// <summary>
    /// Stats about the comments for this entity
    /// </summary>
    [JsonPropertyName("comments")]
    public CommentStats Comments { get; set; } = new();

    /// <summary>
    /// The statistics for comments
    /// </summary>
    public class CommentStats
    {
        /// <summary>
        /// The ID of the forum thread for this entity
        /// </summary>
        [JsonPropertyName("threadId")]
        public long ThreadId { get; set; }

        /// <summary>
        /// The number of replies on the forum thread for this entity
        /// </summary>
        [JsonPropertyName("repliesCount")]
        public int RepliesCount { get; set; }

        /// <summary>
        /// The URL of the forum thread for this entity
        /// </summary>
        [JsonIgnore]
        public string ForumThreadUrl => $"https://forums.mangadex.org/threads/{ThreadId}";
    }
}
