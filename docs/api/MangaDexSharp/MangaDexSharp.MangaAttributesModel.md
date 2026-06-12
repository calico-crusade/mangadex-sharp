# MangaDexSharp.MangaAttributesModel

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Manga/Manga.cs:17](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/Manga.cs#L17)

## Purpose

All of the properties on this manga

This page exists so references to [MangaAttributesModel](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/Manga.cs#L17) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Title` | `Localization` | public |
| Property | `AltTitles` | `Localization[]` | public |
| Property | `Description` | `Localization` | public |
| Property | `IsLocked` | `bool` | public |
| Property | `Links` | `Localization` | public |
| Property | `OriginalLanguage` | `string` | public |
| Property | `LastVolume` | `string` | public |
| Property | `LastChapter` | `string` | public |
| Property | `PublicationDemographic` | `Demographic?` | public |
| Property | `Status` | `Status?` | public |
| Property | `Year` | `int?` | public |
| Property | `ContentRating` | `ContentRating?` | public |
| Property | `Tags` | `MangaTag[]` | public |
| Property | `State` | `string` | public |
| Property | `ChapterNumbersResetOnNewVolume` | `bool` | public |
| Property | `CreatedAt` | `DateTime` | public |
| Property | `UpdatedAt` | `DateTime` | public |
| Property | `Version` | `int` | public |
| Property | `AvailableTranslatedLanguages` | `string[]` | public |
| Property | `LatestUploadedChapter` | `string` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




