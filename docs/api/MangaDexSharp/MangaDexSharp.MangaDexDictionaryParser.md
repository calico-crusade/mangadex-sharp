# MangaDexSharp.MangaDexDictionaryParser

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/MangaDexDictionaryParser.cs:50](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexDictionaryParser.cs#L50)

## Purpose

Utility for parsing dictionaries within MD return results. This was a bug fix as some of the MD return results were returning empty arrays instead of empty objects for dictionaries and that was causing parser issues within System.Text.Json.

This page exists so references to [MangaDexDictionaryParser](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexDictionaryParser.cs#L50) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Read` | `public override Localization? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {` | public |
| Method/ctor | `Localization` | `var lcl = new Localization();` | public |
| Method/ctor | `Write` | `public override void Write(Utf8JsonWriter writer, Localization value, JsonSerializerOptions options) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)






