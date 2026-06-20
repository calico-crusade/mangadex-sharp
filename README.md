# mangadex-sharp

A .NET client library for the [MangaDex API](https://api.mangadex.org/docs/redoc.html).

The core package is `MangaDexSharp`. The repository also contains optional utilities for downloading chapters, uploading/editing chapters, rate-limited workflows, and small sample/CLI applications.

## Usage Agreement

By using this library, you are responsible for following MangaDex's [Acceptable Usage Policy](https://api.mangadex.org/docs/#acceptable-usage-policy) and [general connection requirements](https://api.mangadex.org/docs/2-limitations/#general-connection-requirements).

Do not spoof another application's User-Agent. Use an honest User-Agent for your application.

By calling `IMangaDexUploadService.Commit(string, UploadSessionCommit, string?)`, you agree to the MangaDex upload terms at <https://mangadex.org/compliance>.

## Packages

| Package | Purpose |
| --- | --- |
| `MangaDexSharp` | Core API client, models, filters, authentication helpers, and dependency injection setup. |
| `MangaDexSharp.Utilities` | Upload, download, and rate-limit helper workflows built on top of the core client. |
| `MangaDexSharp.UpdatesPoll` | Polling helper for recently updated chapters. |
| `MangaDexSharp.Utilities.Cli` | Command-line utilities for auth checks, downloading, and read-list export. |

## Installation

```powershell
PM> Install-Package MangaDexSharp
```

For upload/download helpers:

```powershell
PM> Install-Package MangaDexSharp.Utilities
```

## Quick Start

```csharp
using MangaDexSharp;

var api = MangaDex.Create();

var manga = await api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");
var feed = await api.Manga.Feed(manga.Data.Id, new MangaFeedFilter
{
    TranslatedLanguage = ["en"],
    IncludeExternalUrl = false
});

var chapter = await api.Chapter.Get(feed.Data[0].Id);
var pages = await api.Pages.Pages(chapter.Data.Id);

Console.WriteLine(manga.Data.Attributes?.Title["en"]);
Console.WriteLine($"Chapter pages: {pages.Chapter.Data.Length}");
```

## Dependency Injection

```csharp
using MangaDexSharp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMangaDex(c => c
    .WithApiConfig(a => a
        .WithUserAgent("my-app/1.0")
        .WithAutoRateLimits(enabled: true, conservative: true))
    .WithAccessTokenFromConfig());

var app = builder.Build();
```

Then inject `IMangaDex` or an individual endpoint service:

```csharp
using MangaDexSharp;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class MangaController(IMangaDex mangaDex) : ControllerBase
{
    [HttpGet("manga/{id}")]
    public Task<MangaDexRoot<Manga>> Get(string id)
    {
        return mangaDex.Manga.Get(id);
    }
}
```

## Direct Client Creation

```csharp
using MangaDexSharp;

var api = MangaDex.Create(c => c
    .WithApiConfig(a => a.WithUserAgent("my-script/1.0")));

var random = await api.Manga.Random(new MangaRandomFilter
{
    ContentRating = [ContentRating.safe]
});
```

For authenticated routes:

```csharp
var api = MangaDex.Create(c => c.WithAccessToken("<access-token>"));
var me = await api.User.Me();
```

You can also pass a token to a single authenticated request:

```csharp
var api = MangaDex.Create();
await api.Manga.Follow("fc0a7b86-992e-4126-b30f-ca04811979bf", "<access-token>");
```

## Service Map

The `IMangaDex` root exposes API areas as properties:

| Property | Examples |
| --- | --- |
| `Manga` | Search, get, create, update, feed, random, tags, status, drafts, relations, recommendations. |
| `Chapter` | Search, get, update, delete chapters. |
| `Author` | Search, get, create, update, delete authors. |
| `Cover` | List, upload, get, edit, delete cover art. |
| `Lists` | Create, get, update, delete, follow, and manage manga in custom lists. |
| `Feed` | Followed-manga feed and custom-list feed. |
| `Follows` | Followed groups, users, manga, and custom lists. |
| `ReadMarker` | Read markers and read-marker batch updates. |
| `Report` | Report reasons, reports, and report creation. |
| `ScanlationGroup` | Search, get, create, update, delete, follow, unfollow groups. |
| `Upload` | Upload sessions, files, commits, cleanup, and moderation approval checks. |
| `User` | User search, profile, current user, account deletion, reading history, legacy login helpers. |
| `Auth` | OAuth token helpers plus API token check/logout. |
| `ApiClient` | API client management and secret regeneration. |
| `Statistics` | Manga, chapter, and group statistics. |
| `Settings` | User settings and settings templates. |
| `Infrastructure` | API healthcheck. |
| `Legacy` | Legacy integer ID to UUID mapping. |
| `Pages` | MangaDex@Home page server resolution. |
| `Ratings`, `Threads`, `Captcha`, `Takedown` | Smaller API groups exposed through `Misc` as convenience properties. |

## Common Examples

### Search Manga

```csharp
var results = await api.Manga.List(new MangaFilter
{
    Title = "The Unrivaled Mememori-kun",
    ContentRating = [ContentRating.safe, ContentRating.suggestive],
    Includes = [MangaIncludes.cover_art, MangaIncludes.author],
    Order = new()
    {
        [MangaFilter.OrderKey.relevance] = OrderValue.desc
    }
});
```

### Read A Manga Feed

```csharp
var chapters = await api.Manga.Feed("fc0a7b86-992e-4126-b30f-ca04811979bf", new MangaFeedFilter
{
    TranslatedLanguage = ["en"],
    IncludeUnavailable = false,
    ExternalUrl = null,
    Order = new()
    {
        [MangaFeedFilter.OrderKey.chapter] = OrderValue.asc
    }
});
```

### Download Pages

```csharp
var pages = await api.Pages.Pages("2c98fbe9-a63f-47c2-9862-ecc9199610a2");

foreach (var page in pages.Chapter.Data)
{
    var url = $"{pages.BaseUrl}/data/{pages.Chapter.Hash}/{page}";
    Console.WriteLine(url);
}
```

### Check Authentication

```csharp
var auth = await api.Auth.Check("<access-token>");

if (auth.IsAuthenticated)
    Console.WriteLine(string.Join(", ", auth.Permissions));
```

### Get Current User And Reading History

```csharp
var me = await api.User.Me("<access-token>");
var history = await api.User.History("<access-token>");

Console.WriteLine(me.Data.Attributes?.Username);
Console.WriteLine($"History entries: {history.Ratings.Count}");
```

### Work With Custom Lists

```csharp
var list = await api.Lists.Create(new CustomListCreate
{
    Name = "Favorites",
    Visibility = Visibility.private,
    Manga = []
}, "<access-token>");

await api.Lists.MangaAdd("fc0a7b86-992e-4126-b30f-ca04811979bf", list.Data.Id, order: 0, token: "<access-token>");
```

### Upload Cover Art

```csharp
using var file = new PathFileUpload("cover.jpg");

var cover = await api.Cover.Upload("fc0a7b86-992e-4126-b30f-ca04811979bf", new CoverArtCreate
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

Console.WriteLine($"Approval required: {approval.RequiresApproval}");
```

### Map Legacy IDs

```csharp
var mappings = await api.Legacy.LegacyMapping(LegacyMappingType.manga, 1, 2, 3);

foreach (var mapping in mappings.Data)
    Console.WriteLine($"{mapping.Attributes?.LegacyId} -> {mapping.Attributes?.NewId}");
```

### User Settings

```csharp
using System.Text.Json;

var current = await api.Settings.Get("<access-token>");

using var doc = JsonDocument.Parse("""{"theme":"dark"}""");
var updated = await api.Settings.Update(new UserSettingsUpdate
{
    Settings = doc.RootElement.Clone(),
    UpdatedAt = DateTime.UtcNow
}, "<access-token>");
```

## Authentication

MangaDex uses OAuth2 bearer tokens for account-level routes. Create an API client from MangaDex account settings, then request tokens with `api.Auth`.

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

Refresh tokens:

```csharp
var refreshed = await api.Auth.Refresh(token.RefreshToken, "<client-id>", "<client-secret>");
```

Check or logout API tokens:

```csharp
var check = await api.Auth.Check(refreshed.AccessToken);
var logout = await api.Auth.Logout(refreshed.AccessToken);
```

Legacy username/password login routes remain available on `api.User`, but they are obsolete and should not be preferred for new applications.

## Configuration

Configuration can come from `MangaDex.Create`, dependency injection, `IConfiguration`, or custom services.

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

Default configuration key paths can be changed through static properties such as:

```csharp
ConfigurationCredentialsService.TokenPath = "MangaDex:AccessToken";
ConfigurationApi.ApiPath = "MangaDex:ApiUrl";
ConfigurationApi.UserAgentPath = "MangaDex:UserAgent";
ConfigurationOIDC.ClientIdPath = "MangaDex:ClientId";
```

For advanced credential loading, implement `ICredentialsService`:

```csharp
public class MyCredentialsService : ICredentialsService
{
    public Task<string> GetToken()
    {
        return Task.FromResult("<access-token>");
    }
}

builder.Services.AddMangaDex(c => c.WithCredentials<MyCredentialsService>());
```

## Uploading And Editing Chapters

Install `MangaDexSharp.Utilities` for the higher-level upload workflow.

```csharp
using MangaDexSharp;
using MangaDexSharp.Utilities.Upload;
using Microsoft.Extensions.DependencyInjection;

var provider = new ServiceCollection()
    .AddMangaDex(c => c
        .WithAuthConfig("<client-id>", "<client-secret>", "<username>", "<password>")
        .AddMangaDexUtils())
    .BuildServiceProvider();

var upload = provider.GetRequiredService<IUploadUtilityService>();

var mangaId = "f9c33607-9180-4ba6-b85c-e4b5faee7192";
var groups = new[] { "e11e461b-8c3a-4b5c-8b07-8892c2dcf449" };

await using var session = await upload.New(mangaId, groups, c => c.MaxBatchSize(5));

await session.UploadFile("page-1.jpg");
await session.UploadFile("page-2.jpg");

var chapter = await session.Commit(new ChapterDraft
{
    Chapter = "1",
    Title = "Example Chapter",
    TranslatedLanguage = "en",
    Volume = "1"
});

Console.WriteLine(chapter.Id);
```

## Documentation

The `docs` folder contains package, app, and generated API reference pages:

- [Documentation index](docs/README.md)
- [Core package guide](docs/packages/mangadexsharp.md)
- [Utilities package guide](docs/packages/mangadexsharp-utilities.md)
- [Updates poll guide](docs/packages/mangadexsharp-updatespoll.md)
- [API reference](docs/api/README.md)
