# MangaDexSharp.CoverArtRelationship

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Relationships/CoverArtRelationship.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Relationships/CoverArtRelationship.cs#L6)

## Purpose

Represents the relationship between cover art and the parent manga

This page exists so references to [CoverArtRelationship](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Relationships/CoverArtRelationship.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Description` | `string` | public |
| Property | `Volume` | `string` | public |
| Property | `FileName` | `string` | public |
| Property | `Locale` | `string` | public |
| Property | `CreatedAt` | `DateTime` | public |
| Property | `UpdatedAt` | `DateTime` | public |
| Property | `Version` | `int` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




