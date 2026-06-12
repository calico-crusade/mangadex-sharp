# MangaDexSharp.IMangaDexUploadService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexUploadService.cs:10](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexUploadService.cs#L10)

## Purpose

Represents all of the requests related to uploading chapters

This page exists so references to [IMangaDexUploadService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexUploadService.cs#L10) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through IMangaDex (pi.Upload) or inject $typeName from a service provider configured with AddMangaDex().

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Get` | `Task<MangaDexRoot<UploadSession>> Get(UploadIncludes[]? include = null, string? token = null);` | public |
| Method/ctor | `Begin` | `Task<MangaDexRoot<UploadSession>> Begin(string manga, string[] groups, string? token = null);` | public |
| Method/ctor | `EditChapter` | `Task<MangaDexRoot<UploadSession>> EditChapter(string chapterId, int version = 1, UploadIncludes[]? include = null, string? token = null);` | public |
| Method/ctor | `Upload` | `Task<UploadSessionFileList> Upload(string sessionId, params IFileUpload[] files);` | public |
| Method/ctor | `Upload` | `Task<UploadSessionFileList> Upload(string sessionId, string token, params IFileUpload[] files);` | public |
| Method/ctor | `Upload` | `Task<UploadSessionFileList> Upload(string sessionId, string? token, CancellationToken cancel, params IFileUpload[] files);` | public |
| Method/ctor | `Upload` | `Task<UploadSessionFileList> Upload(string sessionId, string? token, string? contentType, CancellationToken cancel, params IFileUpload[] files);` | public |
| Method/ctor | `DeleteUpload` | `Task<MangaDexRoot> DeleteUpload(string sessionId, string fileId, string? token = null);` | public |
| Method/ctor | `DeleteUpload` | `Task<MangaDexRoot> DeleteUpload(string sessionId, string[] fileIds, string? token = null);` | public |
| Method/ctor | `Abandon` | `Task<MangaDexRoot> Abandon(string sessionId, string? token = null);` | public |
| Method/ctor | `Commit` | `Task<MangaDexRoot<Chapter>> Commit(string sessionId, UploadSessionCommit data, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




