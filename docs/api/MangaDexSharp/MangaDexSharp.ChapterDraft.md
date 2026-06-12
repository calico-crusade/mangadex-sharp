# MangaDexSharp.ChapterDraft

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Upload/UploadSessionCommit.cs:33](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/UploadSessionCommit.cs#L33)

## Purpose

Represents a draft of the chapter

This page exists so references to [ChapterDraft](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Upload/UploadSessionCommit.cs#L33) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Volume` | `string?` | public |
| Property | `Chapter` | `string?` | public |
| Property | `Title` | `string?` | public |
| Property | `TranslatedLanguage` | `string` | public |
| Property | `ExternalUrl` | `string?` | public |
| Property | `PublishAt` | `DateTime?` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




