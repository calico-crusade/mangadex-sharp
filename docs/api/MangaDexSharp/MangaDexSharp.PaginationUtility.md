# MangaDexSharp.PaginationUtility

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/PaginationUtility.cs:89](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/PaginationUtility.cs#L89)

## Purpose

A paginated utility targeted towards [MangaDexCollection](../MangaDexSharp/MangaDexSharp.MangaDexCollection.md){T} types

This page exists so references to [PaginationUtility](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/PaginationUtility.cs#L89) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Filter` | `TFilter` | public |
| Property | `RequestFn` | `Func<TFilter, CancellationToken, Task<TResult>>` | public |
| Method/ctor | `PaginationUtility` | `public PaginationUtility(TFilter filter, Func<TFilter, CancellationToken, Task<TResult>> request) {` | public |
| Method/ctor | `Request` | `public override async Task<(TSource[] result, int limit, int total)> Request(int offset, CancellationToken token) {` | public |
| Method/ctor | `RequestFn` | `var result = await RequestFn(Filter, token);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)






