# MangaDexSharp.RateLimit

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Base/MangaDexRateLimits.cs:19](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Base/MangaDexRateLimits.cs#L19)

## Purpose

Represents the rate limit headers from the response

This page exists so references to [RateLimit](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Base/MangaDexRateLimits.cs#L19) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Limit` | `[JsonIgnore] public int?` | public |
| Property | `Remaining` | `[JsonIgnore] public int?` | public |
| Property | `RetryAfter` | `[JsonIgnore] public DateTime?` | public |
| Method/ctor | `RetryPassed` | `public bool RetryPassed() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




