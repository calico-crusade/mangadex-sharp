# MangaDexSharp.IMangaDexMangaService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexMangaService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMangaService.cs#L6)

## Purpose

Represents all of the requests related to manga

This page exists so references to [IMangaDexMangaService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMangaService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.Manga) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `List` | `Task<MangaList> List(MangaFilter? filter = null);` | public |
| Method/ctor | `Create` | `Task<MangaDexRoot<Manga>> Create(MangaCreate manga, string? bearer = null);` | public |
| Method/ctor | `Aggregate` | `Task<MangaAggregate> Aggregate(string id, string[]? languages = null, string[]? groups = null, bool includeUnavailable = true);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<Manga>> Get(string id, MangaIncludes[]? includes = null);` | public |
| Method/ctor | `Update` | `Task<MangaDexRoot<Manga>> Update(string id, MangaCreate manga, string? token = null);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string id, string? token = null);` | public |
| Method/ctor | `Unfollow` | `Task<MangaDexRoot> Unfollow(string id, string? token = null);` | public |
| Method/ctor | `Follow` | `Task<MangaDexRoot> Follow(string id, string? token = null);` | public |
| Method/ctor | `Feed` | `Task<ChapterList> Feed(string id, MangaFeedFilter? filter = null);` | public |
| Method/ctor | `Random` | `Task<MangaDexRoot<Manga>> Random(MangaRandomFilter? filter = null);` | public |
| Method/ctor | `Tags` | `Task<TagList> Tags();` | public |
| Method/ctor | `Status` | `Task<MangaReadStatuses> Status(ReadStatus? status = null, string? token = null);` | public |
| Method/ctor | `Status` | `Task<MangaReadStatuses> Status(string id, string? token = null);` | public |
| Method/ctor | `Status` | `Task<MangaDexRoot> Status(string id, ReadStatus? status, string? token = null);` | public |
| Method/ctor | `Draft` | `Task<MangaDexRoot<Manga>> Draft(string id, MangaIncludes[]? includes = null, string? token = null);` | public |
| Method/ctor | `DraftCommit` | `Task<MangaDexRoot<Manga>> DraftCommit(string id, int version = 1, string? token = null);` | public |
| Method/ctor | `Drafts` | `Task<MangaList> Drafts(MangaDraftFilter? filter = null, string? token = null);` | public |
| Method/ctor | `Relations` | `Task<MangaRelationships> Relations(string id);` | public |
| Method/ctor | `Relation` | `Task<MangaDexRoot<MangaRelationship>> Relation(string id, Relationships relation, string target, string? token = null);` | public |
| Method/ctor | `RelationDelete` | `Task<MangaDexRoot> RelationDelete(string mangaId, string id, string? token = null);` | public |
| Method/ctor | `Recommendations` | `Task<RecommendationList> Recommendations(string mangaId);` | public |
| Method/ctor | `ListAll` | `IAsyncEnumerable<Manga> ListAll(MangaFilter? filter = null, int? delay = null, int? rateCap = null);` | public |
| Method/ctor | `FeedAll` | `IAsyncEnumerable<Chapter> FeedAll(string id, MangaFeedFilter? filter = null, int? delay = null, int? rateCap = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




