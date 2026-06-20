# MangaDexSharp.IMangaDexFollowsService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexFollowsService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexFollowsService.cs#L6)

## Purpose

Represents all of the different requests relating to objects the current user follows

This page exists so references to [IMangaDexFollowsService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexFollowsService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.Follows` or inject the service from a provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Groups` | `Task<ScanlationGroupList> Groups(int offset = 0, int limit = 100, string? token = null);` | public |
| Method/ctor | `Users` | `Task<UserList> Users(int offset = 0, int limit = 100, string? token = null);` | public |
| Method/ctor | `User` | `Task<MangaDexRoot> User(string userId, string? token = null);` | public |
| Method/ctor | `Manga` | `Task<MangaList> Manga(int offset = 0, int limit = 100, MangaIncludes[]? includes = null, string? token = null);` | public |
| Method/ctor | `Manga` | `Task<MangaDexRoot> Manga(string mangaId, string? token = null);` | public |
| Method/ctor | `Lists` | `Task<CustomListList> Lists(int offset = 0, int limit = 100, string? token = null);` | public |
| Method/ctor | `List` | `Task<MangaDexRoot> List(string listId, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




