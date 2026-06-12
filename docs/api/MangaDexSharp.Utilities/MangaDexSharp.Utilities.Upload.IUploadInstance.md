# MangaDexSharp.Utilities.Upload.IUploadInstance

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/Upload/UploadInstance.cs:9](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Upload/UploadInstance.cs#L9)

## Purpose

Represents an upload session

This page exists so references to [IUploadInstance](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/Upload/UploadInstance.cs#L9) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Attributes` | `UploadSession.UploadSessionAttributesModel` | public |
| Property | `SessionId` | `string` | public |
| Property | `IsCommitted` | `bool` | public |
| Property | `IsProcessed` | `bool` | public |
| Property | `IsDeleted` | `bool` | public |
| Property | `IsAlive` | `bool` | public |
| Property | `Uploads` | `IEnumerable<UploadSessionFile>` | public |
| Property | `NextBatch` | `IReadOnlyCollection<IFileUpload>` | public |
| Property | `UploadedBatches` | `int` | public |
| Method/ctor | `UploadFiles` | `Task UploadFiles(IEnumerable<IFileUpload> files) =>` | public |
| Method/ctor | `UploadFiles` | `Task UploadFiles(params IFileUpload[] files) =>` | public |
| Method/ctor | `UploadFiles` | `Task UploadFiles(IAsyncEnumerable<IFileUpload> files);` | public |
| Method/ctor | `UploadFile` | `Task UploadFile(IFileUpload file);` | public |
| Method/ctor | `UploadFile` | `Task UploadFile(byte[] data, string fileName);` | public |
| Method/ctor | `UploadFile` | `Task UploadFile(string path, bool buffer = false);` | public |
| Method/ctor | `UploadFile` | `Task UploadFile(Stream stream, string fileName, bool buffer = true, bool leaveOpen = false);` | public |
| Method/ctor | `UploadFiles` | `Task UploadFiles(bool buffer, params string[] paths);` | public |
| Method/ctor | `DeleteUpload` | `Task DeleteUpload(params string[] ids);` | public |
| Method/ctor | `DeleteUpload` | `Task DeleteUpload(params UploadSessionFile[] files) =>` | public |
| Method/ctor | `Abandon` | `Task Abandon();` | public |
| Method/ctor | `Commit` | `Task<Chapter> Commit(ChapterDraft data, string[]? pageOrder = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




