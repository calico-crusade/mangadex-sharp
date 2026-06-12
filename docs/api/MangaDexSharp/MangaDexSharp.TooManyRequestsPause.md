# MangaDexSharp.TooManyRequestsPause

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/ApiLayer/TooManyRequestsPause.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/TooManyRequestsPause.cs#L6)

## Purpose

A helper for pausing the API when too many requests are made

This page exists so references to [TooManyRequestsPause](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/TooManyRequestsPause.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `ResumeAt` | `DateTimeOffset` | public |
| Method/ctor | `new` | `private readonly Lock _lock = new();` | public |
| Method/ctor | `new` | `private readonly object _lock = new();` | public |
| Method/ctor | `WaitAsync` | `public Task WaitAsync(CancellationToken token) {` | public |
| Method/ctor | `lock` | `Task pauseTask; lock(_lock) {` | public |
| Method/ctor | `Wait` | `return Wait(pauseTask, token);` | public |
| Method/ctor | `Pause` | `public void Pause(DateTimeOffset until) {` | public |
| Method/ctor | `lock` | `lock(_lock) {` | public |
| Method/ctor | `Pause` | `public void Pause(TimeSpan duration) {` | public |
| Method/ctor | `Pause` | `return; Pause(DateTimeOffset.UtcNow + duration);` | public |
| Method/ctor | `OperationCanceledException` | `throw new OperationCanceledException(token);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




