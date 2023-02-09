namespace MangaDexSharp;

public class MangaDraftFilter : IFilter
{
	public int Limit { get; set; } = 100;
	public int Offset { get; set; } = 0;
	public DraftStatus? State { get; set; }
	public OrderValue? CreatedAtOrder { get; set; }
	public MangaIncludes[] Includes { get; set; } = new[]
	{
		MangaIncludes.manga,
		MangaIncludes.cover_art,
		MangaIncludes.author,
		MangaIncludes.artist,
		MangaIncludes.tag
	};

	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("state", State)
			.Add("includes", Includes);

		if (CreatedAtOrder != null)
			bob.Add("order", new Dictionary<MangaFilter.OrderKey, OrderValue> 
			{
				[MangaFilter.OrderKey.createdAt] = CreatedAtOrder ?? OrderValue.desc
			});

		return bob.Build();
	}
}
