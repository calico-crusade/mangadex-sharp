# MangaDexSharp.Utilities.Cli.Services.ExportReadListService

- **Kind:** `class`
- **Access:** `internal`
- **Assembly/project:** [MangaDexSharp.Utilities.Cli](../../packages/../apps/utilities-cli.md)
- **Source:** [src/MangaDexSharp.Utilities.Cli/Services/ExportReadListService.cs:12](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Services/ExportReadListService.cs#L12)

## Purpose

This type is part of the documented API surface.

This page exists so references to [ExportReadListService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Services/ExportReadListService.cs#L12) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `GetWriters` | `public static Dictionary<string, Func<string, IRecordWriter<MangaStatus>>> GetWriters(string lang) {` | public |
| Method/ctor | `new` | `return new() {` | public |
| Method/ctor | `GetUsersMangaStatus` | `public async IAsyncEnumerable<MangaStatus> GetUsersMangaStatus(ReadStatus? status, bool includeChapters, string lang, [EnumeratorCancellation] CancellationToken token) {` | public |
| Method/ctor | `PopChapter` | `yield return await PopChapter(m, ms, includeChapters, lang, token);` | public |
| Method/ctor | `PopChapter` | `public async Task<MangaStatus> PopChapter(Manga manga, ReadStatus status, bool chaps, string lang, CancellationToken token) {` | public |
| Method/ctor | `return` | `if (!chaps) return (manga, status, null);` | public |
| Method/ctor | `new` | `Order = new() {` | public |
| Method/ctor | `return` | `var chapter = chapters.Data.FirstOrDefault(); return (manga, status, chapter);` | public |
| Method/ctor | `WriteUsersMangaStatusToFile` | `public async Task WriteUsersMangaStatusToFile(string file, ReadStatus? status, bool includeChapters, string lang, CancellationToken token) {` | public |
| Method/ctor | `fetch` | `using var writer = fetch(file);` | public |
| Method/ctor | `GetUsersMangaStatus` | `var manga = GetUsersMangaStatus(status, includeChapters, lang, token);` | public |
| Method/ctor | `FromManga` | `public static CsvManga FromManga(MangaStatus m, string lang) {` | public |
| Method/ctor | `DetermineChapterTitle` | `string? DetermineChapterTitle() {` | public |
| Method/ctor | `StringBuilder` | `var bob = new StringBuilder();` | public |
| Method/ctor | `DetermineChapterUrl` | `string? DetermineChapterUrl() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




