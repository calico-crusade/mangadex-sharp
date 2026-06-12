# MangaDexSharp.IMangaDexBuilder

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Configuration/MangaDexBuilder.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/MangaDexBuilder.cs#L6)

## Purpose

A service for building the [MangaDex](../MangaDexSharp/MangaDexSharp.MangaDex.md) API client configuration

This page exists so references to [IMangaDexBuilder](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/MangaDexBuilder.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `WithApiConfig` | `IMangaDexBuilder WithApiConfig(IConfigurationApi config);` | public |
| Method/ctor | `WithApiConfig` | `IMangaDexBuilder WithApiConfig( string? apiUrl = null, string? userAgent = null, bool throwOnError = false, bool rateLimits = true, bool conservative = true);` | public |
| Method/ctor | `WithApiConfig` | `IMangaDexBuilder WithApiConfig(Action<MangaDexApiConfigBuilder> config);` | public |
| Method/ctor | `WithAuthConfig` | `IMangaDexBuilder WithAuthConfig(IConfigurationOIDC config);` | public |
| Method/ctor | `WithAuthConfig` | `IMangaDexBuilder WithAuthConfig( string clientId, string clientSecret, string username, string password, string? authUrl = null, string? realmPath = null);` | public |
| Method/ctor | `WithAuthConfig` | `IMangaDexBuilder WithAuthConfig(Action<MangaDexOIDCConfigBuilder> config);` | public |
| Method/ctor | `WithCredentials` | `IMangaDexBuilder WithCredentials(ICredentialsService credentials);` | public |
| Method/ctor | `WithCredentials` | `IMangaDexBuilder WithCredentials(Func<IServiceProvider, ICredentialsService> config);` | public |
| Method/ctor | `WithAccessToken` | `IMangaDexBuilder WithAccessToken(string token);` | public |
| Method/ctor | `WithAccessTokenFromConfig` | `IMangaDexBuilder WithAccessTokenFromConfig();` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




