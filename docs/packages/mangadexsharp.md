# MangaDexSharp Package

`MangaDexSharp` is the core NuGet package for calling the MangaDex API from .NET. It provides a typed `IMangaDex` root client, endpoint-specific services, request filters, response models, OAuth helpers, and dependency injection registration.

The client is manually written around MangaDex's OpenAPI description, so it keeps idiomatic C# service names while staying close to the API routes.

## Install

```powershell
PM> Install-Package MangaDexSharp
```

## Entry Points

Create a client directly:

```csharp
using MangaDexSharp;

var api = MangaDex.Create(c => c
    .WithApiConfig(a => a.WithUserAgent("my-app/1.0")));
```

Register with dependency injection:

```csharp
using MangaDexSharp;

builder.Services.AddMangaDex(c => c
    .WithApiConfig(a => a
        .WithUserAgent("my-app/1.0")
        .WithAutoRateLimits(enabled: true, conservative: true))
    .WithAccessTokenFromConfig());
```

Inject the root client:

```csharp
public class MangaLookup(IMangaDex api)
{
    public Task<MangaDexRoot<Manga>> Get(string mangaId)
    {
        return api.Manga.Get(mangaId);
    }
}
```

## Configuration

`ConfigurationApi` controls the MangaDex API URL, User-Agent, error behavior, and rate-limit handling.

| Setting | Default key | Purpose |
| --- | --- | --- |
| `ApiUrl` | `Mangadex:ApiUrl` | Base API URL. Defaults to `https://api.mangadex.org`. |
| `UserAgent` | `Mangadex:UserAgent` | User-Agent sent with API requests. Use your own application name. |
| `ThrowOnError` | `Mangadex:ThrowOnError` | Throws when MangaDex returns an API error. |
| `HandleRateLimits` | `Mangadex:RateLimits:Enabled` | Enables automatic rate-limit handling. |
| `ConservativeLimits` | `Mangadex:RateLimits:Conservative` | Uses one fewer request per rate-limit window. |

`ConfigurationOIDC` controls OAuth/OIDC token requests.

| Setting | Default key | Purpose |
| --- | --- | --- |
| `AuthUrl` | `Mangadex:AuthUrl` | Auth server root. |
| `RealmPath` | `Mangadex:RealmPath` | Token endpoint realm path. |
| `ClientId` | `Mangadex:ClientId` | MangaDex API client ID. |
| `ClientSecret` | `Mangadex:ClientSecret` | MangaDex API client secret. |
| `Username` | `Mangadex:Username` | Account username for personal-client token requests. |
| `Password` | `Mangadex:Password` | Account password for personal-client token requests. |

Example `appsettings.json`:

```json
{
  "Mangadex": {
    "ApiUrl": "https://api.mangadex.org",
    "UserAgent": "my-app/1.0",
    "ThrowOnError": "false",
    "RateLimits": {
      "Enabled": "true",
      "Conservative": "true"
    },
    "Token": "<access-token>",
    "ClientId": "<client-id>",
    "ClientSecret": "<client-secret>",
    "Username": "<username>",
    "Password": "<password>"
  }
}
```

## Authentication

Most public MangaDex routes can be called without authentication. Account routes require a bearer token.

```csharp
var api = MangaDex.Create();

var token = await api.Auth.Personal(
    id: "<client-id>",
    secret: "<client-secret>",
    username: "<username>",
    password: "<password>");

var authed = MangaDex.Create(c => c.WithAccessToken(token.AccessToken));
var me = await authed.User.Me();
```

Check or logout a token:

```csharp
var check = await api.Auth.Check(token.AccessToken);

if (check.IsAuthenticated)
    Console.WriteLine(string.Join(", ", check.Roles));

await api.Auth.Logout(token.AccessToken);
```

Authenticated methods also accept an optional `token` argument:

```csharp
await api.Manga.Follow("fc0a7b86-992e-4126-b30f-ca04811979bf", token.AccessToken);
```

For custom token resolution, implement `ICredentialsService` and register it with `WithCredentials<T>()`.

## Service Areas

`IMangaDex` exposes these endpoint groups:

