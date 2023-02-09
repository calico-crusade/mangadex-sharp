namespace MangaDexSharp;

public class MangaDexDictionaryParser : JsonConverter<Localization>
{
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

	public override void Write(Utf8JsonWriter writer, Localization value, JsonSerializerOptions options)
	{
		JsonSerializer.Serialize(writer, value, typeof(Dictionary<string, string>), options);
	}
}
