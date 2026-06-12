# MangaDexSharp.IMdEventServiceBase

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/IMdEventService.cs:8](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/IMdEventService.cs#L8)

## Purpose

The base interface for [IMdEventService](../MangaDexSharp/MangaDexSharp.IMdEventService.md)

This page exists so references to [IMdEventServiceBase](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/IMdEventService.cs#L8) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `OnRequestFinished` | `void OnRequestFinished(string url, Exception? error);` | public |
| Method/ctor | `OnRequestError` | `void OnRequestError(string url, Exception error);` | public |
| Method/ctor | `OnRequestStarting` | `void OnRequestStarting(string url);` | public |
| Method/ctor | `OnResponseReceived` | `void OnResponseReceived(string url, HttpResponseMessage response, HttpRequestMessage request);` | public |
| Method/ctor | `OnResponseParsed` | `void OnResponseParsed(string url, HttpResponseMessage response, object? data);` | public |
| Method/ctor | `OnRateLimitDataReceived` | `void OnRateLimitDataReceived(string url, RateLimit limits);` | public |
| Method/ctor | `OnRateLimitExceeded` | `void OnRateLimitExceeded(string url, RateLimit limits);` | public |
| Method/ctor | `OnRateLimitGlobalPaused` | `void OnRateLimitGlobalPaused(string url, RateLimit limits, TimeSpan span);` | public |
| Method/ctor | `OnRateLimitGlobalUnpaused` | `void OnRateLimitGlobalUnpaused(string url, RateLimit limits);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




