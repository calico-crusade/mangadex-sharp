# MangaDexSharp.MangaDexParser

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/MangaDexParser.cs:19](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexParser.cs#L19)

## Purpose

Utility for parsing relationship and attribute types from MD return results. This is to support the varying types returned in the `relationships` and `attributes` result sets.

This page exists so references to [MangaDexParser](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/MangaDexParser.cs#L19) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `IgnoreMapErrors` | `static bool` | public |
| Property | `Type` | `string` | public |
| Method/ctor | `Read` | `public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {` | public |
| Method/ctor | `GetTypeMap` | `var actualMap = GetTypeMap();` | public |
| Method/ctor | `JsonException` | `throw new JsonException("Type is not present in types list");` | public |
| Method/ctor | `JsonException` | `throw new JsonException("Type is not present in type map");` | public |
| Method/ctor | `Write` | `public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) {` | public |
| Method/ctor | `JsonException` | `var actualMap = GetTypeMap() ?? throw new JsonException("Type is not present in types list");` | public |
| Method/ctor | `JsonException` | `if (!map.ContainsKey(value.Type)) throw new JsonException("Type is not present in type map");` | public |
| Method/ctor | `Types` | `public TypeMap[] Types() {` | public |
| Method/ctor | `GetTypeMap` | `public TypeMap? GetTypeMap() {` | public |
| Method/ctor | `Types` | `var types = Types();` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




