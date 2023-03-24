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
});

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

//Example of using the old login method to fetch the current user's token
string username = "",
	   password = "";

var result = await api.User.Login(username, password); //While it says the method is obsolete, it will still work (for now)
var token = result.Data.Session;

//You can either pass the token to the authenticated endpoints
var me = await api.User.Me(token);
//Or you can create a authenticated API
var authedApi = MangaDex.Create(token);
var me2 = await authedApi.User.Me();

Console.WriteLine("Login Success: " + me.Data.Attributes.Username);