namespace MangaDexSharp;

public interface IJsonType
{
	string Type { get; set; }
}

public class MangaDexParser<T> : JsonConverter<T> where T : IJsonType
{
	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var actualMap = GetTypeMap();
		if (actualMap == null) throw new JsonException("Type is not present in types list");

		var (_, map) = actualMap;

		Utf8JsonReader readerClone = reader;
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException();

		var typeName = ReadUntilType(readerClone);
		if (!map.ContainsKey(typeName))
			throw new JsonException("Type is not present in type map");

		var pureType = map[typeName];
		var deser = JsonSerializer.Deserialize(ref reader, pureType, options);
		return (T?)deser;
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		var actualMap = GetTypeMap();
		if (actualMap == null) throw new JsonException("Type is not present in types list");

		var (_, map) = actualMap;
		if (!map.ContainsKey(value.Type)) throw new JsonException("Type is not present in type map");

		var pureType = map[value.Type];
		JsonSerializer.Serialize(writer, value, pureType, options);
	}

	public string ReadUntilType(Utf8JsonReader reader)
	{
		while (true)
		{
			reader.Read();
			var (name, value) = ReadProperty(reader);
			if (name == "type") return value ?? string.Empty;
			reader.Read();
		}
	}

	public (string? name, string? value) ReadProperty(Utf8JsonReader reader)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			var item = reader.GetString();
			Console.WriteLine(item);
		}

		if (reader.TokenType != JsonTokenType.PropertyName)
			throw new JsonException();

		var name = reader.GetString();

		reader.Read();
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException();

		var value = reader.GetString();

		return (name, value);
	}

	public TypeMap[] Types()
	{
		return new[]
		{
			new TypeMap(typeof(IRelationship), new()
			{
				["author"] = typeof(PersonRelationship),
				["artist"] = typeof(PersonRelationship),
				["cover_art"] = typeof(CoverArtRelationship),
				["manga"] = typeof(RelatedDataRelationship),
				["scanlation_group"] = typeof(ScanlationGroup),
				["user"] = typeof(UserRelationship)
			})
		};
	}

	public TypeMap? GetTypeMap()
	{
		var types = Types();
		foreach (var map in types)
			if (typeof(T) == map.Interface)
				return map;

		return null;
	}
}

public class TypeMap
{
	public Type Interface { get; set; }
	public Dictionary<string, Type> Maps { get; set; }

	public TypeMap(Type @interface, Dictionary<string, Type> maps)
	{
		Interface = @interface;
		Maps = maps;
	}

	public void Deconstruct(out Type @interface, out Dictionary<string, Type> maps)
	{
		@interface = Interface;
		maps = Maps;
	}
}
