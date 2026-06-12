# MangaDexSharp.UpdatesPoll.IUpdatesPollService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.UpdatesPoll](../../packages/mangadexsharp-updatespoll.md)
- **Source:** [src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs:4](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs#L4)

## Purpose

$typeName is part of the $typeProject surface.

This page exists so references to [IUpdatesPollService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs#L4) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

```csharp
var service = provider.GetRequiredService<IUpdatesPollService>();
await service.Poll(chapters => Task.CompletedTask, langs: ["en"]);
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Poll` | `Task Poll(Func<ChapterPages[], Task> callback, DateTime? since = null, string[]? langs = null, bool includePages = true, CancellationToken? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




