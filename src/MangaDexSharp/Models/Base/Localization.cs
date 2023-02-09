namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexDictionaryParser))]
public class Localization : Dictionary<string, string> { }