| Property | Purpose |
| --- | --- |
| `Manga` | Manga search, CRUD, feeds, random manga, tags, reading status, drafts, relations, recommendations. |
| `Chapter` | Chapter search, get, update, and delete. |
| `Author` | Author search, get, create, update, and delete. |
| `Cover` | Cover list, multipart upload, get, update, and delete. |
| `Lists` | Custom list CRUD, follow/unfollow, and manga membership management. |
| `Feed` | Followed manga feed and custom-list feed. |
| `Follows` | Followed groups, users, manga, and custom lists. |
| `ReadMarker` | Read marker fetch and batch update. |
| `Report` | Report reasons, reports, and report creation. |
| `ScanlationGroup` | Scanlation group search, CRUD, follow, and unfollow. |
| `Upload` | Upload sessions, files, commits, cleanup, and approval checks. |
| `User` | User search, profile, current user, delete flow, history, and obsolete legacy login helpers. |
| `Auth` | OAuth token helpers plus MangaDex API check/logout routes. |
| `ApiClient` | API client CRUD and secret management. |
| `Statistics` | Manga, chapter, and group statistics. |
| `Settings` | Settings templates and current user settings. |
| `Infrastructure` | API healthcheck. |
| `Legacy` | Legacy integer ID mapping. |
| `Pages` | MangaDex@Home page server lookup. |
| `Ratings`, `Threads`, `Captcha`, `Takedown` | Smaller API groups surfaced as convenience properties. |

## Examples

### Search Manga

```csharp
var results = await api.Manga.List(new MangaFilter
{
    Title = "Frieren",
    ContentRating = [ContentRating.safe, ContentRating.suggestive],
    Includes = [MangaIncludes.cover_art, MangaIncludes.author],
    Order = new()
    {
        [MangaFilter.OrderKey.relevance] = OrderValue.desc
    }
});
```

### Fetch Chapters

```csharp
var chapters = await api.Chapter.List(new ChaptersFilter
{
    Manga = "fc0a7b86-992e-4126-b30f-ca04811979bf",
    TranslatedLanguage = ["en"],
    IncludeExternalUrl = false,
    ExternalUrl = null,
    ExcludeExternalUrl = "example.com",
    Order = new()
    {
        [ChaptersFilter.OrderKey.publishAt] = OrderValue.desc
    }
});
```

### Manga Recommendations

```csharp
var recommendations = await api.Manga.Recommendations(
    "fc0a7b86-992e-4126-b30f-ca04811979bf",
    new MangaRecommendationFilter
    {
        ContentRating = [ContentRating.safe],
        Order = new()
        {
            [MangaRecommendationFilter.OrderKey.score] = OrderValue.desc
        }
    });
```

### Upload Cover Art

```csharp
using var file = new PathFileUpload("cover.jpg");

var result = await api.Cover.Upload("fc0a7b86-992e-4126-b30f-ca04811979bf", new CoverArtCreate
{
    File = file,
    Volume = "1",
    Description = "Volume 1 cover",
    Locale = "en",
    ContentType = "image/jpeg"
}, "<access-token>");
```

### Check Upload Approval

```csharp
var approval = await api.Upload.CheckApprovalRequired(
    manga: "fc0a7b86-992e-4126-b30f-ca04811979bf",
    locale: "en",
    token: "<access-token>");
```

### Read History

```csharp
var history = await api.User.History("<access-token>");

foreach (var item in history.Ratings)
    Console.WriteLine($"{item.ChapterId}: {item.ReadDate}");
```

### Settings

```csharp
using System.Text.Json;

var settings = await api.Settings.Get("<access-token>");
var latestTemplate = await api.Settings.LatestTemplate("<access-token>");

using var doc = JsonDocument.Parse("""{"theme":"dark"}""");
await api.Settings.Update(new UserSettingsUpdate
{
    Settings = doc.RootElement.Clone(),
    UpdatedAt = DateTime.UtcNow
}, "<access-token>");
```

### Legacy ID Mapping

```csharp
var mappings = await api.Legacy.LegacyMapping(LegacyMappingType.manga, 1, 2, 3);

foreach (var mapping in mappings.Data)
    Console.WriteLine($"{mapping.Attributes?.LegacyId} -> {mapping.Attributes?.NewId}");
```

### Healthcheck

```csharp
var ping = await api.Infrastructure.Ping();
Console.WriteLine(ping.Result);
```

## Models And Filters

Filters such as `MangaFilter`, `MangaFeedFilter`, `ChaptersFilter`, `CoverArtFilter`, `AuthorFilter`, and `ScanlationGroupFilter` build MangaDex-style query strings. Create/update models are plain C# objects that serialize to request bodies.

Response models inherit from `MangaDexRoot`, `MangaDexRoot<T>`, or `MangaDexCollection<T>`. Always check `Result` or use the helper extension methods before assuming a response succeeded.

```csharp
var response = await api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");

if (response.IsError(out var error, out var manga))
    Console.WriteLine(error);
else
    Console.WriteLine(manga.Attributes?.Title["en"]);
```

## Reference

The generated reference pages live under [API Reference](../api/README.md). Some hand-written docs may lead newly added APIs until the generated reference is refreshed.
