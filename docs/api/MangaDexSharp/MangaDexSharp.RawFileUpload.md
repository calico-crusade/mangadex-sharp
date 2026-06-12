# MangaDexSharp.RawFileUpload

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Upload/FileUpload.cs:29](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/FileUpload.cs#L29)

## Purpose

Represents a files raw byte content

This page exists so references to [RawFileUpload](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/FileUpload.cs#L29) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Data` | `byte[]` | public |
| Method/ctor | `Content` | `public Task<byte[]> Content(CancellationToken _) =>` | public |
| Method/ctor | `Dispose` | `public void Dispose() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




