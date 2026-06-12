# MangaDexSharp.Utilities.Download.IImageDownloadQueue

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Download/ImageDownloadQueue.cs:12](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/ImageDownloadQueue.cs#L12)

## Purpose

A utility for downloading images

This page exists so references to [IImageDownloadQueue](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/ImageDownloadQueue.cs#L12) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

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
| Property | `AllFiles` | `IEnumerable<DownloadFile>` | public |
| Property | `Downloaded` | `IAsyncEnumerable<DownloadFile>` | public |
| Method/ctor | `Initialize` | `void Initialize();` | public |
| Method/ctor | `WaitToFinish` | `Task WaitToFinish();` | public |
| Method/ctor | `Queue` | `ValueTask Queue(DownloadFile file);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




