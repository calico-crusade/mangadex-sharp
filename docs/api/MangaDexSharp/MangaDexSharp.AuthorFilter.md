# MangaDexSharp.AuthorFilter

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Author/AuthorFilter.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Author/AuthorFilter.cs#L6)

## Purpose

Represents a query parameter filter for the authors endpoints

This page exists so references to [AuthorFilter](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Author/AuthorFilter.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Ids` | `string[]` | public |
| Property | `NameOrder` | `OrderValue?` | public |
| Property | `Includes` | `MangaIncludes[]` | public |
| Method/ctor | `BuildQuery` | `public string BuildQuery() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




