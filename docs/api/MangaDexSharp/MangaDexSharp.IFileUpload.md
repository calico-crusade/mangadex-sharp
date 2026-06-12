# MangaDexSharp.IFileUpload

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Upload/FileUpload.cs:9](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/FileUpload.cs#L9)

## Purpose

Represents a file that is to be uploaded to the chapter

This page exists so references to [IFileUpload](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/FileUpload.cs#L9) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Property | `FileName` | `string` | public |
| Method/ctor | `Content` | `Task<byte[]> Content(CancellationToken token);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




