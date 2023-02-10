namespace MangaDexSharp;

public class AuthorFilter : IFilter
{
	public int Limit { get; set; } = 100;
	public int Offset { get; set; } = 0;
	public string[] Ids { get; set; } = Array.Empty<string>();
	public OrderValue? NameOrder { get; set; }
	public MangaIncludes[] Includes { get; set; } = new[]
	{
		MangaIncludes.manga
	};

	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("ids", Ids)
			.Add("includes", Includes);

		if (NameOrder != null)
			bob.Add("order", new Dictionary<OrderKey, OrderValue>
			{
				[OrderKey.name] = NameOrder ?? OrderValue.desc
			});

		return bob.Build();
	}

	public enum OrderKey
	{
		name
	}
}