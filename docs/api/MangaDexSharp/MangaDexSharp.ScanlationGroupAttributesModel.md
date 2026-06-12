# MangaDexSharp.ScanlationGroupAttributesModel

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/ScanlationGroup/ScanlationGroup.cs:17](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/ScanlationGroup/ScanlationGroup.cs#L17)

## Purpose

The properties of the scanlation group

This page exists so references to [ScanlationGroupAttributesModel](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/ScanlationGroup/ScanlationGroup.cs#L17) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Name` | `string` | public |
| Property | `AltNames` | `Localization[]` | public |
| Property | `Locked` | `bool` | public |
| Property | `Website` | `string?` | public |
| Property | `IrcServer` | `string?` | public |
| Property | `IrcChannel` | `string?` | public |
| Property | `Discord` | `string?` | public |
| Property | `ContactEmail` | `string?` | public |
| Property | `Description` | `string?` | public |
| Property | `Twitter` | `string?` | public |
| Property | `MangaUpdates` | `string?` | public |
| Property | `FocusedLanguages` | `string[]` | public |
| Property | `Official` | `bool` | public |
| Property | `Verified` | `bool` | public |
| Property | `Inactive` | `bool` | public |
| Property | `CreatedAt` | `DateTime` | public |
| Property | `UpdatedAt` | `DateTime` | public |
| Property | `Version` | `int` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




