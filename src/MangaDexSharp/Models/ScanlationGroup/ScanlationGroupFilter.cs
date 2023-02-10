namespace MangaDexSharp;

public class ScanlationGroupFilter : IFilter
{
	public int Limit { get; set; } = 100;
	public int Offset { get; set; } = 0;
	public string[] Ids { get; set; } = Array.Empty<string>();
	public string? Name { get; set; }
	public string? FocusedLanguage { get; set; }
	public OrderValue? LatestUploadedChapterOrder { get; set; }

	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("ids", Ids)
			.Add("name", Name)
			.Add("focusedLanguage", FocusedLanguage)
			.Add("includes", new[] { "leader", "member" });

		if (LatestUploadedChapterOrder != null)
			bob.Add("order", new Dictionary<MangaFilter.OrderKey, OrderValue>
			{
				[MangaFilter.OrderKey.latestUploadedChapter] = LatestUploadedChapterOrder ?? OrderValue.desc
			});

		return bob.Build();
	}
}
