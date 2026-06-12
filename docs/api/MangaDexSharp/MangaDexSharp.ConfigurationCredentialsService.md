# MangaDexSharp.ConfigurationCredentialsService

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Credentialing/ConfigurationCredentialsService.cs:7](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Credentialing/ConfigurationCredentialsService.cs#L7)

## Purpose

Represents a provider that fetches the [ICredentialsService](../MangaDexSharp/MangaDexSharp.ICredentialsService.md) from the configuration

This page exists so references to [ConfigurationCredentialsService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Credentialing/ConfigurationCredentialsService.cs#L7) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `TokenPath` | `static string` | public |
| Method/ctor | `GetToken` | `public virtual Task<string?> GetToken() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




