namespace MangaDexSharp;

public class FilterBuilder
{
	public List<(string key, string value)> Parameters { get; } = new();

	public FilterBuilder Add(string key, string? value)
	{
		if (string.IsNullOrEmpty(value)) return this;

		Parameters.Add((key, value));
		return this;
	}

	public FilterBuilder Add(string key, string[] items)
	{
		foreach (var item in items)
			Parameters.Add((key + "[]", item));

		return this;
	}

	public FilterBuilder Add<T>(string key, T[] items)
	{
		return Add(key, items
			.Select(t => t?.ToString() ?? "")
			.Where(t => !string.IsNullOrEmpty(t))
			.ToArray());
	}

	public FilterBuilder Add(string key, DateTime? date)
	{
		if (date == null) return this;

		Parameters.Add((key, date.Value.ToString("yyyy-MM-ddThh:mm:ss.fffZ")));
		return this;
	}

	public FilterBuilder Add(string key, int? value)
	{
		if (value == null) return this;

		Parameters.Add((key, value.Value.ToString()));
		return this;
	}

	public FilterBuilder Add(string key, int value)
	{
		Parameters.Add((key, value.ToString()));
		return this;
	}

	public FilterBuilder Add(string key, bool? value)
	{
		if (value == null) return this;

		Parameters.Add((key, value.Value ? "1" : "0"));
		return this;
	}

	public FilterBuilder Add(string key, bool value)
	{
		Parameters.Add((key, value ? "1" : "0"));
		return this;
	}

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

	public FilterBuilder Add<T>(string key, T? item) where T : struct, IConvertible
	{
		if (!typeof(T).IsEnum) throw new ArgumentException("Type must be an enum", nameof(item));

		var value = item?.ToString();
		if (string.IsNullOrEmpty(value)) return this;

		Parameters.Add((key, value));
		return this;
	}

	public string Build()
	{
		return string.Join("&", Parameters.Select(t => $"{t.key}={t.value}"));
	}
}
