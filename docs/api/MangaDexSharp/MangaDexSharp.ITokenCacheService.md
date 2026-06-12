# MangaDexSharp.ITokenCacheService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Credentialing/ITokenCacheService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Credentialing/ITokenCacheService.cs#L6)

## Purpose

A service for caching the token and the time it was last executed

This page exists so references to [ITokenCacheService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Credentialing/ITokenCacheService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `GetCache` | `Task<(TokenResult? result, DateTime? executed)> GetCache();` | public |
| Method/ctor | `SetCache` | `Task SetCache(TokenResult? result, DateTime? executed);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




