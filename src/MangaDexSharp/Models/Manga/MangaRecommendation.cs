namespace MangaDexSharp;

/// <summary>
/// Represents a recommendation item returned from the MD api
/// </summary>
public class MangaRecommendation : MangaDexModel<MangaRecommendation.RecommendationAttributes>, IRelationshipModel, IRelationship
{
    /// <summary>
    /// All of the relationship items related to this recommendation
    /// </summary>
    [JsonPropertyName("relationships")]
    public IRelationship[] Relationships { get; set; } = [];

    /// <summary>
    /// All of the properties on this recommendation
    /// </summary>
    public class RecommendationAttributes
    {
        /// <summary>
        /// How closely the related manga is to the original manga
        /// </summary>
        [JsonPropertyName("score")]
        public double Score { get; set; }
    }
}

/// <summary>
/// Represents a collection of recommendations returned by the MD api
/// </summary>
public class RecommendationList : MangaDexCollection<MangaRecommendation> { }
