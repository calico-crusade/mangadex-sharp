# MangaDexSharp.IMangaDexAuthService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexAuthService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexAuthService.cs#L6)

## Purpose

Represents all of the requests for the auth.mangadex.org service

This page exists so references to [IMangaDexAuthService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexAuthService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.Auth` or inject `IMangaDexAuthService` from a service provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Check` | `Task<AuthCheck> Check(string? token = null);` | public |
| Method/ctor | `Logout` | `Task<AuthLogout> Logout(string? token = null);` | public |
| Method/ctor | `Request` | `Task<TokenResult> Request(TokenRequest request);` | public |
| Method/ctor | `Personal` | `Task<TokenResult> Personal( string? id = null, string? secret = null, string? username = null, string? password = null);` | public |
| Method/ctor | `Refresh` | `Task<TokenResult> Refresh(string refreshToken, string? id = null, string? secret = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




