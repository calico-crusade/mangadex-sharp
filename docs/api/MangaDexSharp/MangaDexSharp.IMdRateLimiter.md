# MangaDexSharp.IMdRateLimiter

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/ApiLayer/IMdRateLimiter.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/IMdRateLimiter.cs#L6)

## Purpose

An instance of a rate limiter for the MD API.

This page exists so references to [IMdRateLimiter](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/IMdRateLimiter.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Limit` | `Task<IDisposable> Limit(MdHttpBuilder request, CancellationToken token);` | public |
| Method/ctor | `Observe` | `void Observe(string url, RateLimit limits);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




