# MangaDexSharp.MdEventService

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/IMdEventService.cs:88](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/IMdEventService.cs#L88)

## Purpose

Default implementation of [IMdEventService](../MangaDexSharp/MangaDexSharp.IMdEventService.md) that does nothing and provides easy overrides for the events you want

This page exists so references to [MdEventService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/IMdEventService.cs#L88) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `OnRequestError` | `public virtual void OnRequestError(string url, Exception error) {` | public |
| Method/ctor | `OnRequestFinished` | `public virtual void OnRequestFinished(string url, Exception? error) {` | public |
| Method/ctor | `OnRequestStarting` | `public virtual void OnRequestStarting(string url) {` | public |
| Method/ctor | `OnResponseReceived` | `public virtual void OnResponseReceived(string url, HttpResponseMessage response, HttpRequestMessage request) {` | public |
| Method/ctor | `OnResponseParsed` | `public virtual void OnResponseParsed(string url, HttpResponseMessage response, object? data) {` | public |
| Method/ctor | `OnRateLimitDataReceived` | `public virtual void OnRateLimitDataReceived(string url, RateLimit limits) {` | public |
| Method/ctor | `OnRateLimitExceeded` | `public virtual void OnRateLimitExceeded(string url, RateLimit limits) {` | public |
| Method/ctor | `OnRateLimitGlobalPaused` | `public virtual void OnRateLimitGlobalPaused(string url, RateLimit limits, TimeSpan span) {` | public |
| Method/ctor | `OnRateLimitGlobalUnpaused` | `public virtual void OnRateLimitGlobalUnpaused(string url, RateLimit limits) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




