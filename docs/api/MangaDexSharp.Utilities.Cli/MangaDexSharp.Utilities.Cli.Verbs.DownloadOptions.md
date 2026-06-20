# MangaDexSharp.Utilities.Cli.Verbs.DownloadOptions

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities.Cli](../../packages/../apps/utilities-cli.md)
- **Source:** [src/MangaDexSharp.Utilities.Cli/Verbs/DownloadVerb.cs:8](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Verbs/DownloadVerb.cs#L8)

## Purpose

This type is part of the documented API surface.

This page exists so references to [DownloadOptions](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Verbs/DownloadVerb.cs#L8) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `MangaId` | `string?` | public |
| Property | `ChapterIds` | `IEnumerable<string>` | public |
| Property | `CacheDirectory` | `string?` | public |
| Property | `PurgeCache` | `bool` | public |
| Property | `Directory` | `string?` | public |
| Property | `Output` | `string` | public |
| Property | `Grouping` | `string` | public |
| Property | `RateLimitTimeout` | `double` | public |
| Property | `RateLimitAfter` | `int` | public |
| Property | `FormatImageNames` | `bool` | public |
| Property | `Language` | `string?` | public |
| Property | `PreferredGroupIds` | `IEnumerable<string>` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




