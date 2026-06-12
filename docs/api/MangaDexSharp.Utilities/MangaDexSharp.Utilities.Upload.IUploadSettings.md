# MangaDexSharp.Utilities.Upload.IUploadSettings

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Upload/UploadSettings.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Upload/UploadSettings.cs#L6)

## Purpose

An interface for editing upload settings

This page exists so references to [IUploadSettings](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Upload/UploadSettings.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `AbandonSessionOnDispose` | `bool` | public |
| Property | `MaxBatchSize` | `int` | public |
| Property | `Token` | `CancellationToken` | public |
| Property | `PageOrderFactory` | `Func<IEnumerable<UploadSessionFile>, string[]>` | public |
| Property | `AuthTokenFactory` | `Func<Task<string>>?` | public |
| Method/ctor | `DoNotAbandonSessionOnDispose` | `IUploadSettings DoNotAbandonSessionOnDispose(bool enabled = false);` | public |
| Method/ctor | `WithAuthToken` | `IUploadSettings WithAuthToken(string token);` | public |
| Method/ctor | `WithAuthToken` | `IUploadSettings WithAuthToken(Func<Task<string>> token);` | public |
| Method/ctor | `WithMaxBatchSize` | `IUploadSettings WithMaxBatchSize(int size);` | public |
| Method/ctor | `WithNoBatchBuffering` | `IUploadSettings WithNoBatchBuffering() =>` | public |
| Method/ctor | `WithCancellationToken` | `IUploadSettings WithCancellationToken(CancellationToken token);` | public |
| Method/ctor | `WithPageOrderFactory` | `IUploadSettings WithPageOrderFactory(Func<IEnumerable<UploadSessionFile>, string[]> factory);` | public |
| Method/ctor | `WithPageOrderFactory` | `IUploadSettings WithPageOrderFactory(Func<IEnumerable<UploadSessionFile>, IOrderedEnumerable<UploadSessionFile>> factory);` | public |
| Method/ctor | `WithLogging` | `IUploadSettings WithLogging(ILogger logger);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




