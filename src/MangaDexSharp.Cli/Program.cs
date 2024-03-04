using MangaDexSharp;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

//Create a manga dex API instance
var api = MangaDex.Create(config: (c) =>
{
	//Add some logging providers
	c.AddLogging(a =>
	{
		a.AddSerilog(new LoggerConfiguration()
			.WriteTo.Console()
			.CreateLogger());
	});
}, throwOnError: true);


var chapter = await api.Chapter.Get("158f54ed-9983-406d-b882-208858288874");
Console.WriteLine("Chapter: " + chapter.Data.Attributes.Title);

var group = await api.ScanlationGroup.List(new ScanlationGroupFilter
{
	Name = "europaisacoolmoon"
});

Console.WriteLine("Group: " + group?.Data?.FirstOrDefault()?.Attributes?.Name);

//Example of using any of the ListAll endpoints
//These are useful for fetching all of the results of a specific paginated request
var allChaps = api.Chapter.ListAll(new ChaptersFilter
{
	Limit = 10,
	Offset = 0,
	Manga = "fc0a7b86-992e-4126-b30f-ca04811979bf"
});

await foreach(var chap in allChaps)
{
	Console.WriteLine("Chap: {0} - vol {1} - {2}", chap.Attributes.Chapter, chap.Attributes.Volume, chap.Attributes.Title);
}

//Example of fetching a manga by ID
var manga = await api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");
Console.WriteLine($"Title: {manga.Data.Attributes.Title.First().Value}");


//Example of using the personal client auth method
string clientId = "Your client ID from https://mangadex.org/settings",
	   clientSecret = "Your client secret from https://mangadex.org/settings";

string username = "Your account username for https://mangadex.org",
       password = "Your account password for https://mangadex.org";

var auth = await api.Auth.Personal(clientId, clientSecret, username, password);
Console.WriteLine("Token: " + auth.AccessToken);

var refresh = await api.Auth.Refresh(auth.RefreshToken, clientId, clientSecret);
Console.WriteLine("Refreshed Token: " + refresh.AccessToken);

//Example of using the old login method to fetch the current user's token
var result = await api.User.Login(username, password); //While it says the method is obsolete, it will still work (for now)
var token = result.Data.Session;

//You can either pass the token to the authenticated endpoints
var me = await api.User.Me(token);
//Or you can create a authenticated API
var authedApi = MangaDex.Create(token);
var me2 = await authedApi.User.Me();

Console.WriteLine("Login Success: " + me.Data.Attributes.Username);