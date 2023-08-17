using CardboardBox.Json;

namespace MangaDexSharp;

/// <summary>
/// Represents an instance of the MangaDex API
/// </summary>
public interface IMangaDex
{
	/// <summary>
	/// All of the manga endpoints
	/// </summary>
	IMangaDexMangaService Manga { get; }

	/// <summary>
	/// All of the chapter endpoints
	/// </summary>
	IMangaDexChapterService Chapter { get; }

	/// <summary>
	/// All of the miscellaneous endpoints
	/// </summary>
	IMangaDexMiscService Misc { get; }

	/// <summary>
	/// All of the chapter-page endpoints
	/// </summary>
	IMangaDexPageService Pages { get; }

	/// <summary>
	/// All of the author endpoints
	/// </summary>
	IMangaDexAuthorService Author { get; }

	/// <summary>
	/// All of the cover-art endpoints
	/// </summary>
	IMangaDexCoverArtService Cover { get; }

	/// <summary>
	/// All of the custom lists endpoints
	/// </summary>
	IMangaDexCustomListService Lists { get; }

	/// <summary>
	/// All of the manga-feed endpoints
	/// </summary>
	IMangaDexFeedService Feed { get; }

	/// <summary>
	/// All of the follows endpoints
	/// </summary>
	IMangaDexFollowsService Follows { get; }

	/// <summary>
	/// All of the ratings endpoints
	/// </summary>
	IMangaDexRatingService Ratings { get; }

	/// <summary>
	/// All of the threads endpoints
	/// </summary>
	IMangaDexThreadsService Threads { get; }

	/// <summary>
	/// All of the captcha endpoints
	/// </summary>
	IMangaDexCaptchaService Captcha { get; }

	/// <summary>
	/// All of the read-status endpoints
	/// </summary>
	IMangaDexReadMarkerService ReadMarker { get; }

	/// <summary>
	/// All of the report endpoints
	/// </summary>
	IMangaDexReportService Report { get; }

	/// <summary>
	/// All of the scanlation group endpoints
	/// </summary>
	IMangaDexScanlationGroupService ScanlationGroup { get; }

	/// <summary>
	/// All of the upload session endpoints
	/// </summary>
	IMangaDexUploadService Upload { get; }

	/// <summary>
	/// All of the user endpoints
	/// </summary>
	IMangaDexUserService User { get; }
}

/// <summary>
/// An instance of the MangaDex API
/// </summary>
public class MangaDex : IMangaDex
{
	/// <summary>
	/// All of the manga endpoints
	/// </summary>
	public IMangaDexMangaService Manga { get; }

	/// <summary>
	/// All of the chapter endpoints
	/// </summary>
	public IMangaDexChapterService Chapter { get; }

	/// <summary>
	/// All of the miscellaneous endpoints
	/// </summary>
	public IMangaDexMiscService Misc { get; }

	/// <summary>
	/// All of the author endpoints
	/// </summary>
	public IMangaDexAuthorService Author { get; }

	/// <summary>
	/// All of the cover-art endpoints
	/// </summary>
	public IMangaDexCoverArtService Cover { get; }

	/// <summary>
	/// All of the custom lists endpoints
	/// </summary>
	public IMangaDexCustomListService Lists { get; }

	/// <summary>
	/// All of the read-status endpoints
	/// </summary>
	public IMangaDexReadMarkerService ReadMarker { get; }

	/// <summary>
	/// All of the manga-feed endpoints
	/// </summary>
	public IMangaDexFeedService Feed { get; }

	/// <summary>
	/// All of the follows endpoints
	/// </summary>
	public IMangaDexFollowsService Follows { get; }

	/// <summary>
	/// All of the report endpoints
	/// </summary>
	public IMangaDexReportService Report { get; }

	/// <summary>
	/// All of the scanlation group endpoints
	/// </summary>
	public IMangaDexScanlationGroupService ScanlationGroup { get; }

	/// <summary>
	/// All of the upload session endpoints
	/// </summary>
	public IMangaDexUploadService Upload { get; }

	/// <summary>
	/// All of the user endpoints
	/// </summary>
	public IMangaDexUserService User { get; }

	/// <summary>
	/// All of the chapter-page endpoints
	/// </summary>
	public IMangaDexPageService Pages => Misc;

	/// <summary>
	/// All of the ratings endpoints
	/// </summary>
	public IMangaDexRatingService Ratings => Misc;

	/// <summary>
	/// All of the threads endpoints
	/// </summary>
	public IMangaDexThreadsService Threads => Misc;

	/// <summary>
	/// All of the captcha endpoints
	/// </summary>
	public IMangaDexCaptchaService Captcha => Misc;

	/// <summary>
	/// Dependency Injection CTOR
	/// </summary>
	/// <param name="manga"></param>
	/// <param name="chapter"></param>
	/// <param name="misc"></param>
	/// <param name="author"></param>
	/// <param name="cover"></param>
	/// <param name="lists"></param>
	/// <param name="feed"></param>
	/// <param name="follows"></param>
	/// <param name="readMarker"></param>
	/// <param name="report"></param>
	/// <param name="scanlationGroup"></param>
	/// <param name="upload"></param>
	/// <param name="user"></param>
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
		IMangaDexScanlationGroupService scanlationGroup,
		IMangaDexUploadService upload,
		IMangaDexUserService user)
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
		Upload = upload;
		User = user;
	}

    /// <summary>
    /// Creates an isolated instance of the MangaDex API 
    /// </summary>
    /// <param name="token">The optional authentication token</param>
    /// <param name="apiUrl">The optional api URL</param>
    /// <param name="config">The optional configuration action</param>
    /// <param name="userAgent">The User-Agent header to send with requests (see <see cref="API_USER_AGENT"/>)</param>
    /// <returns>The instance of the MangaDex API</returns>
    public static IMangaDex Create(string? token = null, string? apiUrl = null, Action<IServiceCollection>? config = null, string? userAgent = null)
	{
		var create = new ServiceCollection()
			.AddMangaDex(token ?? string.Empty, apiUrl, userAgent)
			.AddCardboardHttp()
			.AddJson();

		config?.Invoke(create);

		return create
			.BuildServiceProvider()
			.GetRequiredService<IMangaDex>();
	}
}
