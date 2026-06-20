# MangaDexSharp.IMangaDexCaptchaService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexMiscService.cs:72](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L72)

## Purpose

Represents all of the requests related to captchas

This page exists so references to [IMangaDexCaptchaService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexMiscService.cs#L72) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.Captcha` or inject the service from a provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `Captcha` | `Task<MangaDexRoot> Captcha(string challenge, string? token = null);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




