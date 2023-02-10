namespace MangaDexSharp;

public class MangaDexEnumParser<T> : JsonConverter<T> where T : struct, IConvertible
{
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		VerifyType(typeToConvert);
		var value = reader.GetString();
		if (string.IsNullOrEmpty(value) ||
			!Enum.TryParse<T>(value, true, out var val))
			return default;

		return val;
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		VerifyType(typeof(T));
		JsonSerializer.Serialize(writer, value.ToString(), typeof(string), options);
	}

	public void VerifyType(Type type)
	{
		if (!type.IsEnum) throw new ArgumentException("Type must be an enum");
	}
}
