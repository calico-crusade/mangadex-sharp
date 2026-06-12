# MangaDexSharp.Extensions

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Extensions.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Extensions.cs#L6)

## Purpose

A bunch of useful extensions for MD related tasks

This page exists so references to [Extensions](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Extensions.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `CoverArt` | `public static CoverArtRelationship[] CoverArt(this IRelationshipModel? source) =>` | public |
| Method/ctor | `People` | `public static PersonRelationship[] People(this IRelationshipModel? source) =>` | public |
| Method/ctor | `Manga` | `public static RelatedDataRelationship[] Manga(this IRelationshipModel? source) =>` | public |
| Method/ctor | `ScanlationGroups` | `public static ScanlationGroup[] ScanlationGroups(this IRelationshipModel? source) =>` | public |
| Method/ctor | `Users` | `public static User[] Users(this IRelationshipModel? source) =>` | public |
| Method/ctor | `AddMangaDex` | `public static IServiceCollection AddMangaDex(this IServiceCollection services, Action<IMangaDexBuilder>? config = null) {` | public |
| Method/ctor | `MangaDexBuilder` | `var builder = new MangaDexBuilder(services);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




