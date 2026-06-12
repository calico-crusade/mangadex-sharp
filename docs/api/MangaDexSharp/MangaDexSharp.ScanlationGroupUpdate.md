# MangaDexSharp.ScanlationGroupUpdate

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/ScanlationGroup/ScanlationGroupUpdate.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/ScanlationGroup/ScanlationGroupUpdate.cs#L6)

## Purpose

Represents a request to update a scanlation group

This page exists so references to [ScanlationGroupUpdate](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/ScanlationGroup/ScanlationGroupUpdate.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Leader` | `string?` | public |
| Property | `Members` | `string[]` | public |
| Property | `FocusedLanguages` | `string[]` | public |
| Property | `Website` | `string?` | public |
| Property | `IrcServer` | `string?` | public |
| Property | `IrcChannel` | `string?` | public |
| Property | `Discord` | `string?` | public |
| Property | `ContactEmail` | `string?` | public |
| Property | `Description` | `string?` | public |
| Property | `Twitter` | `string?` | public |
| Property | `MangaUpdates` | `string?` | public |
| Property | `Inactive` | `bool` | public |
| Property | `PublishDelay` | `string?` | public |
| Property | `Locked` | `bool` | public |
| Property | `Version` | `int` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




