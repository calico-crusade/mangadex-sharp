namespace MangaDexSharp;

public class UserFilter : IFilter
{
	public int Limit { get; set; } = 100;
	public int Offset { get; set; } = 0;

	public string[] Ids { get; set; } = Array.Empty<string>();
	public string? Username { get; set; }
	public OrderValue? UsernameOrder { get; set; }

	public string BuildQuery()
	{
		var bob = new FilterBuilder()
			.Add("limit", Limit)
			.Add("offset", Offset)
			.Add("ids", Ids)
			.Add("username", Username);

		if (UsernameOrder != null)
			bob.Add("order", new Dictionary<OrderKey, OrderValue>
			{
				[OrderKey.username] = UsernameOrder ?? OrderValue.desc
			});

		return bob.Build();
	}

	public enum OrderKey
	{
		username
	}
}
