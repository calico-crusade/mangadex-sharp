namespace MangaDexSharp;

/// <summary>
/// Represents the available query parameters for the users endpoint
/// </summary>
public class UserFilter : IPaginateFilter
{
	/// <summary>
	/// The number of records to return for this filter (MAX 100, MIN 1, DEFAULT 100)
	/// </summary>
	public int Limit { get; set; } = 100;

	/// <summary>
	/// The number of records to skip when using this filter (MIN 0, DEFAULT 0)
	/// </summary>
	public int Offset { get; set; } = 0;

	/// <summary>
	/// Return only users in this list of IDs
	/// </summary>
	public string[] Ids { get; set; } = Array.Empty<string>();

	/// <summary>
	/// Return only users whose username matches this
	/// </summary>
	public string? Username { get; set; }
	
	/// <summary>
	/// Determine how to order the returned users
	/// </summary>
	public OrderValue? UsernameOrder { get; set; }

	/// <summary>
	/// Builds the query parameters for the URL
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// The available fields the can be ordered by
	/// </summary>
	public enum OrderKey
	{
		/// <summary>
		/// The username field
		/// </summary>
		username
	}
}
