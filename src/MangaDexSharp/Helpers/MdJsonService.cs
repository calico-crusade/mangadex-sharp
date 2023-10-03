using CardboardBox.Json;

namespace MangaDexSharp;

/// <summary>
/// Exposes common Json serialization and deserialization methods tailored to MangaDex
/// </summary>
public interface IMdJsonService : IJsonService { }

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
}
