# MangaDexSharp.StreamFileUpload

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Upload/FileUpload.cs:69](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/FileUpload.cs#L69)

## Purpose

Represents a files stream content Warning; using this class can cause issues if request retries are enabled (like in the case of using upload sessions). So it is preferably to use

This page exists so references to [StreamFileUpload](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/FileUpload.cs#L69) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Data` | `Stream` | public |
| Method/ctor | `Content` | `public async Task<byte[]> Content(CancellationToken token) {` | public |
| Method/ctor | `MemoryStream` | `using var ms = new MemoryStream();` | public |
| Method/ctor | `Dispose` | `public void Dispose() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




