using CardboardBox.Json;

namespace MangaDexSharp;

/// <summary>
/// Exposes common Json serialization and deserialization methods tailored to MangaDex
/// </summary>
public interface IMdJsonService : IJsonService 
{
    /// <summary>
    /// Serializes the given data into an indented JSON string
    /// </summary>
    /// <typeparam name="T">The type of data to serialize</typeparam>
    /// <param name="data">The data to serialize</param>
    /// <returns>The pretty print version of the JSON</returns>
    string? Pretty<T>(T data);
}

/// <summary>
/// The concrete implementation for the <see cref="IMdJsonService"/>
/// </summary>
public class MdJsonService : SystemTextJsonService, IMdJsonService
{
    /// <summary>
    /// The default JSON Serialization options for the MangaDex API
    /// </summary>
    public static JsonSerializerOptions? DEFAULT_OPTIONS = null;

    /// <summary>
    /// The concrete implementation for the <see cref="IMdJsonService"/>
    /// </summary>
    public MdJsonService() : base(DEFAULT_OPTIONS ??= new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    }) { }

    /// <summary>
    /// Serializes the given data into an indented JSON string
    /// </summary>
    /// <typeparam name="T">The type of data to serialize</typeparam>
    /// <param name="data">The data to serialize</param>
    /// <returns>The pretty print version of the JSON</returns>
    public string? Pretty<T>(T data)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        return JsonSerializer.Serialize(data, options);
    }
}
