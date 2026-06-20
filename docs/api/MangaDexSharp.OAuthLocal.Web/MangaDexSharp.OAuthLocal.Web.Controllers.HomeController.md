# MangaDexSharp.OAuthLocal.Web.Controllers.HomeController

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp.OAuthLocal.Web](../../packages/../apps/oauth-local-web.md)
- **Source:** [src/MangaDexSharp.OAuthLocal.Web/Controllers/HomeController.cs:10](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.OAuthLocal.Web/Controllers/HomeController.cs#L10)

## Purpose

This type is part of the documented API surface.

This page exists so references to [HomeController](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.OAuthLocal.Web/Controllers/HomeController.cs#L10) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Method/ctor | `HomeController` | `public HomeController(ILogger<HomeController> logger) {` | public |
| Method/ctor | `Index` | `public IActionResult Index() {` | public |
| Method/ctor | `View` | `return View(model: token);` | public |
| Method/ctor | `Logout` | `public async Task<IActionResult> Logout() {` | public |
| Method/ctor | `RedirectToAction` | `return RedirectToAction("Index");` | public |
| Method/ctor | `Error` | `public IActionResult Error() {` | public |
| Method/ctor | `View` | `return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




