namespace MangaDexSharp;

public class CoverArtFilter : IFilter
{
	public int Limit { get; set; } = 100;
	public int Offset { get; set; } = 0;
	public string[] MangaIds { get; set; } = Array.Empty<string>();
	public string[] Ids { get; set; } = Array.Empty<string>();
	public string[] Uploaders { get; set; } = Array.Empty<string>();
	public string[] Locales { get; set; } = Array.Empty<string>();
	public Dictionary<OrderKey, OrderValue> Order { get; set; } = new();
	public MangaIncludes[] Includes { get; set; } = new[]
	{
		MangaIncludes.manga,
		MangaIncludes.user
	};

	public string BuildQuery()
	{
		return new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("ids", Ids)
			.Add("manga", MangaIds)
			.Add("uploaders", Uploaders)
			.Add("locales", Locales)
			.Add("includes", Includes)
			.Add("order", Order)
			.Build();
	}

	public enum OrderKey
	{
		name,
		updatedAt,
		volume
	}
}
