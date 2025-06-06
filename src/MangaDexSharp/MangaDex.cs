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

	/// <summary>
	/// The OAuth2.0 service for auth.mangadex.org
	/// </summary>
	IMangaDexAuthService Auth { get; }

	/// <summary>
	/// All of the API client endpoints
	/// </summary>
	IMangaDexApiClientService ApiClient { get; }

    /// <summary>
    /// All of the statistics endpoints
    /// </summary>
    IMangaDexStatisticsService Statistics { get; }
}

/// <inheritdoc cref="IMangaDex" />
public class MangaDex(
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
    IMangaDexUserService user,
    IMangaDexAuthService auth,
    IMangaDexApiClientService apiClient,
    IMangaDexStatisticsService statistics) : IMangaDex
{
    /// <inheritdoc />
    public IMangaDexMangaService Manga { get; } = manga;

    /// <inheritdoc />
    public IMangaDexChapterService Chapter { get; } = chapter;

    /// <inheritdoc />
    public IMangaDexMiscService Misc { get; } = misc;

    /// <inheritdoc />
    public IMangaDexAuthorService Author { get; } = author;

    /// <inheritdoc />
    public IMangaDexCoverArtService Cover { get; } = cover;

    /// <inheritdoc />
    public IMangaDexCustomListService Lists { get; } = lists;

    /// <inheritdoc />
    public IMangaDexReadMarkerService ReadMarker { get; } = readMarker;

    /// <inheritdoc />
    public IMangaDexFeedService Feed { get; } = feed;

    /// <inheritdoc />
    public IMangaDexFollowsService Follows { get; } = follows;

    /// <inheritdoc />
    public IMangaDexReportService Report { get; } = report;

    /// <inheritdoc />
    public IMangaDexScanlationGroupService ScanlationGroup { get; } = scanlationGroup;

    /// <inheritdoc />
    public IMangaDexUploadService Upload { get; } = upload;

    /// <inheritdoc />
    public IMangaDexUserService User { get; } = user;

    /// <inheritdoc />
    public IMangaDexPageService Pages => Misc;

    /// <inheritdoc />
    public IMangaDexRatingService Ratings => Misc;

    /// <inheritdoc />
    public IMangaDexThreadsService Threads => Misc;

    /// <inheritdoc />
    public IMangaDexCaptchaService Captcha => Misc;

    /// <inheritdoc />
    public IMangaDexAuthService Auth { get; } = auth;

    /// <inheritdoc />
    public IMangaDexApiClientService ApiClient { get; } = apiClient;

    /// <inheritdoc />
	public IMangaDexStatisticsService Statistics { get; } = statistics;

    /// <summary>
    /// Creates an isolated instance of the MangaDex API 
    /// </summary>
    /// <param name="config">The optional configuration action</param>
    /// <param name="services">The optional service collection to use</param>
    /// <returns>The instance of the MangaDex API</returns>
    public static IMangaDex Create(
		Action<IMangaDexBuilder>? config = null,
        IServiceCollection? services = null)
	{
		return (services ?? new ServiceCollection())
            .AddMangaDex(config)
			.BuildServiceProvider()
			.GetRequiredService<IMangaDex>();
	}
}
