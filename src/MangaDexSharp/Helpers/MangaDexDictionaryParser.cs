namespace MangaDexSharp;

/// <summary>
/// Utility for parsing dictionaries within MD return results.
/// This was a bug fix as some of the MD return results were returning
/// empty arrays instead of empty objects for dictionaries and that was
/// causing parser issues within System.Text.Json.
/// </summary>
/// <typeparam name="TKey">The key of the dictionary</typeparam>
/// <typeparam name="TValue">The value of the dictionary</typeparam>
public class MangaDexDictionaryParser<TKey, TValue> : JsonConverter<Dictionary<TKey, TValue>>
{
    /// <summary>
    /// Read the dictionary (or empty array) from the JSON reader
    /// </summary>
    /// <param name="reader">The reader to read from</param>
    /// <param name="typeToConvert">The type the reader is attempting to read</param>
    /// <param name="options">The serialization options</param>
    /// <returns>The localization dictionary</returns>
    public override Dictionary<TKey, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            _ = JsonSerializer.Deserialize<string[]>(ref reader, options);
            return new();
        }

        var dic = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(ref reader, options) ?? new();

        var lcl = new Dictionary<TKey, TValue>();
        foreach (var item in dic)
            lcl.Add(item.Key, item.Value);

        return lcl;
    }

    /// <summary>
    /// Writes the dictionary correctly
    /// </summary>
    /// <param name="writer">Where to write to</param>
    /// <param name="value">The value to write</param>
    /// <param name="options">The serialization options</param>
    public override void Write(Utf8JsonWriter writer, Dictionary<TKey, TValue> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, typeof(Dictionary<TKey, TValue>), options);
    }
}

/// <summary>
/// Utility for parsing dictionaries within MD return results.
/// This was a bug fix as some of the MD return results were returning
/// empty arrays instead of empty objects for dictionaries and that was
/// causing parser issues within System.Text.Json.
/// </summary>
public class MangaDexDictionaryParser : JsonConverter<Localization>
{
	/// <summary>
	/// Read the dictionary (or empty array) from the JSON reader
	/// </summary>
	/// <param name="reader">The reader to read from</param>
	/// <param name="typeToConvert">The type the reader is attempting to read</param>
	/// <param name="options">The serialization options</param>
	/// <returns>The localization dictionary</returns>
	public override Localization? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.StartArray)
		{
			_ = JsonSerializer.Deserialize<string[]>(ref reader, options);
			return new Localization();
		}

		var dic = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options) ?? new();

		var lcl = new Localization();
		foreach (var item in dic)
			lcl.Add(item.Key, item.Value);

		return lcl;
	}

    /// <summary>
    /// Writes the dictionary correctly
    /// </summary>
    /// <param name="writer">Where to write to</param>
    /// <param name="value">The value to write</param>
    /// <param name="options">The serialization options</param>
    public override void Write(Utf8JsonWriter writer, Localization value, JsonSerializerOptions options)
	{
		JsonSerializer.Serialize(writer, value, typeof(Dictionary<string, string>), options);
	}
}
