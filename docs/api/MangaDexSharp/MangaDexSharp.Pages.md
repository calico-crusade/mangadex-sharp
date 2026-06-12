# MangaDexSharp.Pages

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Pages/Pages.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Pages/Pages.cs#L6)

## Purpose

Represents the response of a pages request

This page exists so references to [Pages](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Pages/Pages.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `BaseUrl` | `string` | public |
| Property | `Chapter` | `ChapterData` | public |
| Property | `Hash` | `string` | public |
| Property | `Data` | `string[]` | public |
| Property | `DataSaver` | `string[]` | public |
| Method/ctor | `new` | `public ChapterData Chapter { get; set; } = new();` | public |
| Method/ctor | `GenerateImageLinks` | `public string[] Images => GenerateImageLinks();` | public |
| Method/ctor | `GenerateImageLinks` | `public string[] DataSaverImages => GenerateImageLinks(true);` | public |
| Method/ctor | `GenerateImageLinks` | `public string[] GenerateImageLinks(bool dataSaver = false) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




