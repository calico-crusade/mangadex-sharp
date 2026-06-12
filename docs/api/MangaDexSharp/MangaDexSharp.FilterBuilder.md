# MangaDexSharp.FilterBuilder

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/FilterBuilder.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/FilterBuilder.cs#L6)

## Purpose

A utility for building query parameters in MD's style

This page exists so references to [FilterBuilder](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/FilterBuilder.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Add` | `public FilterBuilder Add(string key, string? value) {` | public |
| Method/ctor | `Add` | `public FilterBuilder Add(string key, string[] items) {` | public |
| Method/ctor | `Add` | `public FilterBuilder Add(string key, DateTime? date) {` | public |
| Method/ctor | `Add` | `public FilterBuilder Add(string key, int? value) {` | public |
| Method/ctor | `Add` | `public FilterBuilder Add(string key, int value) {` | public |
| Method/ctor | `Add` | `public FilterBuilder Add(string key, bool? value) {` | public |
| Method/ctor | `Add` | `public FilterBuilder Add(string key, bool value) {` | public |
| Method/ctor | `Build` | `public string Build() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




