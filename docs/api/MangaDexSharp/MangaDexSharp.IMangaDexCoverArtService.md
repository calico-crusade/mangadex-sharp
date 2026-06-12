# MangaDexSharp.IMangaDexCoverArtService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexCoverArtService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexCoverArtService.cs#L6)

## Purpose

Represents all of the requests on the /cover endpoints

This page exists so references to [IMangaDexCoverArtService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexCoverArtService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.CoverArt) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `List` | `Task<CoverArtList> List(CoverArtFilter? filter = null);` | public |
| Method/ctor | `Upload` | `Task<MangaDexRoot<CoverArtRelationship>> Upload(string mangaOrCoverId, CoverArtCreate cover, string? token = null);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<CoverArtRelationship>> Get(string mangaOrCoverId);` | public |
| Method/ctor | `Update` | `Task<MangaDexRoot<CoverArtRelationship>> Update(string mangaOrCoverId, CoverArtUpdate cover, string? token = null);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string mangaOrCoverId, string? token = null);` | public |
| Method/ctor | `ListAll` | `IAsyncEnumerable<CoverArtRelationship> ListAll(CoverArtFilter? filter = null, int? delay = null, int? rateCap = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




