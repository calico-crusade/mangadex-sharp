# mangadex-sharp
A C# API for Mangadex.org

## Usage Agreement
By using this library, you agree to follow MangaDex's [Acceptable Use Policy](https://api.mangadex.org/docs/#acceptable-usage-policy) and [Acknowledge the General Connection Requirements](https://api.mangadex.org/docs/2-limitations/#general-connection-requirements).

Note: Using a spoofed user-agent is not allowed and will result in being banned from using the API. There is no reason to spoof the user-agent, except for malicious purposes.

## Installation
You can install the NuGet package with Visual Studio. It targets .net standard 2.1 to take advantage of most of the new features within C# and .net.

```
PM> Install-Package MangaDexSharp
```

## Setup
You can either use it directly or via dependency injection (for use with asp.net core).
> **_NOTE:_** It is no longer necessary to manually register the `CardboardBox.Http` and `CardboardBox.Json` packages as of v1.0.20.

### Depdency Injection:
```csharp

using MangaDexSharp;

var builder = WebApplication.CreateBuilder(args);

...

//This will find the authentication token in your configuration file (appsettings.json) under: "MangaDex:Token"
builder.Services.AddMangaDex(); 
//Or, if you want to inject the token directly you can use this (You don't need both of these.):
builder.Services.AddMangaDex(c => c.WithAccessToken("<AUTH TOKEN HERE>"));

var app = builder.Build();
```

Then you can inject the API into any of your controllers or other services:
```csharp
using MangaDexSharp;
using Microsoft.AspNetCore.Mvc;

namespace SomeApplication;

[ApiController]
public class SomeController : ControllerBase
{
    private readonly IMangaDex _md;

    public SomeController(IMangaDex md)
    {
        _md = md;
    }

    [HttpGet, Route("manga/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _md.Manga.Get(id));
    }
}
```


### Directly:
```csharp
using MangaDexSharp;

...
//You cannot access authed routes if you use this option.
var api = MangaDex.Create();

//However, you can specify the token like so:
var api = MangaDex.Create(c => c.WithAccessToken("<AUTH TOKEN HERE>"));

var manga = await api.Manga.Get("some-manga-id-here");
```

