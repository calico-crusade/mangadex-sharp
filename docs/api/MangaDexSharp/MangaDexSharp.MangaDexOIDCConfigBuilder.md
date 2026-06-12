# MangaDexSharp.MangaDexOIDCConfigBuilder

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Configuration/MangaDexOIDCConfigBuilder.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/MangaDexOIDCConfigBuilder.cs#L6)

## Purpose

The builder for the [IConfigurationOIDC](../MangaDexSharp/MangaDexSharp.IConfigurationOIDC.md) service

This page exists so references to [MangaDexOIDCConfigBuilder](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/MangaDexOIDCConfigBuilder.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `AuthUrl` | `string?` | public |
| Property | `RealmPath` | `string?` | public |
| Property | `ClientId` | `string?` | public |
| Property | `ClientSecret` | `string?` | public |
| Property | `Username` | `string?` | public |
| Property | `Password` | `string?` | public |
| Method/ctor | `WithAuthUrl` | `public MangaDexOIDCConfigBuilder WithAuthUrl(string? url) {` | public |
| Method/ctor | `WithDevAuthUrl` | `public MangaDexOIDCConfigBuilder WithDevAuthUrl() {` | public |
| Method/ctor | `WithRealmPath` | `public MangaDexOIDCConfigBuilder WithRealmPath(string? path) {` | public |
| Method/ctor | `WithClientId` | `public MangaDexOIDCConfigBuilder WithClientId(string? id) {` | public |
| Method/ctor | `WithClientSecret` | `public MangaDexOIDCConfigBuilder WithClientSecret(string? secret) {` | public |
| Method/ctor | `WithUsername` | `public MangaDexOIDCConfigBuilder WithUsername(string? username) {` | public |
| Method/ctor | `WithPassword` | `public MangaDexOIDCConfigBuilder WithPassword(string? password) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




