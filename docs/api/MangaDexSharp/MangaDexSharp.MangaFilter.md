# MangaDexSharp.MangaFilter

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Manga/MangaFilter.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/MangaFilter.cs#L6)

## Purpose

Represents the available query parameters for the manga endpoint

This page exists so references to [MangaFilter](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/MangaFilter.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Title` | `string` | public |
| Property | `Limit` | `int` | public |
| Property | `Offset` | `int` | public |
| Property | `AuthorOrArtist` | `string` | public |
| Property | `ContentRating` | `ContentRating[]` | public |
| Property | `Includes` | `MangaIncludes[]` | public |
| Property | `Authors` | `string[]` | public |
| Property | `Artists` | `string[]` | public |
| Property | `Year` | `int?` | public |
| Property | `IncludedTags` | `string[]` | public |
| Property | `IncludeTagsMode` | `Mode?` | public |
| Property | `ExcludedTags` | `string[]` | public |
| Property | `ExcludedTagsMode` | `Mode?` | public |
| Property | `Status` | `Status[]` | public |
| Property | `OriginalLanguage` | `string[]` | public |
| Property | `ExcludedOriginalLanguage` | `string[]` | public |
| Property | `AvailableTranslatedLanguage` | `string[]` | public |
| Property | `PublicationDemographic` | `Demographic[]` | public |
| Property | `Ids` | `string[]` | public |
| Property | `CreatedAtSince` | `DateTime?` | public |
| Property | `UpdatedAtSince` | `DateTime?` | public |
| Property | `HasAvailableChapters` | `bool?` | public |
| Property | `HasUnavailableChapters` | `bool?` | public |
| Property | `Group` | `string` | public |
| Property | `Order` | `Dictionary<OrderKey, OrderValue>` | public |
| Method/ctor | `BuildQuery` | `public string BuildQuery() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




