# MangaDexSharp.UpdatesPoll.UpdatesPollService

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.UpdatesPoll](../../packages/mangadexsharp-updatespoll.md)
- **Source:** [src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs:18](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs#L18)

## Purpose

This type is part of the documented API surface.

This page exists so references to [UpdatesPollService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs#L18) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

```csharp
var service = provider.GetRequiredService<IUpdatesPollService>();
await service.Poll(chapters => Task.CompletedTask, langs: ["en"]);
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `UpdatesPollService` | `public UpdatesPollService( IMangaDex api, ILogger<UpdatesPollService> logger) {` | public |
| Method/ctor | `Latest` | `public IAsyncEnumerable<Chapter> Latest(DateTime since, string[] langs) {` | public |
| Method/ctor | `new` | `Order = new() {` | public |
| Method/ctor | `PollForUpdatesWithPages` | `public async IAsyncEnumerable<ChapterPages> PollForUpdatesWithPages(DateTime since, string[] langs) {` | public |
| Method/ctor | `Latest` | `var chapters = Latest(since, langs);` | public |
| Method/ctor | `new` | `yield return new(chap, [], []);` | public |
| Method/ctor | `new` | `yield return new(chap, pages.Images, pages.DataSaverImages);` | public |
| Method/ctor | `PollForUpdatesWithoutPages` | `public async Task<ChapterPages[]> PollForUpdatesWithoutPages(DateTime since, string[] langs) {` | public |
| Method/ctor | `Poll` | `public async Task Poll(Func<ChapterPages[], Task> callback, DateTime? since = null, string[]? langs = null, bool includePages = true, CancellationToken? token = null) {` | public |
| Method/ctor | `PollForUpdatesWithoutPages` | `await PollForUpdatesWithoutPages(since.Value, langs);` | public |
| Method/ctor | `callback` | `await callback(res);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




