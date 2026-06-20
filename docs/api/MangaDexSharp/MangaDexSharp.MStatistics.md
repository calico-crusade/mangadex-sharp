# MangaDexSharp.MStatistics

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Statistics/MangaStatistics.cs:39](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Statistics/MangaStatistics.cs#L39)

## Purpose

This type is part of the documented API surface.

This page exists so references to [MStatistics](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Statistics/MangaStatistics.cs#L39) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Rating` | `RatingStats` | public |
| Property | `Follows` | `int` | public |
| Property | `UnavailableChaptersCount` | `int` | public |
| Method/ctor | `new` | `public RatingStats Rating { get; set; } = new();` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




