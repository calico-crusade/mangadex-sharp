# MangaDexSharp.MangaDexRootExtensions

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Base/MangaDexRootExtensions.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Base/MangaDexRootExtensions.cs#L6)

## Purpose

A few helpful extension methods for [MangaDexRoot](../MangaDexSharp/MangaDexSharp.MangaDexRoot.md) models

This page exists so references to [MangaDexRootExtensions](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Base/MangaDexRootExtensions.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `CompileError` | `public static string CompileError(MangaDexError error) {` | public |
| Method/ctor | `CompileErrors` | `public static string? CompileErrors(params MangaDexError[] errors) {` | public |
| Method/ctor | `IsError` | `public static bool IsError(this MangaDexRoot root) {` | public |
| Method/ctor | `IsError` | `public static bool IsError(this MangaDexRoot root, out string error) {` | public |
| Method/ctor | `new` | `where T: new() {` | public |
| Method/ctor | `ThrowIfError` | `public static void ThrowIfError(this MangaDexRoot result) {` | public |
| Method/ctor | `MangaDexException` | `throw new MangaDexException(result);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




