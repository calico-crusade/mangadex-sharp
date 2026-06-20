# MangaDexSharp.Utilities Package

`MangaDexSharp.Utilities` adds higher-level workflows on top of [`MangaDexSharp`](mangadexsharp.md): downloading chapters, uploading/editing chapter drafts, and wrapping requests with utility rate limiting.

## Install

```powershell
PM> Install-Package MangaDexSharp.Utilities
```

## Registration

```csharp
using MangaDexSharp;
using Microsoft.Extensions.DependencyInjection;

var provider = new ServiceCollection()
    .AddMangaDex(c => c
        .WithAuthConfig(clientId, clientSecret, username, password)
        .AddMangaDexUtils())
    .BuildServiceProvider();
```

[`DiExtensions.AddMangaDexUtils`](../api/MangaDexSharp.Utilities/MangaDexSharp.DiExtensions.md) registers [`IRateLimitService`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.IRateLimitService.md), [`IUploadUtilityService`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Upload.IUploadUtilityService.md), and [`IDownloadUtilityService`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.IDownloadUtilityService.md).

## Downloading Chapters

Use [`IDownloadUtilityService`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.IDownloadUtilityService.md) to create a [`DownloadInstance`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.IDownloadInstance.md), configure output via [`DownloadSettings`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.IDownloadSettings.md), and download chapter images into ZIP, CBZ, or directories.

```csharp
var api = provider.GetRequiredService<IMangaDex>();
var downloader = provider.GetRequiredService<IDownloadUtilityService>();

var manga = await api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");
var chapters = await api.Manga.Feed(manga.Data.Id, new MangaFeedFilter
{
    TranslatedLanguage = ["en"],
    Order = new()
    {
        [MangaFeedFilter.OrderKey.chapter] = OrderValue.asc
    }
});

using var download = downloader.Start(c => c
    .WithZipOutput("downloads")
    .WithGrouping(FileGroupingType.Volumes)
    .WithOrdinalImageName());

await download.DownloadChapters(chapters, manga.Data);
await download.WaitUntilFinish();
```

Key configuration types are [`ArchiveType`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.ArchiveType.md), [`FileGroupingType`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.FileGroupingType.md), [`ImageNameTransform`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.ImageNameTransform.md), and [`DownloadStatus`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.DownloadStatus.md).

## Uploading Chapters

Use [`IUploadUtilityService`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Upload.IUploadUtilityService.md) to create or continue upload sessions. [`UploadInstance`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Upload.IUploadInstance.md) owns file upload, delete, commit, and cleanup behavior.

```csharp
var upload = provider.GetRequiredService<IUploadUtilityService>();
await using var session = await upload.New(mangaId, [groupId], c => c.MaxBatchSize(5));

await session.UploadFile("001.png");
await session.UploadFile("002.png");

var chapter = await session.Commit(new ChapterDraft
{
    Volume = "1",
    Chapter = "1",
    Title = "Example Chapter",
    TranslatedLanguage = "en"
});
```

Continue an existing upload session:

```csharp
await using var session = await upload.Continue();
await session.UploadFile("003.png");
```

Edit an existing chapter:

```csharp
await using var session = await upload.Edit("2c98fbe9-a63f-47c2-9862-ecc9199610a2");
await session.UploadFile("replacement-page.png");
```

## Rate-Limited Requests

[`IRateLimitService`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.IRateLimitService.md) centralizes calls through [`IMangaDex`](../api/MangaDexSharp/MangaDexSharp.IMangaDex.md) and can run paginated request flows while applying [`RateLimitSettings`](../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.RateLimitSettings.md) callbacks.

```csharp
var rates = provider.GetRequiredService<IRateLimitService>();
var manga = await rates.Request(api => api.Manga.Get(mangaId), CancellationToken.None);
```

See [MangaDexSharp.Utilities API Reference](../api/README.md#mangadexsharputilities) for every public utility class, method, property, and source link.

