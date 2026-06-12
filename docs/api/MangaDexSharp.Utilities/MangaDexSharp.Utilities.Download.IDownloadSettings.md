# MangaDexSharp.Utilities.Download.IDownloadSettings

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Download/DownloadSettings.cs:13](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadSettings.cs#L13)

## Purpose

An interface for editing download settings

This page exists so references to [IDownloadSettings](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadSettings.cs#L13) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `CacheDirectory` | `string` | public |
| Property | `PurgeCache` | `bool` | public |
| Property | `RateLimitTimeout` | `TimeSpan` | public |
| Property | `RateLimitAfter` | `int` | public |
| Property | `MaxRetries` | `int` | public |
| Property | `ParallelImages` | `int` | public |
| Property | `Token` | `CancellationToken` | public |
| Property | `ImageNameFactory` | `Func<ImageNameTransform, string>?` | public |
| Property | `ArchiveFactory` | `ArchiveFactory` | public |
| Property | `GroupingFactory` | `GroupingFactory` | public |
| Property | `RateLimitsEnabled` | `bool` | public |
| Method/ctor | `WithCacheDirectory` | `IDownloadSettings WithCacheDirectory(string dir);` | public |
| Method/ctor | `WithTempCacheDirectory` | `IDownloadSettings WithTempCacheDirectory();` | public |
| Method/ctor | `WithPurgeCache` | `IDownloadSettings WithPurgeCache(bool purge = true);` | public |
| Method/ctor | `WithGrouping` | `IDownloadSettings WithGrouping(FileGroupingType type);` | public |
| Method/ctor | `WithRateLimitTimeout` | `IDownloadSettings WithRateLimitTimeout(TimeSpan timeout);` | public |
| Method/ctor | `WithRateLimitTimeout` | `IDownloadSettings WithRateLimitTimeout(double seconds) =>` | public |
| Method/ctor | `WithRateLimitAfter` | `IDownloadSettings WithRateLimitAfter(int after);` | public |
| Method/ctor | `WithNoRateLimits` | `IDownloadSettings WithNoRateLimits() =>` | public |
| Method/ctor | `WithCancellationToken` | `IDownloadSettings WithCancellationToken(CancellationToken token);` | public |
| Method/ctor | `WithImageNameFactory` | `IDownloadSettings WithImageNameFactory(Func<ImageNameTransform, string> factory);` | public |
| Method/ctor | `WithOrdinalImageName` | `IDownloadSettings WithOrdinalImageName();` | public |
| Method/ctor | `WithParallelImages` | `IDownloadSettings WithParallelImages(int parallel);` | public |
| Method/ctor | `WithMaxRetries` | `IDownloadSettings WithMaxRetries(int retries);` | public |
| Method/ctor | `WithArchiveFactory` | `IDownloadSettings WithArchiveFactory(ArchiveFactory factory);` | public |
| Method/ctor | `WithDirectoryOutput` | `IDownloadSettings WithDirectoryOutput(string? directory = null, Func<string?, int, string>? subDirFactory = null);` | public |
| Method/ctor | `WithZipOutput` | `IDownloadSettings WithZipOutput(string? directory = null, Func<string?, int, string>? archiveNameFactory = null);` | public |
| Method/ctor | `WithCbzOutput` | `IDownloadSettings WithCbzOutput(string? directory = null, Func<string?, int, string>? archiveNameFactory = null);` | public |
| Method/ctor | `WithGroupingFactory` | `IDownloadSettings WithGroupingFactory(GroupingFactory factory);` | public |
| Method/ctor | `WithLogger` | `IDownloadSettings WithLogger(ILogger logger);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




