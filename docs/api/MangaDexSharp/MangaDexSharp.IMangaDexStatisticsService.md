# MangaDexSharp.IMangaDexStatisticsService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexStatisticsService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexStatisticsService.cs#L6)

## Purpose

Represents all of the requests on the /statistics endpoints

This page exists so references to [IMangaDexStatisticsService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexStatisticsService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.Statistics) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Chapter` | `Task<CommentStatistics> Chapter(string chapterId, string? token = null);` | public |
| Method/ctor | `Chapters` | `Task<CommentStatistics> Chapters(string[] chapterIds, string? token = null);` | public |
| Method/ctor | `ScanlationGroup` | `Task<CommentStatistics> ScanlationGroup(string groupId, string? token = null);` | public |
| Method/ctor | `ScanlationGroups` | `Task<CommentStatistics> ScanlationGroups(string[] groupIds, string? token = null);` | public |
| Method/ctor | `Manga` | `Task<MangaStatistics> Manga(string mangaId, string? token = null);` | public |
| Method/ctor | `Manga` | `Task<MangaStatistics> Manga(string[] mangaIds, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




