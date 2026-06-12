# MangaDexSharp.IMangaDexTakedownService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexMiscService.cs:86](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L86)

## Purpose

Represents all of the requests related to takedown notices

This page exists so references to [IMangaDexTakedownService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L86) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.Takedown) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Takedowns` | `Task<TakedownList> Takedowns(int limit = 20, int offset = 0, Dictionary<TakedownOrder, OrderValue>? orderBy = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




