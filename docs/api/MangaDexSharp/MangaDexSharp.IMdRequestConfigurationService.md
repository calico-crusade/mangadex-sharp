# MangaDexSharp.IMdRequestConfigurationService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/IMdRequestConfigurationService.cs:20](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/IMdRequestConfigurationService.cs#L20)

## Purpose

A service for adding custom configuration to the underlying HTTP requests that MD api makes You should only register one of these and you should only do it if you know what you are doing.

This page exists so references to [IMdRequestConfigurationService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/IMdRequestConfigurationService.cs#L20) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Configure` | `void Configure(string uri, IHttpBuilder config);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




