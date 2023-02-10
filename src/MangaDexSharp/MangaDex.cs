namespace MangaDexSharp;

public interface IMangaDex
{
	IMangaDexMangaService Manga { get; }

	IMangaDexChapterService Chapter { get; }

	IMangaDexPagesService Pages { get; }

	IMangaDexAuthorService Author { get; }

	IMangaDexCoverArtService Cover { get; }
}

public class MangaDex : IMangaDex
{
	public IMangaDexMangaService Manga { get; }

	public IMangaDexChapterService Chapter { get; }

	public IMangaDexPagesService Pages { get; }

	public IMangaDexAuthorService Author { get; }

	public IMangaDexCoverArtService Cover { get; }

	public MangaDex(
		IMangaDexMangaService manga, 
		IMangaDexChapterService chapter,
		IMangaDexPagesService pages,
		IMangaDexAuthorService author,
		IMangaDexCoverArtService cover)
	{
		Manga = manga;
		Chapter = chapter;
		Pages = pages;
		Author = author;
		Cover = cover;
	}

	public static IMangaDex Create(string? token = null)
	{
		return new ServiceCollection()
			.AddMangaDex(token ?? string.Empty)
			.BuildServiceProvider()
			.GetRequiredService<IMangaDex>();
	}
}
