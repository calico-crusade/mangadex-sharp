# MangaDexSharp.Utilities.Upload.IUploadUtilityService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Upload/UploadUtilityService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Upload/UploadUtilityService.cs#L6)

## Purpose

Service for starting upload sessions

This page exists so references to [IUploadUtilityService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Upload/UploadUtilityService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `CloseExisting` | `Task CloseExisting(Action<IUploadSettings>? config = null);` | public |
| Method/ctor | `New` | `Task<IUploadInstance> New(string manga, string[] groups, Action<IUploadSettings>? config = null);` | public |
| Method/ctor | `Continue` | `Task<IUploadInstance> Continue(Action<IUploadSettings>? config = null);` | public |
| Method/ctor | `Edit` | `Task<IUploadInstance> Edit(string chapterId, int version, Action<IUploadSettings>? config = null);` | public |
| Method/ctor | `Edit` | `Task<IUploadInstance> Edit(Chapter chapter, Action<IUploadSettings>? config = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




