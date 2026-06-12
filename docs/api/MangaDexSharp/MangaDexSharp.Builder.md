# MangaDexSharp.Builder

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/MangaDexParser.cs:177](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexParser.cs#L177)

## Purpose

A builder for making a [TypeMap](../MangaDexSharp/MangaDexSharp.TypeMap.md)

This page exists so references to [Builder](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexParser.cs#L177) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `Map` | `Dictionary<string, Type>` | public |
| Method/ctor | `typeof` | `Map[name] = typeof(TType);` | public |
| Method/ctor | `Build` | `public TypeMap Build() {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




