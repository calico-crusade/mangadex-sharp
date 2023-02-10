namespace MangaDexSharp;

public class ReportFilter : IFilter
{
	public int Limit { get; set; } = 100;
	public int Offset { get; set; } = 0;
	public ReportCategory? Category { get; set; }
	public string? ReasonId { get; set; }
	public string? ObjectId { get; set; }
	public ReportState? State { get; set; }
	public OrderValue? CreatedAtOrder { get; set; }

	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("category", Category)
			.Add("reasonId", ReasonId)
			.Add("objectId", ObjectId)
			.Add("status", State)
			.Add("includes", new[] { "user", "reason" });

		if (CreatedAtOrder != null)
			bob.Add("order", new Dictionary<MangaFilter.OrderKey, OrderValue>
			{
				[MangaFilter.OrderKey.createdAt] = CreatedAtOrder ?? OrderValue.asc
			});

		return bob.Build();
	}
}
