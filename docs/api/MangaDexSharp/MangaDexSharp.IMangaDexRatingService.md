# MangaDexSharp.IMangaDexRatingService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexMiscService.cs:26](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L26)

## Purpose

Represents all of the requests relating to rating manga

This page exists so references to [IMangaDexRatingService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L26) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.Rating` or inject the service from a provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Ratings` | `Task<RatingList> Ratings(string[] mangaIds, string? token = null);` | public |
| Method/ctor | `Rate` | `Task<MangaDexRoot> Rate(string mangaId, int rating, string? token = null);` | public |
| Method/ctor | `RatingDelete` | `Task<MangaDexRoot> RatingDelete(string mangaId, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




