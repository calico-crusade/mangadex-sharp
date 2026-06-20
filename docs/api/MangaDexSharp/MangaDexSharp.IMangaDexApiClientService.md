# MangaDexSharp.IMangaDexApiClientService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexApiClientService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexApiClientService.cs#L6)

## Purpose

Represents all of the requests related to API Clients

This page exists so references to [IMangaDexApiClientService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexApiClientService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.ApiClient` or inject the service from a provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Mine` | `Task<ApiClientList> Mine(ApiClientFilter? filter = null, string? token = null);` | public |
| Method/ctor | `MineAll` | `IAsyncEnumerable<ApiClient> MineAll(ApiClientFilter? filter = null, string? token = null, int? delay = null, int? rateCap = null);` | public |
| Method/ctor | `Create` | `Task<MangaDexRoot<ApiClient>> Create(ApiClientData data, string? token = null);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<ApiClient>> Get(string id, string? token = null, ApiClientIncludes[]? includes = null);` | public |
| Method/ctor | `Update` | `Task<MangaDexRoot<ApiClient>> Update(string id, ApiClientUpdateData data, string? token = null);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string id, string? token = null, int? version = null);` | public |
| Method/ctor | `Secret` | `Task<MangaDexStruct<string>> Secret(string id, string? token = null);` | public |
| Method/ctor | `Regenerate` | `Task<MangaDexStruct<string>> Regenerate(string id, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




