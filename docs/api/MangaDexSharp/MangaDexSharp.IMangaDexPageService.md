# MangaDexSharp.IMangaDexPageService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexMiscService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L6)

## Purpose

Represents all of the requests for pages and md-at-home

This page exists so references to [IMangaDexPageService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.Page) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Pages` | `Task<Pages> Pages(string chapterId);` | public |
| Method/ctor | `ReportPage` | `Task ReportPage(PageReport report);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




