# MangaDexSharp.ConfigurationOIDC

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Configuration/ConfigurationOIDC.cs:42](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/ConfigurationOIDC.cs#L42)

## Purpose

The configuration options for Open-ID Connect (OIDC) services

This page exists so references to [ConfigurationOIDC](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/ConfigurationOIDC.cs#L42) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `AuthPath` | `static string` | public |
| Property | `ClientIdPath` | `static string` | public |
| Property | `ClientSecretPath` | `static string` | public |
| Property | `UsernamePath` | `static string` | public |
| Property | `PasswordPath` | `static string` | public |
| Property | `RealmPathPath` | `static string` | public |
| Property | `AuthUrl` | `string` | public |
| Property | `RealmPath` | `string` | public |
| Property | `ClientId` | `string?` | public |
| Property | `ClientSecret` | `string?` | public |
| Property | `Username` | `string?` | public |
| Property | `Password` | `string?` | public |
| Method/ctor | `FromConfiguration` | `public static IConfigurationOIDC FromConfiguration(IConfiguration config) {` | public |
| Method/ctor | `FromHardCoded` | `public static IConfigurationOIDC FromHardCoded( string? clientId = null, string? clientSecret = null, string? username = null, string? password = null, string? authUrl = null, string? realmPath = null) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




