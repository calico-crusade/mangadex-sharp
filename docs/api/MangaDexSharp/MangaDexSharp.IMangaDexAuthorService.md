# MangaDexSharp.IMangaDexAuthorService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexAuthorService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexAuthorService.cs#L6)

## Purpose

Represents all of the requests on the /author endpoints

This page exists so references to [IMangaDexAuthorService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexAuthorService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.Author) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `List` | `Task<PersonList> List(AuthorFilter? filter = null);` | public |
| Method/ctor | `Create` | `Task<MangaDexRoot<PersonRelationship>> Create(AuthorCreate author, string? token = null);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<PersonRelationship>> Get(string id);` | public |
| Method/ctor | `Update` | `Task<MangaDexRoot<PersonRelationship>> Update(string id, AuthorCreate author, string? token = null);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string id, string? token = null);` | public |
| Method/ctor | `ListAll` | `IAsyncEnumerable<PersonRelationship> ListAll(AuthorFilter? filter = null, int? delay = null, int? rateCap = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




