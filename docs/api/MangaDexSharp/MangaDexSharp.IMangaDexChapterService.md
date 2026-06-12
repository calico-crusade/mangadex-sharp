# MangaDexSharp.IMangaDexChapterService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexChapterService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexChapterService.cs#L6)

## Purpose

Represents all of the requests on the /chapter endpoints

This page exists so references to [IMangaDexChapterService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexChapterService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.Chapter) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `List` | `Task<ChapterList> List(ChaptersFilter? filter = null);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<Chapter>> Get(string id, string[]? includes = null);` | public |
| Method/ctor | `Update` | `Task<MangaDexRoot<Chapter>> Update(string id, ChapterUpdate update, string? token = null);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string id, string? token = null);` | public |
| Method/ctor | `ListAll` | `IAsyncEnumerable<Chapter> ListAll(ChaptersFilter? filter = null, int? delay = null, int? rateCap = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




