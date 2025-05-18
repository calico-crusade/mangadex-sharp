namespace MangaDexSharp;

/// <summary>
/// A utility for building query parameters in MD's style
/// </summary>
public class FilterBuilder
{
	/// <summary>
	/// All of that parameters in this builder
	/// </summary>
	public List<(string key, string value)> Parameters { get; } = [];

	/// <summary>
	/// Adds an optional string value to the parameters
	/// </summary>
	/// <param name="key">The name of the parameter</param>
	/// <param name="value">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add(string key, string? value)
	{
		if (string.IsNullOrEmpty(value)) return this;

		Parameters.Add((key, value));
		return this;
	}

	/// <summary>
	/// Adds an array of parameters
	/// </summary>
	/// <param name="key">The name of the parameter</param>
	/// <param name="items">The values of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add(string key, string[] items)
	{
		foreach (var item in items)
			Parameters.Add((key + "[]", item));

		return this;
	}

	/// <summary>
	/// Adds an array of parameters
	/// </summary>
	/// <typeparam name="T">The type of array</typeparam>
	/// <param name="key">The name of the parameter</param>
	/// <param name="items">The values of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add<T>(string key, T[] items)
	{
		return Add(key, items
			.Select(t => t?.ToString() ?? "")
			.Where(t => !string.IsNullOrEmpty(t))
			.ToArray());
	}

	/// <summary>
	/// Adds a date to the parameters
	/// </summary>
	/// <param name="key">The name of the parameter</param>
	/// <param name="date">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add(string key, DateTime? date)
	{
		if (date == null) return this;

		Parameters.Add((key, date.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss")));
		return this;
	}

	/// <summary>
	/// Adds an optional int value to the parameters
	/// </summary>
	/// <param name="key">The name of the parameter</param>
	/// <param name="value">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add(string key, int? value)
	{
		if (value == null) return this;

		Parameters.Add((key, value.Value.ToString()));
		return this;
	}

	/// <summary>
	/// Adds an int value to the parameters
	/// </summary>
	/// <param name="key">The name of the parameter</param>
	/// <param name="value">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add(string key, int value)
	{
		Parameters.Add((key, value.ToString()));
		return this;
	}


	/// <summary>
	/// Adds an optional bool value to the parameters
	/// </summary>
	/// <param name="key">The name of the parameter</param>
	/// <param name="value">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add(string key, bool? value)
	{
		if (value == null) return this;

		Parameters.Add((key, value.Value ? "1" : "0"));
		return this;
	}


	/// <summary>
	/// Adds a bool value to the parameters
	/// </summary>
	/// <param name="key">The name of the parameter</param>
	/// <param name="value">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add(string key, bool value)
	{
		Parameters.Add((key, value ? "1" : "0"));
		return this;
	}

	/// <summary>
	/// Adds a dictionary to the parameters
	/// </summary>
	/// <typeparam name="TKey">The type of key of the dictionary</typeparam>
	/// <typeparam name="TValue">The type of value of the dictionary</typeparam>
	/// <param name="key">The name of the parameter</param>
	/// <param name="obj">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	public FilterBuilder Add<TKey, TValue>(string key, Dictionary<TKey, TValue>? obj) where TKey : notnull
	{
		if (obj == null) return this;

		foreach (var (k, v) in obj)
		{
			if (v == null) continue;

			var type = $"{key}[{k}]";
			Parameters.Add((type, v.ToString() ?? ""));
		}

		return this;
	}

	/// <summary>
	/// Adds an enum to the parameters
	/// </summary>
	/// <typeparam name="T">The type of enum</typeparam>
	/// <param name="key">The name of the parameter</param>
	/// <param name="item">The value of the parameter</param>
	/// <returns>The current builder for chaining</returns>
	/// <exception cref="ArgumentException">Thrown if the type isn't an enum</exception>
	public FilterBuilder Add<T>(string key, T? item) where T : struct, IConvertible
	{
		if (!typeof(T).IsEnum) throw new ArgumentException("Type must be an enum", nameof(item));

		var value = item?.ToString();
		if (string.IsNullOrEmpty(value)) return this;

		Parameters.Add((key, value));
		return this;
	}

	/// <summary>
	/// Combines all of the query parameters together for use in a URL
	/// </summary>
	/// <returns>The query parameters</returns>
	public string Build()
	{
		return string.Join("&", Parameters.Select(t => $"{t.key}={t.value}"));
	}
}
