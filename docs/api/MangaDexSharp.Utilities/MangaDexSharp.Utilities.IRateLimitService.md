# MangaDexSharp.Utilities.IRateLimitService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.Utilities](../../packages/mangadexsharp-utilities.md)
- **Source:** [src/MangaDexSharp.Utilities/RateLimitService.cs:8](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/RateLimitService.cs#L8)

## Purpose

Service for handling rate-limits for the MD API

This page exists so references to [IRateLimitService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.Utilities/RateLimitService.cs#L8) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Register `AddMangaDexUtils()` and inject the interface from dependency injection.

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
| Method/ctor | `new` | `where T : MangaDexRoot, new();` | public |
| Method/ctor | `new` | `where TFilter : IPaginateFilter, new();` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




