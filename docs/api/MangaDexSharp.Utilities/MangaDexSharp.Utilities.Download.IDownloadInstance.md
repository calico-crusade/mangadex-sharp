# MangaDexSharp.Utilities.Download.IDownloadInstance

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Download/DownloadInstance.cs:12](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadInstance.cs#L12)

## Purpose

Represents a download session

This page exists so references to [IDownloadInstance](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadInstance.cs#L12) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `IsDownloading` | `bool` | public |
| Property | `Elapsed` | `TimeSpan` | public |
| Property | `TotalDownloaded` | `int` | public |
| Property | `TotalQueued` | `int` | public |
| Property | `TotalActive` | `int` | public |
| Property | `TotalFailed` | `int` | public |
| Property | `ImageUrls` | `IEnumerable<string>` | public |
| Property | `Files` | `IEnumerable<DownloadFile>` | public |
| Method/ctor | `DownloadManga` | `Task DownloadManga(string mangaId, MangaFeedFilter? filter = null);` | public |
| Method/ctor | `DownloadManga` | `Task DownloadManga(Manga manga, MangaFeedFilter? filter = null);` | public |
| Method/ctor | `DownloadChapters` | `Task DownloadChapters(ChapterList chapters, Manga? manga = null);` | public |
| Method/ctor | `DownloadChapter` | `Task DownloadChapter(string chapterId, Manga? manga = null);` | public |
| Method/ctor | `DownloadChapter` | `Task DownloadChapter(Chapter chapter, Manga? manga = null);` | public |
| Method/ctor | `DownloadChapters` | `Task DownloadChapters(IAsyncEnumerable<Chapter> chapters, Manga? manga = null);` | public |
| Method/ctor | `WaitUntilFinish` | `Task WaitUntilFinish();` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




