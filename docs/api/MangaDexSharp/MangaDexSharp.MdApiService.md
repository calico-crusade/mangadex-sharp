# MangaDexSharp.MdApiService

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/ApiLayer/MdApiService.cs:34](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/MdApiService.cs#L34)

## Purpose

The default implementation of the IApiService The DI constructor

This page exists so references to [MdApiService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/MdApiService.cs#L34) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Auth` | `public async Task<Action<IHttpBuilder>> Auth(string? token, bool optional = false) {` | public |
| Method/ctor | `WrapUrl` | `public string WrapUrl(string url) {` | public |
| Method/ctor | `FillRateLimits` | `public void FillRateLimits(HttpResponseMessage resp, object? data) {` | public |
| Method/ctor | `RateLimit` | `var rateLimits = new RateLimit();` | public |
| Method/ctor | `Create` | `public override IHttpBuilder Create(string url, IJsonService? json, string? method, CancellationToken? token) {` | public |
| Method/ctor | `MdHttpBuilder` | `var builder = new MdHttpBuilder(_factory, _json, _rateLimiter);` | public |
| Method/ctor | `WrapUrl` | `var uri = WrapUrl(url);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




