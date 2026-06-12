# MangaDexSharp.DiExtensions

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/DiExtensions.cs:10](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/DiExtensions.cs#L10)

## Purpose

[Extensions](../MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Extensions.md) for dependency injection in MangaDexSharp

This page exists so references to [DiExtensions](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/DiExtensions.cs#L10) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

```csharp
using MangaDexSharp;
using Microsoft.Extensions.DependencyInjection;

var provider = new ServiceCollection()
    .AddMangaDex(c => c.AddMangaDexUtils())
    .BuildServiceProvider();
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `AddMangaDexUtils` | `public static IServiceCollection AddMangaDexUtils(this IServiceCollection services) {` | public |
| Method/ctor | `AddMangaDexUtils` | `public static IMangaDexBuilder AddMangaDexUtils(this IMangaDexBuilder builder) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




