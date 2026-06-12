# MangaDexSharp.Utilities.Download.IDownloadUtilityService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Download/DownloadUtilityService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadUtilityService.cs#L6)

## Purpose

Service for downloading chapters

This page exists so references to [IDownloadUtilityService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadUtilityService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Register `AddMangaDexUtils()` and inject the interface from dependency injection.

```csharp
using MangaDexSharp;
using Microsoft.Extensions.DependencyInjection;

var provider = new ServiceCollection()
    .AddMangaDex(c => c.AddMangaDexUtils())
    .BuildServiceProvider();
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Start` | `IDownloadInstance Start(Action<IDownloadSettings>? config = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




