namespace MangaDexSharp;

/// <summary>
/// Represents a request to create an author
/// </summary>
public class AuthorCreate
{
	/// <summary>
	/// The authors name
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// The authors biography localized to different langauges
	/// </summary>
	[JsonPropertyName("biography")]
	public Localization Biography { get; set; } = new();

	/// <summary>
	/// The authors twitter handle
	/// </summary>
	[JsonPropertyName("twitter")]
	public string? Twitter { get; set; }

	/// <summary>
	/// The authors pixiv ID
	/// </summary>
	[JsonPropertyName("pixiv")]
	public string? Pixiv { get; set; }

	/// <summary>
	/// The authors Melon book ID
	/// </summary>
	[JsonPropertyName("melonBook")]
	public string? MelonBook { get; set; }

	/// <summary>
	/// The authors fanbox ID
	/// </summary>
	[JsonPropertyName("fanBox")]
	public string? FanBox { get; set; }

	/// <summary>
	/// The authors boot ID
	/// </summary>
	[JsonPropertyName("booth")]
	public string? Booth { get; set; }

	/// <summary>
	/// The authors NicoVideo ID
	/// </summary>
	[JsonPropertyName("nicoVideo")]
	public string? NicoVideo { get; set; }

	/// <summary>
	/// The authors SKEB ID
	/// </summary>
	[JsonPropertyName("skeb")]
	public string? Skeb { get; set; }

	/// <summary>
	/// The authors fantia ID
	/// </summary>
	[JsonPropertyName("fantia")]
	public string? Fantia { get; set; }

	/// <summary>
	/// The authors tumblr ID
	/// </summary>
	[JsonPropertyName("tumblr")]
	public string? Tumblr { get; set; }

	/// <summary>
	/// The authors YouTube
	/// </summary>
	[JsonPropertyName("youtube")]
	public string? Youtube { get; set; }

	/// <summary>
	/// The authors Weibo ID
	/// </summary>
	[JsonPropertyName("weibo")]
	public string? Weibo { get; set; }

	/// <summary>
	/// The authors naver ID
	/// </summary>
	[JsonPropertyName("naver")]
	public string? Naver { get; set; }

	/// <summary>
	/// The authors website URL
	/// </summary>
	[JsonPropertyName("website")]
	public string? Website { get; set; }

	/// <summary>
	/// The version of the request
	/// </summary>
	[JsonPropertyName("version")]
	public int Version { get; set; } = 1;
}
