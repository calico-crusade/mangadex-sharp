namespace MangaDexSharp;

public interface IMangaDex
{
	IMangaDexMangaService Manga { get; }

	IMangaDexChapterService Chapter { get; }

	IMangaDexPagesService Pages { get; }
}

public class MangaDex : IMangaDex
{
	public IMangaDexMangaService Manga { get; }

	public IMangaDexChapterService Chapter { get; }

	public IMangaDexPagesService Pages { get; }

	public MangaDex(
		IMangaDexMangaService manga, 
		IMangaDexChapterService chapter,
		IMangaDexPagesService pages)
	{
		Manga = manga;
		Chapter = chapter;
		Pages = pages;
	}

	public static IMangaDex Create(string? token = null)
	{
		return new ServiceCollection()
			.AddMangaDex(token ?? string.Empty)
			.BuildServiceProvider()
			.GetRequiredService<IMangaDex>();
	}
}