## Usage
The API follows the [docs](https://api.mangadex.org/docs/redoc.html) pretty closely. 
With the root object for the api being the `IMangaDex` interface.
Once you have an instance (see above), you can access any of the sub-sections within it.
Here are some common use cases:

```csharp
var api = MangaDex.Create();

//Fetching a manga by manga ID:
var manga = await api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");

//Searching for a manga via it's title:
var results = await api.Manga.List(new MangaFilter 
{
    Title = "The Unrivaled Mememori-kun"
});

//Get all of the chapters from a manga by manga ID:
var chapters = await api.Manga.Feed("fc0a7b86-992e-4126-b30f-ca04811979bf");

//Fetch a chapter by chapter ID:
var chapter = await api.Chapter.Get("2c98fbe9-a63f-47c2-9862-ecc9199610a2");

//Get all of the pages of a specific chapter by chapter ID:
var pages = await api.Pages.Pages("2c98fbe9-a63f-47c2-9862-ecc9199610a2");
```

## Authentication
MangaDex switched to authorization bearer tokens via an OAuth2 flow recently. 
In order to access any resources that require an account, you will need to get one of those tokens (you can read more [here](https://api.mangadex.org/docs/02-authentication/)).
Once you have a bearer token, you can either add it at an API level or for a specific request, you can also create your own token service provider.

> Note: You can see an example of how to fetch bearer tokens for the new OAuth2 flow in the `src/MangaDexSharp.OAuthLocal.Web` project.

### OAuth2 Personal Clients:
You can use the OAuth2 Personal client to fetch the session token like so:

```csharp
var api = MangaDex.Create();

//You can get these from https://mangadex.org/settings under the "API Clients" tab.
string clientId = "<client-id>"; 
string clientSecret = "<client-secret>";

//These are your mangadex.org account credentials
string username = "<username>";
string password = "<password>";

//Request the tokens from the authorization service
var auth = await api.Auth.Personal(clientId, clientSecret, username, password);
var accessToken = auth.AccessToken;
var refreshToken = auth.RefreshToken;

var me = await api.User.Me(accessToken);

//Or you can create an authenticated api
var authedApi = MangaDex.Create(accessToken);
var me = await authedApi.User.Me();

//You can also refresh the token like so:
var refreshed = await api.Auth.Refresh(refreshToken, clientId, clientSecret);
var me = await api.User.Me(refreshed.AccessToken);
```

### OAuth2 Public Clients:
These are not implemented yet on MangaDex, this library will be updated when they are.

You can read more about them [here](https://api.mangadex.org/docs/02-authentication/public-clients/)

### Legacy Authentication method:
You can use the legacy login method to fetch the session token like so:

```csharp
var api = MangaDex.Create();

var result = await api.User.Login("<username>", "<password>");

var token = result.Data.Session;

//You can either pass the token into authenticated routes
var me = await api.User.Me(token);

//Or you can create an authenticated api
var authedApi = MangaDex.Create(c => c.WithAccessToken(token));
var me = await authedApi.User.Me();
```

> Note: These methods are technically deprecated on mangadex's docs, so they are marked as `[Obsolete]` and will show up as warnings.

### API Level:
You have 2 primary options for using this, you can either specify it directly when you create a client or inject it via configuration.

```csharp
//You can apply it directly like this:
var api = MangaDex.Create(c => c.WithAccessToken("<AUTH TOKEN HERE>"));

//Or if you're using dependency injection, you can provide it here:
builder.Services.AddMangaDex(c => c.WithAccessToken("<AUTH TOKEN HERE>"));

//Or you can include it in your appsettings.json (or environment variables):
builder.Services.AddMangaDex();
//And then add a "MangaDex:Token": "<AUTH TOKEN HERE>" to your environment variables.
//Note: you can change the name of the environment variable by setting doing this:
MangaDexSharp.ConfigurationCredentialsService.TokenPath = "SomeEnvironmentVariableName";
```

Alternatively, you can create your own `ICredentialsService` implementation and then add it via dependency injection:
```csharp
using MangaDexSharp;

//This has access to your services if you're using Dependency Injection.
public class MyCustomCredentialsService : ICredentialsService
{
    public async Task<string> GetToken() 
    {
        return "Some-token resolved here";
    }
}
...
//Then add your service like so:
builder.Services.AddMangaDex(c => c.WithCredentials<MyCustomCredentialsService>());
```

### Specific Request
Any request that requires authentication within the API has a `string? token = null` parameter on the method.
The API will default to using this parameter if it's set. 

```csharp
var api = MangaDex.Create();

//Follow a manga
await api.Manga.Follow("fc0a7b86-992e-4126-b30f-ca04811979bf", "<AUTH TOKEN HERE>");
```

## Configuration options
There are a number of different configuration options (namely: the API URL, authentication token, and the User-Agent) that you can set.

These can all be set in a few different ways:
* Via a configuration file (appsettings.json)
* Via the `MangaDex.Create()` method
* Via the DI services injector `IServiceCollection.AddMangaDex()`
* Or via a custom `ICredentialsService`.

Below is an example of how to do each of them:
```csharp
//With MangaDex.Create():
var api = MangaDex.Create(c => c
    .WithAccessToken("Some Token")
    .WithApiConfig(a => a
        .WithApiUrl("https://api.mangadex.dev")
        .WithUserAgent("Some-Fancy-User-Agent")));

//With DI services:
services.AddMangaDex(c => c
    .WithAccessToken("Some Token")
    .WithApiConfig(a => a
        .WithApiUrl("https://api.mangadex.dev")
        .WithUserAgent("Some-Fancy-User-Agent")));

//With a custom IConfigurationApi
public class SomeConfig : IConfigurationApi
{
    ...
    public string? Token => "Some Token";
    public string ApiUrl => "https://api.mangadex.dev";
    public string UserAgent => "Some-Fancy-User-Agent";
    ...
}
...
services.AddMangaDex(c => c.WithApiConfig<SomeConfig>());

//From configuration file (appsettings.json)
{
    "Mangadex": {
        "Token": "Some-Token",
        "UserAgent": "Some-Fancy-User-Agent",
        "ApiUrl": "https://api.mangadex.dev"
    }
}
```

For the last option, if you want to change the [configuration keys](https://github.com/calico-crusade/mangadex-sharp/blob/1f09a1aceef0a79d7553c45b69cd401b5ed888bb/src/MangaDexSharp/CredentialsService.cs#L28) that the application loads the variables from, you can change the following static properties:
```csharp
ConfigurationCredentialsService.TokenPath = "SomeOther:Path:ToThe:Token";
ConfigurationApi.UserAgentPath = "SomeOther:Path:ToThe:UserAgent";
ConfigurationApi.ApiPath = "SomeOther:Path:ToThe:ApiUrl";
ConfigurationApi.UserAgentPath = "SomeOther:Path:ToThe:UserAgent";
ConfigurationOIDC.AuthPath = "SomeOther:Path:ToThe:AuthUrl";
ConfigurationOIDC.ClientIdPath = "SomeOther:Path:ToThe:ClientId";
ConfigurationOIDC.ClientSecretPath = "SomeOther:Path:ToThe:ClientSecret";
ConfigurationOIDC.UsernamePath = "SomeOther:Path:ToThe:Username";
ConfigurationOIDC.PasswordPath = "SomeOther:Path:ToThe:Password";
```

## Uploading / Editing Chapters
There is now a handy utility for uploading / editing chapters via the API.

You will need to install the [new NuGet package](https://www.nuget.org/packages/MangaDexSharp.Utilities).

```bash
PM> Install-Package MangaDexSharp.Utilities
```

Then you can use it like so:

```csharp
using MangaDexSharp;
using MangaDexSharp.Utilities.Upload;

//Get an instance of the API
var provider = new ServiceCollection()
    .AddMangaDex(c => c
        .WithAuthConfig(a => a
            .WithClientId("<client-id>")
            .WithClientSecret("<client-secret>")
            .WithUsername("<username>")
            .WithPassword("<password>"))
        .AddMangaDexUtils())
    .BuildServiceProvider();

var upload = provider.GetRequiredService<IUploadUtilityService>();

//Get the manga ID and groups you want to upload to
string mangaId = "f9c33607-9180-4ba6-b85c-e4b5faee7192"; //Official "Test" Manga
string[] groups = ["e11e461b-8c3a-4b5c-8b07-8892c2dcf449"]; //Cardboard test

//Create a session for the manga
await using var session = await upload.New(mangaId, groups);

//Upload some files to the session (by file path)
await session.UploadFile("wrong-file.png");
await session.UploadFile("page-1.jpg");
await session.UploadFile("page-2.jpg");
await session.UploadFile("page-3.png");

//Maybe upload the files by stream instead?
using var io = File.OpenRead("some-weird-gif.gif");
await session.UploadFile(io, "page-4.gif");

//Woops, messed up that one, let's delete it from the upload
var file = session.Uploads
    .FirstOrDefault(t => t.Attributes.OriginalFileName == "wrong-file.png");
if (file is not null)
    await session.DeleteUpload(file);

//Commit the chapter to MD
var chapter = await session.Commit(new ChapterDraft 
{
    Chapter = "69.5",
    Title = "My Super Chapter",
    TranslatedLanguage = "en",
    Volume = "420"
});
//Print out the chapter ID
Console.WriteLine("Chapter ID: {0}", chapter.Id);

//You can also edit existing sessions:
await using var session = await upload.Continue();

//Or you can edit an existing chapter that has already been upload
var chapterId = "8f32fa09-593b-49d4-ae23-229cee63f005";
await using var session = await upload.Edit(chapterId);

//There are a bunch of settings you can change for the upload utility.
//There is a builder method you can specify when creating the sessions:
await using var session = await upload.New(mangaId, groups, 
    config => 
    {
        config.MaxBatchSize(5);
    });
```