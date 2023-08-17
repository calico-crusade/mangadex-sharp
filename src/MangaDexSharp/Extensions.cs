using System.Net.Http;

namespace MangaDexSharp;

/// <summary>
/// A bunch of useful extensions for MD related tasks
/// </summary>
public static class Extensions
{
	/// <summary>
	/// The base API URL for the production MD instance
	/// </summary>
	public const string API_ROOT = "https://api.mangadex.org";

	/// <summary>
	/// The base API URL for the developer sandbox MD instance
	/// </summary>
	public const string API_DEV_ROOT = "https://api.mangadex.dev";

	/// <summary>
	/// The user agent to use for all requests
	/// </summary>
	public const string API_USER_AGENT = "manga-dex-sharp";

	/// <summary>
	/// An array of all of the available content ratings
	/// </summary>
	public static ContentRating[] ContentRatingsAll => new[]
	{
		ContentRating.safe,
		ContentRating.erotica,
		ContentRating.suggestive,
		ContentRating.pornographic
	};

	/// <summary>
	/// Provides a method of resolving the user's authentication token from varying sources
	/// </summary>
	/// <param name="token">The contextual token</param>
	/// <param name="creds">The credentials service</param>
	/// <param name="optional">Whether or not the token is optional for this request</param>
	/// <returns>The current request with the attached authentication token</returns>
	/// <exception cref="ArgumentException">Thrown if the authentication token is required but is missing</exception>
	public static async Task<Action<HttpRequestMessage>> Auth(string? token, ICredentialsService creds, bool optional = false)
	{
		token ??= await creds.GetToken();
		if (string.IsNullOrEmpty(token) && optional) return c => { };

		if (string.IsNullOrEmpty(token))
			throw new ArgumentException("No token provided by credentials service", nameof(token));

		return c => c.Headers.Add("Authorization", "Bearer " + token);
	}

	/// <summary>
	/// Fetches all of a specific type of related item
	/// </summary>
	/// <typeparam name="T">The type of relationship data to fetch</typeparam>
	/// <param name="source">Where to fetch the relationship data from</param>
	/// <returns>The related items</returns>
	public static T[] Relationship<T>(this IRelationshipModel? source) where T: IRelationship
	{
		return source?
			.Relationships
			.Where(t => t is T)
			.Select(t => (T)t)
			.ToArray() ?? Array.Empty<T>();
	}

	/// <summary>
	/// Fetches all of the cover art from the given source
	/// </summary>
	/// <param name="source">The relationship source</param>
	/// <returns>All of the related cover art</returns>
	public static CoverArtRelationship[] CoverArt(this IRelationshipModel? source) => source.Relationship<CoverArtRelationship>();

	/// <summary>
	/// Fetches all of the people from the given source
	/// </summary>
	/// <param name="source">The relationship source</param>
	/// <returns>All of the related people</returns>
	public static PersonRelationship[] People(this IRelationshipModel? source) => source.Relationship<PersonRelationship>();

	/// <summary>
	/// Fetches all of the related manga from the given source
	/// </summary>
	/// <param name="source">The relationship source</param>
	/// <returns>All of the related manga</returns>
	public static RelatedDataRelationship[] Manga(this IRelationshipModel? source) => source.Relationship<RelatedDataRelationship>();

	/// <summary>
	/// Fetches all of the related scanlation groups from the given source
	/// </summary>
	/// <param name="source">The relationship source</param>
	/// <returns>All of the related scanlation groups</returns>
	public static ScanlationGroup[] ScanlationGroups(this IRelationshipModel? source) => source.Relationship<ScanlationGroup>();

	/// <summary>
	/// Fetches all of the related users from the given source
	/// </summary>
	/// <param name="source">The relationship source</param>
	/// <returns>All of the related users</returns>
	public static User[] Users(this IRelationshipModel? source)  => source.Relationship<User>();

	/// <summary>
	/// Adds all of the base mangadex services to the given service collection
	/// </summary>
	/// <param name="services">The service collection to inject to</param>
	/// <returns>The service collection for chaining</returns>
	private static IServiceCollection AddBaseMangaDex(this IServiceCollection services)
	{
		return services
			.AddTransient<IMangaDex, MangaDex>()
			.AddTransient<IMdApiService, MdApiService>()

			.AddTransient<IMangaDexMangaService, MangaDexMangaService>()
			.AddTransient<IMangaDexChapterService, MangaDexChapterService>()
			.AddTransient<IMangaDexAuthorService, MangaDexAuthorService>()
			.AddTransient<IMangaDexCoverArtService, MangaDexCoverArtService>()
			.AddTransient<IMangaDexCustomListService, MangaDexCustomListService>()
			.AddTransient<IMangaDexFeedService, MangaDexFeedService>()
			.AddTransient<IMangaDexFollowsService, MangaDexFollowsService>()
			.AddTransient<IMangaDexReadMarkerService, MangaDexReadMarkerService>()
			.AddTransient<IMangaDexReportService, MangaDexReportService>()
			.AddTransient<IMangaDexScanlationGroupService, MangaDexScanlationGroupService>()
			.AddTransient<IMangaDexUploadService, MangaDexUploadService>()
			.AddTransient<IMangaDexUserService, MangaDexUserService>()

			.AddTransient<IMangaDexMiscService, MangaDexMiscService>()
			.AddTransient<IMangaDexPageService, MangaDexMiscService>()
			.AddTransient<IMangaDexRatingService, MangaDexMiscService>()
			.AddTransient<IMangaDexThreadsService, MangaDexMiscService>()
			.AddTransient<IMangaDexCaptchaService, MangaDexMiscService>();
	}
	
	/// <summary>
	/// Adds the MangaDex API to the given service colleciton
	/// </summary>
	/// <param name="services">The service collection to inject into</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddMangaDex(this IServiceCollection services)
	{
		return services.AddMangaDex<ConfigurationCredentialsService>();
	}

    /// <summary>
    /// Adds the MangaDex API with the associated authentication token and API url to the given service collection
    /// </summary>
    /// <param name="services">The service collection to inject into</param>
    /// <param name="token">The user's authentication token</param>
    /// <param name="apiUrl">The API url for the MangaDex environment (see <see cref="API_ROOT"/> or <see cref="API_DEV_ROOT"/>)</param>
    /// <param name="userAgent">The User-Agent header to send with requests (see <see cref="API_USER_AGENT"/>)</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddMangaDex(this IServiceCollection services, string token, string? apiUrl = null, string? userAgent = null)
	{
		return services
			.AddMangaDex(new HardCodedCredentialsService(token, apiUrl, userAgent));
	}

	/// <summary>
	/// Adds the MangaDex API with the associated credential service
	/// </summary>
	/// <typeparam name="T">The type of <see cref="ICredentialsService"/></typeparam>
	/// <param name="services">The service collection to inject into</param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddMangaDex<T>(this IServiceCollection services) where T: class, ICredentialsService
	{
		return services
			.AddBaseMangaDex()
			.AddTransient<ICredentialsService, T>();
	}

	/// <summary>
	/// Adds the MangaDex API with the associated credential service instance
	/// </summary>
	/// <typeparam name="T">The type of <see cref="ICredentialsService"/></typeparam>
	/// <param name="services">The service collection to inject into</param>
	/// <param name="instance">The instance of the <see cref="ICredentialsService"/></param>
	/// <returns>The service collection for chaining</returns>
	public static IServiceCollection AddMangaDex<T>(this IServiceCollection services, T instance) where T : ICredentialsService
	{
		return services
			.AddBaseMangaDex()
			.AddSingleton<ICredentialsService>(instance);
	}
}