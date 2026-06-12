# MangaDexSharp.Utilities.Cli.Verbs.DefaultVerb

- **Kind:** `class`
- **Access:** `internal`
- **Assembly/project:** [MangaDexSharp.Utilities.Cli](../../packages/../apps/utilities-cli.md)
- **Source:** [src/MangaDexSharp.Utilities.Cli/Verbs/DefaultVerb.cs:14](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Verbs/DefaultVerb.cs#L14)

## Purpose

$typeName is part of the $typeProject surface.

This page exists so references to [DefaultVerb](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities.Cli/Verbs/DefaultVerb.cs#L14) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `GetPropertyValue` | `public object? GetPropertyValue(VerbOption option) {` | public |
| Method/ctor | `GetText` | `object? GetText(bool allowEmpty, int? index = null) {` | public |
| Method/ctor | `GetNumber` | `object? GetNumber(bool allowEmpty, int? index = null, Type? type = null) {` | public |
| Method/ctor | `GetBool` | `object? GetBool(bool allowEmpty) {` | public |
| Method/ctor | `GetArray` | `object? GetArray(Func<bool, int?, object?> fetcher, Type type) {` | public |
| Method/ctor | `typeof` | `var generic = typeof(List<>);` | public |
| Method/ctor | `Exception` | `?? throw new Exception($"Could not find Add method for type {arrayType.Name}");` | public |
| Method/ctor | `fetcher` | `var item = fetcher(true, index);` | public |
| Method/ctor | `GetText` | `return GetText(false);` | public |
| Method/ctor | `GetNumber` | `return GetNumber(false);` | public |
| Method/ctor | `GetBool` | `return GetBool(false);` | public |
| Method/ctor | `typeof` | `var generic = typeof(IEnumerable<>);` | public |
| Method/ctor | `GetArray` | `return GetArray((b, i) =>` | public |
| Method/ctor | `RunVerb` | `public async Task<bool> RunVerb(VerbDisplay verb, CancellationToken token) {` | public |
| Method/ctor | `GetPropertyValue` | `var value = GetPropertyValue(option);` | public |
| Method/ctor | `Verbs` | `public IEnumerable<VerbDisplay> Verbs() {` | public |
| Method/ctor | `GetOptions` | `IEnumerable<VerbOption> GetOptions(CommandLineBuilder.CommandVerb verb) {` | public |
| Method/ctor | `VerbOption` | `yield return new VerbOption(attribute, prop);` | public |
| Method/ctor | `VerbDisplay` | `yield return new VerbDisplay(verb, attribute, properties);` | public |
| Method/ctor | `Execute` | `public override async Task<bool> Execute(DefaultOptions _, CancellationToken token) {` | public |
| Method/ctor | `async` | `["logout - Clear any existing/cached access tokens"] = async (_) =>` | public |
| Method/ctor | `RunVerb` | `var action = (CancellationToken token) => RunVerb(t, token);` | public |
| Method/ctor | `return` | `return (display, action);` | public |
| Method/ctor | `action` | `var result = await action(token);` | public |
| Method/ctor | `VerbDisplay` | `public record class VerbDisplay( CommandLineBuilder.CommandVerb Verb, VerbAttribute Attribute, VerbOption[] Properties) {` | public |
| Method/ctor | `Display` | `public string Display() {` | public |
| Method/ctor | `VerbOption` | `public record class VerbOption( OptionAttribute Attribute, PropertyInfo Property) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




