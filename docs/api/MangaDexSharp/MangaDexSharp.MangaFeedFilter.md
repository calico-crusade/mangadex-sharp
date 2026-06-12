# MangaDexSharp.MangaFeedFilter

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Manga/MangaFeedFilter.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/MangaFeedFilter.cs#L6)

## Purpose

Represents a query parameter filter for the manga feed endpoints

This page exists so references to [MangaFeedFilter](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/MangaFeedFilter.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `DefaultLimit` | `static int` | public |
| Property | `Limit` | `int` | public |
| Property | `Offset` | `int` | public |
| Property | `TranslatedLanguage` | `string[]` | public |
| Property | `OriginalLanguage` | `string[]` | public |
| Property | `ExcludedOriginalLanguage` | `string[]` | public |
| Property | `ContentRating` | `ContentRating[]` | public |
| Property | `ExcludedGroups` | `string[]` | public |
| Property | `ExcludedUploaders` | `string[]` | public |
| Property | `IncludeFutureUpdates` | `bool?` | public |
| Property | `CreatedAtSince` | `DateTime?` | public |
| Property | `UpdatedAtSince` | `DateTime?` | public |
| Property | `PublishAtSince` | `DateTime?` | public |
| Property | `Order` | `Dictionary<OrderKey, OrderValue>` | public |
| Property | `Includes` | `MangaIncludes[]` | public |
| Property | `IncludeEmptyPages` | `bool?` | public |
| Property | `IncludeFuturePublishAt` | `bool?` | public |
| Property | `IncludeExternalUrl` | `bool?` | public |
| Property | `IncludeUnavailable` | `bool?` | public |
| Method/ctor | `BuildQuery` | `public string BuildQuery() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




