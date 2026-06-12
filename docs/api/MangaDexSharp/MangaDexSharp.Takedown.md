# MangaDexSharp.Takedown

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Takedown/Takedown.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Takedown/Takedown.cs#L6)

## Purpose

Represents a takedown notice returned from the MD api

This page exists so references to [Takedown](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Takedown/Takedown.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Relationships` | `IRelationship[]` | public |
| Property | `OriginalWork` | `string?` | public |
| Property | `Owner` | `CopyrightOwner?` | public |
| Property | `PreferredAction` | `string?` | public |
| Property | `CreatedAt` | `DateTime` | public |
| Property | `UpdatedAt` | `DateTime` | public |
| Property | `Version` | `int` | public |
| Property | `IsPermanent` | `bool` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




