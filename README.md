# mangadex-sharp
A C# API for Mangadex.org

## Installation
You can install the nuget package with Visual Studio. It targets .net standard 2.1 to take advantage of most of the new features within C# and .net.

```
PM> Install-Package MangaDexSharp
```

## Setup
You can either use it directly or via dependency injection (for use with asp.net core).

### Depdency Injection:
```csharp

using MangaDexSharp;

var builder = WebApplication.CreateBuilder(args);

...

//This will find the authentication token in your configuration file (appsettings.json) under: "MangaDex:Token"
builder.Services.AddMangaDex(); 
//Or, if you want to inject the token directly you can use this (You don't need both of these.):
builder.Services.AddMangaDex("<AUTH TOKEN HERE>");

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
var api = MangaDex.Create("<AUTH TOKEN HERE>");

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
var pages = await api.Pages.Get("2c98fbe9-a63f-47c2-9862-ecc9199610a2");
```

## Authentication
MangaDex switched to authorization bearer tokens via an OAuth2 flow recently. 
In order to access any resources that require an account, you will need to get one of those tokens (you can read more [here](https://api.mangadex.org/docs/authentication/) once they update the docs).
Once you have a bearer token, you can either add it at an API level or for a specific request, you can also create your own token service provider.

### API Level:
You have 2 primary options for using this, you can either specify it directly when you create a client or inject it via configuration.

```csharp
//You can apply it directly like this:
var api = MangaDex.Create("<AUTH TOKEN HERE>");

//Or if you're using dependency injection, you can provide it here:
builder.Services.AddMangaDex("<AUTH TOKEN HERE>");

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
builder.Services.AddMangaDex<MyCustomCredentialsService>();
```

### Specific Request
Any request that requires authentication within the API has a `string? token = null` parameter on the method.
The API will default to using this parameter if it's set. 

```csharp
var api = MangaDex.Create();

//Follow a manga
await api.Manga.Follow("fc0a7b86-992e-4126-b30f-ca04811979bf", "<AUTH TOKEN HERE>");
```