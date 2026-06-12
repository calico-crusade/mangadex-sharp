# MangaDexSharp.MangaDexApiConfigBuilder

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Configuration/MangaDexApiConfigBuilder.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/MangaDexApiConfigBuilder.cs#L6)

## Purpose

The builder for the [IConfigurationApi](../MangaDexSharp/MangaDexSharp.IConfigurationApi.md) service

This page exists so references to [MangaDexApiConfigBuilder](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/MangaDexApiConfigBuilder.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `ApiUrl` | `string?` | public |
| Property | `UserAgent` | `string?` | public |
| Property | `ThrowOnError` | `bool?` | public |
| Property | `ConservativeLimits` | `bool?` | public |
| Property | `HandleRateLimits` | `bool?` | public |
| Method/ctor | `WithApiUrl` | `public MangaDexApiConfigBuilder WithApiUrl(string? url) {` | public |
| Method/ctor | `WithDevApi` | `public MangaDexApiConfigBuilder WithDevApi() {` | public |
| Method/ctor | `WithApiUrl` | `return WithApiUrl(ConfigurationApi.API_ROOT_DEV);` | public |
| Method/ctor | `WithUserAgent` | `public MangaDexApiConfigBuilder WithUserAgent(string? userAgent) {` | public |
| Method/ctor | `ThrowExceptionOnError` | `public MangaDexApiConfigBuilder ThrowExceptionOnError() {` | public |
| Method/ctor | `FailGracefully` | `public MangaDexApiConfigBuilder FailGracefully() {` | public |
| Method/ctor | `WithAutoRateLimits` | `public MangaDexApiConfigBuilder WithAutoRateLimits(bool enabled = true, bool conservative = true) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




