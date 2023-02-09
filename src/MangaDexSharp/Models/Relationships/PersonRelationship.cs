namespace MangaDexSharp;

public class PersonRelationship : MangaDexModel<PersonRelationship.AttributesModel>, IRelationship
{
	public class AttributesModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("imageUrl")]
		public string? ImageUrl { get; set; }

		[JsonPropertyName("twitter")]
		public string? Twitter { get; set; }

		[JsonPropertyName("pixiv")]
		public string? Pixiv { get; set; }

		[JsonPropertyName("melonBook")]
		public string? MelonBook { get; set; }

		[JsonPropertyName("fanBox")]
		public string? FanBox { get; set; }

		[JsonPropertyName("booth")]
		public string? Booth { get; set; }

		[JsonPropertyName("nicoVideo")]
		public string? NicoVideo { get; set; }

		[JsonPropertyName("skeb")]
		public string? Skeb { get; set; }

		[JsonPropertyName("fantia")]
		public string? Fantia { get; set; }

		[JsonPropertyName("tumblr")]
		public string? Tumblr { get; set; }

		[JsonPropertyName("youtube")]
		public string? Youtube { get; set; }

		[JsonPropertyName("weibo")]
		public string? Weibo { get; set; }

		[JsonPropertyName("naver")]
		public string? Naver { get; set; }

		[JsonPropertyName("website")]
		public string? Website { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime CreatedAt { get; set; }

		[JsonPropertyName("updatedAt")]
		public DateTime UpdatedAt { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}
