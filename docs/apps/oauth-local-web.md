# MangaDexSharp.OAuthLocal.Web

`MangaDexSharp.OAuthLocal.Web` is a local ASP.NET Core sample app that demonstrates MangaDex OAuth/OpenID Connect integration and passing the access token into [`MangaDexSharp`](../packages/mangadexsharp.md).

## Main Flow

- [`Program.cs`](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp.OAuthLocal.Web/Program.cs) configures cookie auth and OpenID Connect against the MangaDex dev auth realm.
- [`WebCredentialService`](../api/MangaDexSharp.OAuthLocal.Web/MangaDexSharp.OAuthLocal.Web.WebCredentialService.md) implements [`ICredentialsService`](../api/MangaDexSharp/MangaDexSharp.ICredentialsService.md) by reading the `access_token` cookie.
- [`MangaController`](../api/MangaDexSharp.OAuthLocal.Web/MangaDexSharp.OAuthLocal.Web.Controllers.MangaController.md) shows authenticated and unauthenticated API calls through [`IMangaDex`](../api/MangaDexSharp/MangaDexSharp.IMangaDex.md).
- [`HomeController`](../api/MangaDexSharp.OAuthLocal.Web/MangaDexSharp.OAuthLocal.Web.Controllers.HomeController.md) displays the token and handles logout.

## Run

```powershell
dotnet run --project src/MangaDexSharp.OAuthLocal.Web/MangaDexSharp.OAuthLocal.Web.csproj
```

A successful auth flow redirects back to the app, writes an `access_token` cookie, and lets `/api/me` call `api.User.Me()` through [`WebCredentialService`](../api/MangaDexSharp.OAuthLocal.Web/MangaDexSharp.OAuthLocal.Web.WebCredentialService.md).
