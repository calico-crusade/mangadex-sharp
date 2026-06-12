# MangaDexSharp.UpdatesPoll.ChapterPages

- **Kind:** `record class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.UpdatesPoll](../../packages/mangadexsharp-updatespoll.md)
- **Source:** [src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs:169](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs#L169)

## Purpose

Represents a chapter and it's associated page URLs

This page exists so references to [ChapterPages](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.UpdatesPoll/UpdatesPollService.cs#L169) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

```csharp
var service = provider.GetRequiredService<IUpdatesPollService>();
await service.Poll(chapters => Task.CompletedTask, langs: ["en"]);
```

## Members

No public members were detected in this type body.

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




