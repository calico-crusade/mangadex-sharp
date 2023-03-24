namespace MangaDexSharp;

/// <summary>
/// Represents a collection of localized (translated) strings
/// </summary>
[JsonConverter(typeof(MangaDexDictionaryParser))]
public class Localization : Dictionary<string, string> { }
