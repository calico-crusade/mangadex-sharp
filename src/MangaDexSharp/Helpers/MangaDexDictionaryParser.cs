namespace MangaDexSharp;

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
	/// <param name="typeToConvert">The type the reader is attemping to read</param>
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
	/// Writes the directionary correctly
	/// </summary>
	/// <param name="writer">Where to write to</param>
	/// <param name="value">The value to write</param>
	/// <param name="options">The serialization options</param>
	public override void Write(Utf8JsonWriter writer, Localization value, JsonSerializerOptions options)
	{
		JsonSerializer.Serialize(writer, value, typeof(Dictionary<string, string>), options);
	}
}
