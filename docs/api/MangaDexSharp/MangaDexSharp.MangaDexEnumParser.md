# MangaDexSharp.MangaDexEnumParser

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/MangaDexEnumParser.cs:7](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexEnumParser.cs#L7)

## Purpose

Utility for parsing enums with MD return results

This page exists so references to [MangaDexEnumParser](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexEnumParser.cs#L7) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `Read` | `public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {` | public |
| Method/ctor | `VerifyType` | `VerifyType(typeToConvert);` | public |
| Method/ctor | `Write` | `public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) {` | public |
| Method/ctor | `VerifyType` | `public void VerifyType(Type type) {` | public |
| Method/ctor | `ArgumentException` | `if (!type.IsEnum) throw new ArgumentException("Type must be an enum");` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




