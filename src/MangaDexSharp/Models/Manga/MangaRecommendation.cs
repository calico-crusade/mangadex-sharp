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

/// <summary>
/// Represents the available query parameters for manga recommendations
/// </summary>
public class MangaRecommendationFilter
{
	/// <summary>
	/// Determine what to include in the return results of the manga recommendation
	/// </summary>
	public MangaIncludes[] Includes { get; set; } = [MangaIncludes.manga];

	/// <summary>
	/// Only include recommendations with these content ratings
	/// </summary>
	public ContentRating[] ContentRating { get; set; } = ContentRatingsAll;

	/// <summary>
	/// Determine how to order the returned recommendations
	/// </summary>
	public Dictionary<OrderKey, OrderValue> Order { get; set; } = [];

	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns>The query parameters</returns>
	public string BuildQuery()
	{
		return new FilterBuilder()
			.Add("includes", Includes)
			.Add("contentRating", ContentRating)
			.Add("order", Order)
			.Build();
	}

	/// <summary>
	/// The available fields the can be ordered by
	/// </summary>
	public enum OrderKey
	{
		/// <summary>
		/// Order by the recommendation score
		/// </summary>
		score
	}
}
