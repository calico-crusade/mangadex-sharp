using MangaDexSharp;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var api = MangaDex.Create(config: (c) =>
{
	c.AddLogging(a =>
	{
		a.AddSerilog(new LoggerConfiguration()
			.WriteTo.Console()
			.CreateLogger());
	});
});

var manga = await api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");

Console.WriteLine($"Title: {manga.Data.Attributes.Title.First().Value}");


string username = "",
	   password = "";

var result = await api.User.Login(username, password);

var me = await api.User.Me(result.Data.Session);

Console.WriteLine("Login Success: " + me.Data.Attributes.Username);