# MangaDexSharp.Utilities.Download.DownloadFile

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Download/DownloadFile.cs:9](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadFile.cs#L9)

## Purpose

Represents a file that will or has been downloaded.

This page exists so references to [DownloadFile](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/DownloadFile.cs#L9) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Url` | `string` | public |
| Property | `Chapter` | `Chapter` | public |
| Property | `TotalPages` | `int` | public |
| Property | `Index` | `int` | public |
| Property | `TotalRetries` | `int` | public |
| Property | `Output` | `string?` | public |
| Property | `Error` | `string?` | public |
| Property | `Manga` | `Manga?` | public |
| Property | `Status` | `DownloadStatus` | public |
| Method/ctor | `new` | `private readonly Stopwatch _watch = new();` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




