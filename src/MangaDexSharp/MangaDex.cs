namespace MangaDexSharp;

public interface IMangaDex
{
	IMangaDexMangaService Manga { get; }

	IMangaDexChapterService Chapter { get; }

	IMangaDexMiscService Misc { get; }

	IMangaDexPageService Pages { get; }

	IMangaDexAuthorService Author { get; }

	IMangaDexCoverArtService Cover { get; }

	IMangaDexCustomListService Lists { get; }

	IMangaDexFeedService Feed { get; }

	IMangaDexFollowsService Follows { get; }

	IMangaDexRatingService Ratings { get; }

	IMangaDexThreadsService Threads { get; }

	IMangaDexCaptchaService Captcha { get; }

	IMangaDexReadMarkerService ReadMarker { get; }

	IMangaDexReportService Report { get; }

	IMangaDexScanlationGroupService ScanlationGroup { get; }
}

public class MangaDex : IMangaDex
{
	public IMangaDexMangaService Manga { get; }

	public IMangaDexChapterService Chapter { get; }

	public IMangaDexMiscService Misc { get; }

	public IMangaDexAuthorService Author { get; }

	public IMangaDexCoverArtService Cover { get; }

	public IMangaDexCustomListService Lists { get; }

	public IMangaDexReadMarkerService ReadMarker { get; }

	public IMangaDexFeedService Feed { get; }

	public IMangaDexFollowsService Follows { get; }

	public IMangaDexReportService Report { get; }

	public IMangaDexScanlationGroupService ScanlationGroup { get; }

	public IMangaDexPageService Pages => Misc;

	public IMangaDexRatingService Ratings => Misc;

	public IMangaDexThreadsService Threads => Misc;

	public IMangaDexCaptchaService Captcha => Misc;

	public MangaDex(
		IMangaDexMangaService manga, 
		IMangaDexChapterService chapter,
		IMangaDexMiscService misc,
		IMangaDexAuthorService author,
		IMangaDexCoverArtService cover,
		IMangaDexCustomListService lists,
		IMangaDexFeedService feed,
		IMangaDexFollowsService follows,
		IMangaDexReadMarkerService readMarker,
		IMangaDexReportService report,
		IMangaDexScanlationGroupService scanlationGroup)
	{
		Manga = manga;
		Chapter = chapter;
		Misc = misc;
		Author = author;
		Cover = cover;
		Lists = lists;
		Feed = feed;
		Follows = follows;
		ReadMarker = readMarker;
		Report = report;
		ScanlationGroup = scanlationGroup;
	}

	public static IMangaDex Create(string? token = null)
	{
		return new ServiceCollection()
			.AddMangaDex(token ?? string.Empty)
			.BuildServiceProvider()
			.GetRequiredService<IMangaDex>();
	}
}
