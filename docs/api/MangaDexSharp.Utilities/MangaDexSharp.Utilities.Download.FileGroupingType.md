# MangaDexSharp.Utilities.Download.FileGroupingType

- **Kind:** `enum`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Download/FileGroupingType.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/FileGroupingType.cs#L6)

## Purpose

How to group chapters when creating archives

This page exists so references to [FileGroupingType](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/FileGroupingType.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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

| Value |
| --- |
| `SingleFile` |
| `Volumes` |
| `Chapters` |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




