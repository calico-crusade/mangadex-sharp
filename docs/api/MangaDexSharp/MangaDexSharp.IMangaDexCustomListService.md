# MangaDexSharp.IMangaDexCustomListService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexCustomListService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexCustomListService.cs#L6)

## Purpose

Represents all of the requests on the /list endpoints

This page exists so references to [IMangaDexCustomListService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexCustomListService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.Lists` or inject `IMangaDexCustomListService` from a service provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Create` | `Task<MangaDexRoot<CustomList>> Create(CustomListCreate create, string? token = null);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<CustomList>> Get(string id, bool includeManga = false, string? token = null);` | public |
| Method/ctor | `Update` | `Task<MangaDexRoot<CustomList>> Update(string id, CustomListCreate create, string? token = null);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string id, string? token = null);` | public |
| Method/ctor | `Follow` | `Task<MangaDexRoot> Follow(string id, string? token = null);` | public |
| Method/ctor | `Unfollow` | `Task<MangaDexRoot> Unfollow(string id, string? token = null);` | public |
| Method/ctor | `MangaAdd` | `Task<MangaDexRoot> MangaAdd(string mangaId, string listId, int? order = null, string? token = null);` | public |
| Method/ctor | `MangaAdd` | `Task<MangaDexRoot> MangaAdd(string mangaId, string listId, string? token);` | public |
| Method/ctor | `MangaRemove` | `Task<MangaDexRoot> MangaRemove(string mangaId, string listId, int? order = null, string? token = null);` | public |
| Method/ctor | `MangaRemove` | `Task<MangaDexRoot> MangaRemove(string mangaId, string listId, string? token);` | public |
| Method/ctor | `List` | `Task<CustomListList> List(int limit = 100, int offset = 0, string? token = null);` | public |
| Method/ctor | `List` | `Task<CustomListList> List(string userId, int limit = 100, int offset = 0);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




