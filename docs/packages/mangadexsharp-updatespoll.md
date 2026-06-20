# MangaDexSharp.UpdatesPoll Package

`MangaDexSharp.UpdatesPoll` provides [`IUpdatesPollService`](../api/MangaDexSharp.UpdatesPoll/MangaDexSharp.UpdatesPoll.IUpdatesPollService.md), a small polling service for detecting newly updated MangaDex chapters.

## Registration

```csharp
services.AddMangaDex();
services.AddTransient<IUpdatesPollService, UpdatesPollService>();
```

## Usage

```csharp
await poll.Poll(async chapters =>
{
    foreach (var item in chapters)
        Console.WriteLine($"{item.Chapter.Id}: {item.PageUrls.Length} pages");
}, since: DateTime.UtcNow.AddMinutes(-30), langs: ["en"], includePages: true);
```

[`UpdatesPollService`](../api/MangaDexSharp.UpdatesPoll/MangaDexSharp.UpdatesPoll.UpdatesPollService.md) uses [`IMangaDex.Chapter`](../api/MangaDexSharp/MangaDexSharp.IMangaDex.md) to query updated chapters and optionally [`IMangaDex.Pages`](../api/MangaDexSharp/MangaDexSharp.IMangaDex.md) to fetch page URLs. Results are returned as [`ChapterPages`](../api/MangaDexSharp.UpdatesPoll/MangaDexSharp.UpdatesPoll.ChapterPages.md).

## Configuration

The poller currently uses constants in [`UpdatesPollService`](../api/MangaDexSharp.UpdatesPoll/MangaDexSharp.UpdatesPoll.UpdatesPollService.md): a five-second poll delay, a small chapter-list request throttle, and a page-request throttle. MangaDex API configuration still comes from the core [`MangaDexSharp`](mangadexsharp.md) package.
