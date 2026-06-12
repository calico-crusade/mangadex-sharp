# MangaDexSharp.ConfigurationApi

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Configuration/ConfigurationApi.cs:37](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/ConfigurationApi.cs#L37)

## Purpose

The configuration options for the [MangaDex](../MangaDexSharp/MangaDexSharp.MangaDex.md) API services

This page exists so references to [ConfigurationApi](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Configuration/ConfigurationApi.cs#L37) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `ApiPath` | `static string` | public |
| Property | `UserAgentPath` | `static string` | public |
| Property | `ErrorThrownPath` | `static string` | public |
| Property | `ConservativeLimitsPath` | `static string` | public |
| Property | `HandleRateLimitsPath` | `static string` | public |
| Property | `ApiUrl` | `string` | public |
| Property | `UserAgent` | `string` | public |
| Property | `ThrowOnError` | `bool` | public |
| Property | `ConservativeLimits` | `bool` | public |
| Property | `HandleRateLimits` | `bool` | public |
| Method/ctor | `FromConfiguration` | `public static IConfigurationApi FromConfiguration(IConfiguration config) {` | public |
| Method/ctor | `GetBool` | `bool GetBool(string path, bool defValue) {` | public |
| Method/ctor | `FromHardCoded` | `public static IConfigurationApi FromHardCoded( string? apiUrl = null, string? userAgent = null, bool? throwOnError = null, bool? handleRateLimits = null, bool? conservativeLimits = null) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




