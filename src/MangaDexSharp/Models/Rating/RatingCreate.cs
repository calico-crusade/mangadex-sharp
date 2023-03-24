namespace MangaDexSharp;

/// <summary>
/// Represents a request to create a rating in the MD api
/// </summary>
public class RatingCreate
{
	/// <summary>
	/// The rating to give the manga
	/// </summary>
	[JsonPropertyName("rating")]
	public int Rating { get; set; }
}
