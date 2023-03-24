namespace MangaDexSharp;

/// <summary>
/// Utility for parsing enums with MD return results
/// </summary>
/// <typeparam name="T">The type of enum to parse</typeparam>
public class MangaDexEnumParser<T> : JsonConverter<T> where T : struct, IConvertible
{
	/// <summary>
	/// Read the enum from the return results
	/// </summary>
	/// <param name="reader">The reader to read from</param>
	/// <param name="typeToConvert">The type the reader is attempting to read</param>
	/// <param name="options">The serialization options</param>
	/// <returns>The enum</returns>
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		VerifyType(typeToConvert);
		var value = reader.GetString();
		if (string.IsNullOrEmpty(value) ||
			!Enum.TryParse<T>(value, true, out var val))
			return default;

		return val;
	}

	/// <summary>
	/// Writes the enum correctly
	/// </summary>
	/// <param name="writer">Where to write to</param>
	/// <param name="value">The value to write</param>
	/// <param name="options">The serialization options</param>
	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		VerifyType(typeof(T));
		JsonSerializer.Serialize(writer, value.ToString(), typeof(string), options);
	}

	/// <summary>
	/// Verifies that the type is an enum
	/// </summary>
	/// <param name="type">The type to check</param>
	/// <exception cref="ArgumentException">Thrown if the type is not an enum</exception>
	public void VerifyType(Type type)
	{
		if (!type.IsEnum) throw new ArgumentException("Type must be an enum");
	}
}
