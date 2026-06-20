# MangaDexSharp.IMangaDexFeedService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexFeedService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexFeedService.cs#L6)

## Purpose

Represents all of the different feed endpoints (expect manga, that's on the [IMangaDex](../MangaDexSharp/MangaDexSharp.IMangaDex.md).[Manga](../MangaDexSharp/MangaDexSharp.Manga.md) service)

This page exists so references to [IMangaDexFeedService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexFeedService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.Feed` or inject the service from a provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `User` | `Task<ChapterList> User(ChaptersFilter? filter = null, string? token = null);` | public |
| Method/ctor | `List` | `Task<ChapterList> List(string listId, ChaptersFilter? filter = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




