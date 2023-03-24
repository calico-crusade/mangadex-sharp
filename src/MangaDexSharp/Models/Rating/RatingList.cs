namespace MangaDexSharp;

/// <summary>
/// Represents a collection of ratings for a manga
/// </summary>
public class RatingList : MangaDexRoot
{
	/// <summary>
	/// The ratings of the manga
	/// </summary>
	[JsonPropertyName("ratings")]
	public Dictionary<string, Rating> Ratings { get; set; } = new();

	/// <summary>
	/// Represents a rating for a manga
	/// </summary>
	public class Rating
	{
		/// <summary>
		/// The rating value
		/// </summary>
		[JsonPropertyName("rating")]
		public int Value { get; set; }

		/// <summary>
		/// When the rating was created
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime? CreatedAt { get; set; }
	}
}
