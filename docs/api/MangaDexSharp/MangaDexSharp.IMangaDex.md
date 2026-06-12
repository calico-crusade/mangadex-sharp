# MangaDexSharp.IMangaDex

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDex.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDex.cs#L6)

## Purpose

Represents an instance of the [MangaDex](../MangaDexSharp/MangaDexSharp.MangaDex.md) API

This page exists so references to [IMangaDex](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDex.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Create it with `MangaDex.Create()` or inject it after `services.AddMangaDex()`.

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
| Property | `Pages` | `IMangaDexPageService` | public |
| Property | `Author` | `IMangaDexAuthorService` | public |
| Property | `Cover` | `IMangaDexCoverArtService` | public |
| Property | `Lists` | `IMangaDexCustomListService` | public |
| Property | `Feed` | `IMangaDexFeedService` | public |
| Property | `Follows` | `IMangaDexFollowsService` | public |
| Property | `Ratings` | `IMangaDexRatingService` | public |
| Property | `Threads` | `IMangaDexThreadsService` | public |
| Property | `Captcha` | `IMangaDexCaptchaService` | public |
| Property | `Takedown` | `IMangaDexTakedownService` | public |
| Property | `ReadMarker` | `IMangaDexReadMarkerService` | public |
| Property | `Report` | `IMangaDexReportService` | public |
| Property | `ScanlationGroup` | `IMangaDexScanlationGroupService` | public |
| Property | `Upload` | `IMangaDexUploadService` | public |
| Property | `User` | `IMangaDexUserService` | public |
| Property | `Auth` | `IMangaDexAuthService` | public |
| Property | `ApiClient` | `IMangaDexApiClientService` | public |
| Property | `Statistics` | `IMangaDexStatisticsService` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




