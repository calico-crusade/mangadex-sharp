
using MangaDexSharp;
using MangaDexSharp.UpdatesPoll;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var srv = new ServiceCollection()
	.AddMangaDex()
	.AddLogging(c =>
	{
		c.AddSerilog(new LoggerConfiguration()
		 .WriteTo.Console()
		 .CreateLogger());
	})
	.AddTransient<IUpdatesPollService, UpdatesPollService>()
	.BuildServiceProvider()
	.GetRequiredService<IUpdatesPollService>();

await srv.Poll(chapters =>
{
	Console.WriteLine("Found chapters: {0}\r\n\t{1}",
		chapters.Length,
		string.Join("\r\n\t", chapters.Select(t => $"{t.Chapter.Attributes?.Chapter} - Vol {t.Chapter.Attributes?.Volume} - {t.Chapter.Attributes?.Title}: Pages: {t.PageUrls.Length}")));
	return Task.CompletedTask;
}, langs: new[] { "en" });