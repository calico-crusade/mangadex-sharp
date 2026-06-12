# MangaDexSharp.MangaCreate

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Manga/MangaCreate.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/MangaCreate.cs#L6)

## Purpose

Represents a request to create a manga in the MD api

This page exists so references to [MangaCreate](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Manga/MangaCreate.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Authors` | `string[]` | public |
| Property | `Artists` | `string[]` | public |
| Property | `Links` | `Localization` | public |
| Property | `OriginalLanguage` | `string` | public |
| Property | `LastVolume` | `string?` | public |
| Property | `LastChapter` | `string?` | public |
| Property | `Demographic` | `Demographic?` | public |
| Property | `Status` | `Status` | public |
| Property | `Year` | `int?` | public |
| Property | `ContentRating` | `ContentRating` | public |
| Property | `ChapterNumbersResetOnNewVolume` | `bool` | public |
| Property | `Tags` | `string[]` | public |
| Property | `PrimaryCover` | `string?` | public |
| Property | `Version` | `int` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




