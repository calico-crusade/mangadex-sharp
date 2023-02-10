namespace MangaDexSharp;

public class RatingList : MangaDexRoot
{
	[JsonPropertyName("ratings")]
	public Dictionary<string, Rating> Ratings { get; set; } = new();

	public class Rating
	{
		[JsonPropertyName("rating")]
		public int Value { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime? CreatedAt { get; set; }
	}
}
