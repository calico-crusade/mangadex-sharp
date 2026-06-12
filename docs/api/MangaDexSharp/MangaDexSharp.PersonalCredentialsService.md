# MangaDexSharp.PersonalCredentialsService

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Credentialing/PersonalCredentialsService.cs:9](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Credentialing/PersonalCredentialsService.cs#L9)

## Purpose

An instance of the [ICredentialsService](../MangaDexSharp/MangaDexSharp.ICredentialsService.md) that automatically fetches and refreshes the access token

This page exists so references to [PersonalCredentialsService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Credentialing/PersonalCredentialsService.cs#L9) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `new` | `private readonly SemaphoreSlim _lock = new(1);` | public |
| Method/ctor | `Expired` | `public bool AccessExpired => Expired(_last.token?.ExpiresIn);` | public |
| Method/ctor | `Expired` | `public bool RefreshExpired => Expired(_last.token?.RefreshExpiresIn);` | public |
| Method/ctor | `Expired` | `public bool Expired(double? seconds) {` | public |
| Method/ctor | `GetCache` | `public async Task<TokenResult?> GetCache() {` | public |
| Method/ctor | `SetCache` | `public async Task SetCache(TokenResult? token) {` | public |
| Method/ctor | `Fetch` | `public async Task<TokenResult?> Fetch() {` | public |
| Method/ctor | `SetCache` | `await SetCache(token);` | public |
| Method/ctor | `Refresh` | `public async Task<TokenResult?> Refresh(string refresh) {` | public |
| Method/ctor | `ClearCache` | `public async Task ClearCache() {` | public |
| Method/ctor | `GetToken` | `public virtual async Task<string?> GetToken() {` | public |
| Method/ctor | `Fetch` | `var last = await GetCache() ?? await Fetch();` | public |
| Method/ctor | `Refresh` | `var refresh = await Refresh(last.RefreshToken);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




