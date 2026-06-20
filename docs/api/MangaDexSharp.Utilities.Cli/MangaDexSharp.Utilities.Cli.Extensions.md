# MangaDexSharp.Utilities.Cli.Extensions

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities.Cli](../../packages/../apps/utilities-cli.md)
- **Source:** [src/MangaDexSharp.Utilities.Cli/Extensions.cs:4](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Extensions.cs#L4)

## Purpose

This type is part of the documented API surface.

This page exists so references to [Extensions](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Extensions.cs#L4) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Trim` | `public static string Trim(this string text, int maxLength, string replacer = "...") {` | public |
| Method/ctor | `Escape` | `public static string Escape(this string text, int buffer = 5) {` | public |
| Method/ctor | `NullReferenceException` | `if (items is null \|\| !items.Any()) throw new NullReferenceException("Items cannot be null or empty.");` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




