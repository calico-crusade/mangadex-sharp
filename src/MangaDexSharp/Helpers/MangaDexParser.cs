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
	/// Whether or not to ignore missing type maps.
	/// </summary>
	public static bool IgnoreMapErrors { get; set; } = true;

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
		var actualMap = GetTypeMap();

		if (actualMap == null)
		{
			if (IgnoreMapErrors) return default;

			throw new JsonException("Type is not present in types list");
		}
		var (_, map) = actualMap;

		//Create a clone of the reader so we don't mess with subsequent reads
		Utf8JsonReader readerClone = reader;
		var type = JsonSerializer.Deserialize<EmptyType>(ref readerClone, options);

		//Read until we get to the `type` property
		var typeName = type?.Type ?? string.Empty;
		//Ensure that it is a type we're expecting.
		if (!map.ContainsKey(typeName))
		{
			if (IgnoreMapErrors) return default;

			throw new JsonException("Type is not present in type map");
		}

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
	/// Gets a map of all of the available types we know how to deserialize
	/// </summary>
	/// <returns>The map of all of the types</returns>
	public TypeMap[] Types()
	{
		return
        [
			TypeMap.Create<IRelationship>()
				.Register<PersonRelationship>("author")
				.Register<PersonRelationship>("artist")
				.Register<CoverArtRelationship>("cover_art")
				.Register<RelatedDataRelationship>("manga")
				.Register<ScanlationGroup>("scanlation_group")
				.Register<User>("user")
				.Register<User>("leader")
				.Register<User>("creator")
				.Register<User>("member")
				.Register<MangaTag>("tag")
				.Register<Chapter>("chapter")
				.Register<ApiClient>("api_client")
				.Register<UploadSessionFile>("upload_session_file")
				.Register<MangaRecommendation>("manga_recommendation")
				.Build()
		];
	}

	/// <summary>
	/// Gets the type map for the current parser 
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

	internal class EmptyType
	{
		[JsonPropertyName("type")]
		public string Type { get; set; } = string.Empty;
	}
}

/// <summary>
/// Represents the relationship between an interface and it's available concrete types
/// </summary>
/// <param name="interface"> The type of interface we're dealing with</param>
/// <param name="maps">The map of all of the types that represent the interface</param>
public class TypeMap(
	Type @interface, 
	Dictionary<string, Type> maps)
{
    /// <summary>
    /// The type of interface we're dealing with
    /// </summary>
    public Type Interface { get; set; } = @interface;

    /// <summary>
    /// The map of all of the types that represent the interface
    /// </summary>
    public Dictionary<string, Type> Maps { get; set; } = maps;

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

	/// <summary>
	/// Creates a new builder for a type map
	/// </summary>
	/// <typeparam name="T">The base type</typeparam>
	/// <returns>The builder</returns>
	public static Builder<T> Create<T>() => new();

	/// <summary>
	/// A builder for making a <see cref="TypeMap"/>
	/// </summary>
	public class Builder<T>
	{
		/// <summary>
		/// The map of the types that represent the interface
		/// </summary>
		public Dictionary<string, Type> Map { get; set; } = [];

		/// <summary>
		/// Registers a type for the given type
		/// </summary>
		/// <typeparam name="TType">The model type</typeparam>
		/// <param name="name">The key of the type</param>
		/// <returns>The builder for fluent chaining</returns>
		public Builder<T> Register<TType>(string name) where TType : T
		{
			Map[name] = typeof(TType);
			return this;
		}

		/// <summary>
		/// Builds the type map
		/// </summary>
		/// <returns>The type map</returns>
		public TypeMap Build()
		{
			return new TypeMap(typeof(T), Map);
		}
	}
}
