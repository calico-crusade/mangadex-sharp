# MangaDexSharp.OAuthLocal.Web.Controllers.MangaController

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.OAuthLocal.Web](../../packages/../apps/oauth-local-web.md)
- **Source:** [src/MangaDexSharp.OAuthLocal.Web/Controllers/MangaController.cs:7](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.OAuthLocal.Web/Controllers/MangaController.cs#L7)

## Purpose

$typeName is part of the $typeProject surface.

This page exists so references to [MangaController](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.OAuthLocal.Web/Controllers/MangaController.cs#L7) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `MangaController` | `public MangaController(IMangaDex md) {` | public |
| Method/ctor | `Get` | `public async Task<IActionResult> Get([FromRoute] string id) {` | public |
| Method/ctor | `Ok` | `return Ok(manga);` | public |
| Method/ctor | `Me` | `public async Task<IActionResult> Me() {` | public |
| Method/ctor | `Ok` | `return Ok(me);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




