namespace MangaDexSharp;

/// <summary>
/// Utility for parsing the aggregate chapter data from MangaDex.
/// </summary>
/// <remarks>
/// This was a bug fix because if the volumes come back with only one entry for the chapters dictionary,
/// MD gives an array instead. The key for the chapter in this case is the <see cref="MangaAggregate.ChapterData.Chapter"/> value.
/// </remarks>
public class MangaDexAggregateChapterParser : JsonConverter<Dictionary<string, MangaAggregate.ChapterData>>
{
    /// <summary>   
    /// Read the dictionary (or array) from the JSON reader
    /// </summary>
    /// <param name="reader">The reader to read from</param>
    /// <param name="typeToConvert">The type the reader is attempting to read</param>
    /// <param name="options">The serialization options</param>
    /// <returns>The localization dictionary</returns>
    public override Dictionary<string, MangaAggregate.ChapterData>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
            return JsonSerializer.Deserialize<Dictionary<string, MangaAggregate.ChapterData>>(ref reader, options) ?? [];

        if (reader.TokenType != JsonTokenType.StartArray)
            return [];

        var chapters = JsonSerializer.Deserialize<MangaAggregate.ChapterData[]>(ref reader, options);
        return chapters.ToDictionary(t => t.Chapter);
    }

    /// <summary>
    /// Writes the dictionary correctly
    /// </summary>
    /// <param name="writer">Where to write to</param>
    /// <param name="value">The value to write</param>
    /// <param name="options">The serialization options</param>
    public override void Write(Utf8JsonWriter writer, Dictionary<string, MangaAggregate.ChapterData> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, typeof(Dictionary<string, MangaAggregate.ChapterData>), options);
    }
}
