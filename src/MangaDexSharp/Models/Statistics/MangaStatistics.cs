namespace MangaDexSharp;

/// <summary>
/// Represents statistics returned by the MD api for Manga specifically
/// </summary>
public class MangaStatistics : MangaDexRoot
{
    /// <summary>
    /// All of the statistics for the entities
    /// </summary>
    [JsonPropertyName("statistics")]
    public Dictionary<string, MStatistics> Statistics { get; set; } = [];

    /// <summary>
    /// Statis related to the manga's rating
    /// </summary>
    public class RatingStats
    {
        /// <summary>
        /// The average rating for the manga
        /// </summary>
        [JsonPropertyName("average")]
        public double Average { get; set; }

        /// <summary>
        /// The bayesian rating for the manga
        /// </summary>
        [JsonPropertyName("bayesian")]
        public double Bayesian { get; set; }

        /// <summary>
        /// The distribution of ratings for the manga
        /// </summary>
        [JsonPropertyName("distribution")]
        public Dictionary<string, double> Distribution { get; set; } = [];
    }

    /// <inheritdoc cref="MangaStatistics" />
    public class MStatistics : Statistics
    {
        /// <summary>
        /// The rating for the manga
        /// </summary>
        [JsonPropertyName("rating")]
        public RatingStats Rating { get; set; } = new();

        /// <summary>
        /// The number of people following the manga
        /// </summary>
        [JsonPropertyName("follows")]
        public int Follows { get; set; }

        /// <summary>
        /// The number of chapters that are unavailable for the manga
        /// </summary>
        [JsonPropertyName("unavailableChaptersCount")]
        public int UnavailableChaptersCount { get; set; }
    }
}
