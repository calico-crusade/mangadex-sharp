# MangaDexSharp.Utilities.Download.ImageNameTransform

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Download/ImageNameTransform.cs:8](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/ImageNameTransform.cs#L8)

## Purpose

The parameters for the image name transformation.

This page exists so references to [ImageNameTransform](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Download/ImageNameTransform.cs#L8) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Original` | `string` | public |
| Property | `Name` | `string` | public |
| Property | `Extension` | `string` | public |
| Property | `Index` | `int` | public |
| Property | `Settings` | `IDownloadSettings` | public |
| Property | `TotalPages` | `int` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




