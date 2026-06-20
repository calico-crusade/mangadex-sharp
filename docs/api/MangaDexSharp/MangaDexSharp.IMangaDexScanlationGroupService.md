# MangaDexSharp.IMangaDexScanlationGroupService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexScanlationGroupService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexScanlationGroupService.cs#L6)

## Purpose

Represents all of the requests related to scanlation groups

This page exists so references to [IMangaDexScanlationGroupService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexScanlationGroupService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.ScanlationGroup` or inject the service from a provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `List` | `Task<ScanlationGroupList> List(ScanlationGroupFilter? filter = null);` | public |
| Method/ctor | `Create` | `Task<MangaDexRoot<ScanlationGroup>> Create(ScanlationGroupCreate group, string? token = null);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<ScanlationGroup>> Get(string id);` | public |
| Method/ctor | `Update` | `Task<MangaDexRoot<ScanlationGroup>> Update(string id, ScanlationGroupUpdate group, string? token = null);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string id, string? token = null);` | public |
| Method/ctor | `Follow` | `Task<MangaDexRoot> Follow(string id, string? token = null);` | public |
| Method/ctor | `Unfollow` | `Task<MangaDexRoot> Unfollow(string id, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




