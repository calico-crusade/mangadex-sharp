namespace MangaDexSharp;

/// <summary>
/// Represents a JSON object that has a `type` filed on it
/// </summary>
public interface IJsonType
{
	/// <summary>
	/// The type of record we're dealing with
	/// </summary>
	string Type { get; set; }
}

/// <summary>
/// Utility for parsing relationship and attribute types from MD return results.
/// This is to support the varying types returned in the `relationships` and `attributes` result sets.
/// </summary>
/// <typeparam name="T">The type of object to deserialize</typeparam>
public class MangaDexParser<T> : JsonConverter<T> where T : IJsonType
{
	/// <summary>
	/// Reads the object from the JSON reader
	/// </summary>
	/// <param name="reader">The reader to read from</param>
	/// <param name="typeToConvert">The type the reader is attempting to read</param>
	/// <param name="options">The serialization options</param>
	/// <returns>The type read from the JSON</returns>
	/// <exception cref="JsonException">Thrown if an exception happens during the reading process</exception>
	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		//Get a map of all of the types we can deserialize
		var actualMap = GetTypeMap() ?? throw new JsonException("Type is not present in types list");
		var (_, map) = actualMap;

		//Create a clone of the reader so we don't mess with subsequent reads
		Utf8JsonReader readerClone = reader;
		//Validate we're starting an object
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException();

		//Read until we get to the `type` property
		var typeName = ReadUntilType(readerClone);
		//Ensure that it is a type we're expecting.
		if (!map.ContainsKey(typeName))
			throw new JsonException("Type is not present in type map");

		//Get the actual C# POCO that represents the type from the map
		var pureType = map[typeName];
		//Deserialize the results based off of the computed type
		var deser = JsonSerializer.Deserialize(ref reader, pureType, options);
		//Cast results to the correct type
		return (T?)deser;
	}

	/// <summary>
	/// Writes the object correctly
	/// </summary>
	/// <param name="writer">Where to write to</param>
	/// <param name="value">The value to write</param>
	/// <param name="options">The serialization options</param>
	/// <exception cref="JsonException">Thrown if an exception happens during the writing process</exception>
	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		//Get a map of all of the types we can deserialize
		var actualMap = GetTypeMap() ?? throw new JsonException("Type is not present in types list");
		var (_, map) = actualMap;
		//Ensure we know how to serialize the type
		if (!map.ContainsKey(value.Type)) throw new JsonException("Type is not present in type map");
		//Get the actual C# POCO that represents the type from the map
		var pureType = map[value.Type];
		//Serialize the results to JSON
		JsonSerializer.Serialize(writer, value, pureType, options);
	}

	/// <summary>
	/// Keeps reading from the JSON reader until it hits the `type` property
	/// </summary>
	/// <param name="reader">The reader to read from</param>
	/// <returns></returns>
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

	/// <summary>
	/// Reads the value of a property from the JSON reader
	/// </summary>
	/// <param name="reader"></param>
	/// <returns></returns>
	/// <exception cref="JsonException"></exception>
	public (string? name, string? value) ReadProperty(Utf8JsonReader reader)
	{
		//Skip over the previous property value
		if (reader.TokenType == JsonTokenType.String)
			reader.GetString();

		//Ensure we're looking at the property name
		if (reader.TokenType != JsonTokenType.PropertyName)
			throw new JsonException();

		//Read the name from the property
		var name = reader.GetString();

		//Advance until we hit the next token
		reader.Read();
		//Ensure the token is a string 
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException();

		//Read the value of the token
		var value = reader.GetString();
		//Return the property name and value
		return (name, value);
	}

	/// <summary>
	/// Gets a map of all of the available types we know how to deserialize
	/// </summary>
	/// <returns>The map of all of the types</returns>
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
				["user"] = typeof(User)
			})
		};
	}

	/// <summary>
	/// Gets the type map for the current deserailizer 
	/// </summary>
	/// <returns></returns>
	public TypeMap? GetTypeMap()
	{
		var types = Types();
		foreach (var map in types)
			if (typeof(T) == map.Interface)
				return map;

		return null;
	}
}

/// <summary>
/// Represents the relationship between an interface and it's available concrete types
/// </summary>
public class TypeMap
{
	/// <summary>
	/// The type of interface we're dealing with
	/// </summary>
	public Type Interface { get; set; }
	/// <summary>
	/// The map of all of the types that represent the interface
	/// </summary>
	public Dictionary<string, Type> Maps { get; set; }

	/// <summary>
	/// ctor
	/// </summary>
	/// <param name="interface"></param>
	/// <param name="maps"></param>
	public TypeMap(Type @interface, Dictionary<string, Type> maps)
	{
		Interface = @interface;
		Maps = maps;
	}

	/// <summary>
	/// dtor
	/// </summary>
	/// <param name="interface"></param>
	/// <param name="maps"></param>
	public void Deconstruct(out Type @interface, out Dictionary<string, Type> maps)
	{
		@interface = Interface;
		maps = Maps;
	}
}
