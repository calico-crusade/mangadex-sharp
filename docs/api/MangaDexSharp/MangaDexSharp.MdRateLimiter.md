# MangaDexSharp.MdRateLimiter

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/ApiLayer/MdRateLimiter.cs:11](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/MdRateLimiter.cs#L11)

## Purpose

The default rate limiter that

This page exists so references to [MdRateLimiter](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/MdRateLimiter.cs#L11) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `new` | `private readonly TooManyRequestsPause _globalPause = new();` | public |
| Method/ctor | `new` | `private readonly SemaphoreSlim _lock = new(1, 1);` | public |
| Method/ctor | `GenerateCustomLimits` | `public virtual Limit[] GenerateCustomLimits() {` | public |
| Method/ctor | `GenerateGlobalLimit` | `public virtual RateLimiter GenerateGlobalLimit() {` | public |
| Method/ctor | `TokenBucketRateLimiter` | `return new TokenBucketRateLimiter(new() {` | public |
| Method/ctor | `LazyLoadLimits` | `public async Task<(Limit[], RateLimiter)> LazyLoadLimits(CancellationToken token) {` | public |
| Method/ctor | `return` | `if (_customLimits is not null && _globalLimits is not null) return (_customLimits, _globalLimits);` | public |
| Method/ctor | `GenerateCustomLimits` | `_customLimits = GenerateCustomLimits();` | public |
| Method/ctor | `GenerateGlobalLimit` | `_globalLimits = GenerateGlobalLimit();` | public |
| Method/ctor | `return` | `return (_customLimits, _globalLimits);` | public |
| Method/ctor | `GenerateLimiter` | `public virtual RateLimiter GenerateLimiter(int requests, TimeSpan period) {` | public |
| Method/ctor | `Limiter` | `public virtual Limit Limiter(string? method, string? path, RateLimiter limiter) {` | public |
| Method/ctor | `IsApi` | `bool IsApi(MdHttpBuilder builder, out string path) {` | public |
| Method/ctor | `IsPath` | `bool IsPath(string check) {` | public |
| Method/ctor | `IsMethod` | `bool IsMethod(MdHttpBuilder builder) {` | public |
| Method/ctor | `Limiter` | `public virtual Limit Limiter(string? method, string? path, int requests, TimeSpan period) {` | public |
| Method/ctor | `Limiter` | `public virtual Limit Limiter(string? method, string? path, int requests, double minutes) {` | public |
| Method/ctor | `GetLimiter` | `public virtual async Task<(RateLimiter? custom, RateLimiter global)> GetLimiter(MdHttpBuilder request, CancellationToken token) {` | public |
| Method/ctor | `LazyLoadLimits` | `var (Limits, global) = await LazyLoadLimits(token);` | public |
| Method/ctor | `return` | `if (limit.Matches(request)) return (limit.Limiter, global);` | public |
| Method/ctor | `return` | `} return (null, global);` | public |
| Method/ctor | `Limit` | `public async Task<IDisposable> Limit(MdHttpBuilder request, CancellationToken token) {` | public |
| Method/ctor | `AggregateDisposable` | `return new AggregateDisposable([]);` | public |
| Method/ctor | `GetLimiter` | `var (limiter, global) = await GetLimiter(request, token);` | public |
| Method/ctor | `AggregateDisposable` | `return new AggregateDisposable(disposables);` | public |
| Method/ctor | `Observe` | `public void Observe(string url, RateLimit limits) {` | public |
| Method/ctor | `Dispose` | `public void Dispose() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




