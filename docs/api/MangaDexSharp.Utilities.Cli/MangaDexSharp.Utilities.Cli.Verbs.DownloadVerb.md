# MangaDexSharp.Utilities.Cli.Verbs.DownloadVerb

- **Kind:** `class`
- **Access:** `internal`
- **Assembly/project:** [MangaDexSharp.Utilities.Cli](../../packages/../apps/utilities-cli.md)
- **Source:** [src/MangaDexSharp.Utilities.Cli/Verbs/DownloadVerb.cs:52](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Verbs/DownloadVerb.cs#L52)

## Purpose

$typeName is part of the $typeProject surface.

This page exists so references to [DownloadVerb](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Verbs/DownloadVerb.cs#L52) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `FilterChapters` | `public static async IAsyncEnumerable<Chapter> FilterChapters( DownloadOptions options, IAsyncEnumerable<Chapter> chapters, [EnumeratorCancellation] CancellationToken token) {` | public |
| Method/ctor | `return` | `: 0.0; return (key, chapter);` | public |
| Method/ctor | `Download` | `public async Task Download(IDownloadInstance instance, DownloadOptions options, CancellationToken token) {` | public |
| Method/ctor | `DownloadMangaChapters` | `async Task DownloadMangaChapters(string mangaId) {` | public |
| Method/ctor | `new` | `Order = new() {` | public |
| Method/ctor | `async` | `var chapters = _rates.Request<Chapter, MangaFeedFilter>( async (api, filter) =>` | public |
| Method/ctor | `FilterChapters` | `var filtered = FilterChapters(options, chapters, token);` | public |
| Method/ctor | `DownloadChapters` | `async IAsyncEnumerable<Chapter> DownloadChapters(IEnumerable<string> chaptersIds, Manga? manga) {` | public |
| Method/ctor | `DownloadMangaChapters` | `await DownloadMangaChapters(options.MangaId);` | public |
| Method/ctor | `DownloadChapters` | `var chapters = DownloadChapters(options.ChapterIds, manga);` | public |
| Method/ctor | `Execute` | `public override async Task<bool> Execute(DownloadOptions options, CancellationToken token) {` | public |
| Method/ctor | `NotSupportedException` | `throw new NotSupportedException("Unsupported archive type: " + archiveType);` | public |
| Method/ctor | `Download` | `await Download(download, options, token);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




