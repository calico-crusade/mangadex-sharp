# MangaDexSharp.IMangaDexReadMarkerService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexReadMarkerService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexReadMarkerService.cs#L6)

## Purpose

Represents all requests related to read status markers

This page exists so references to [IMangaDexReadMarkerService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexReadMarkerService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.ReadMarker` or inject the service from a provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Read` | `Task<ReadMarkerList> Read(string mangaId, string? token = null);` | public |
| Method/ctor | `Read` | `Task<ReadMarkerList> Read(string[] mangaIds, bool grouped = true, string? token = null);` | public |
| Method/ctor | `BatchUpdate` | `Task<MangaDexRoot> BatchUpdate(string mangaId, string[] chapterIds, bool read, bool updateHistory = true, string? token = null);` | public |
| Method/ctor | `BatchUpdate` | `Task<MangaDexRoot> BatchUpdate(string mangaId, ReadMarkerBatchUpdate update, bool updateHistory = true, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




