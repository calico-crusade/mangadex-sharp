using MangaDexSharp;

var api = MangaDex.Create();

var manga = await api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");

Console.WriteLine($"Title: {manga.Data.Attributes.Title.First().Value}");