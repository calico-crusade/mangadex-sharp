# MangaDexSharp.MangaDexAggregateChapterParser

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/MangaDexAggregateChapterParser.cs:10](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexAggregateChapterParser.cs#L10)

## Purpose

Utility for parsing the aggregate chapter data from [MangaDex](../MangaDexSharp/MangaDexSharp.MangaDex.md). This was a bug fix because if the volumes come back with only one entry for the chapters dictionary, MD gives an array instead. The key for the chapter in this case is the [MangaAggregate](../MangaDexSharp/MangaDexSharp.MangaAggregate.md).[ChapterData](../MangaDexSharp/MangaDexSharp.ChapterData.md).[Chapter](../MangaDexSharp/MangaDexSharp.Chapter.md) value.

This page exists so references to [MangaDexAggregateChapterParser](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexAggregateChapterParser.cs#L10) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Read` | `public override Dictionary<string, MangaAggregate.ChapterData>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {` | public |
| Method/ctor | `Write` | `public override void Write(Utf8JsonWriter writer, Dictionary<string, MangaAggregate.ChapterData> value, JsonSerializerOptions options) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




