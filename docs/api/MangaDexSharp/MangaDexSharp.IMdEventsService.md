# MangaDexSharp.IMdEventsService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/MdEventsService.cs:9](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MdEventsService.cs#L9)

## Purpose

A service that bundles and runs all of the implemented IMdEventServices

This page exists so references to [IMdEventsService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MdEventsService.cs#L9) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Bind` | `void Bind(string url, IHttpBuilder config);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




