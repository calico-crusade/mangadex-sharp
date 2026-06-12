# MangaDexSharp.MangaDex

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDex.cs:115](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDex.cs#L115)

## Purpose

$typeName is part of the $typeProject surface.

This page exists so references to [MangaDex](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDex.cs#L115) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Manga` | `IMangaDexMangaService` | public |
| Property | `Chapter` | `IMangaDexChapterService` | public |
| Property | `Misc` | `IMangaDexMiscService` | public |
| Property | `Author` | `IMangaDexAuthorService` | public |
| Property | `Cover` | `IMangaDexCoverArtService` | public |
| Property | `Lists` | `IMangaDexCustomListService` | public |
| Property | `ReadMarker` | `IMangaDexReadMarkerService` | public |
| Property | `Feed` | `IMangaDexFeedService` | public |
| Property | `Follows` | `IMangaDexFollowsService` | public |
| Property | `Report` | `IMangaDexReportService` | public |
| Property | `ScanlationGroup` | `IMangaDexScanlationGroupService` | public |
| Property | `Upload` | `IMangaDexUploadService` | public |
| Property | `User` | `IMangaDexUserService` | public |
| Property | `Auth` | `IMangaDexAuthService` | public |
| Property | `ApiClient` | `IMangaDexApiClientService` | public |
| Property | `Statistics` | `IMangaDexStatisticsService` | public |
| Method/ctor | `Create` | `public static IMangaDex Create( Action<IMangaDexBuilder>? config = null, IServiceCollection? services = null) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




