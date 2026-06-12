# MangaDexSharp.ReportFilter

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Report/ReportFilter.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Report/ReportFilter.cs#L6)

## Purpose

Represents the available query parameters for the report endpoint

This page exists so references to [ReportFilter](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Report/ReportFilter.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `DefaultLimit` | `static int` | public |
| Property | `Limit` | `int` | public |
| Property | `Offset` | `int` | public |
| Property | `Category` | `ReportCategory?` | public |
| Property | `ReasonId` | `string?` | public |
| Property | `ObjectId` | `string?` | public |
| Property | `State` | `ReportState?` | public |
| Property | `CreatedAtOrder` | `OrderValue?` | public |
| Method/ctor | `BuildQuery` | `public string BuildQuery() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




