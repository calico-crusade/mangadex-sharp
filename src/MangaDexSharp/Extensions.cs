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
	/// The base URL for the MangaDex authentication service
	/// </summary>
	public const string AUTH_URL = "https://auth.mangadex.org";

	/// <summary>
	/// The base URL for the MangaDex developer authentication service
	/// </summary>
	public const string AUTH_DEV_URL = "https://auth.mangadex.dev";

    /// <summary>
    /// The user agent to use for all requests
    /// </summary>
    public const string API_USER_AGENT = "manga-dex-sharp";

	/// <summary>
	/// An array of all of the available content ratings
	/// </summary>
	public static ContentRating[] ContentRatingsAll => 
	[
		ContentRating.safe,
		ContentRating.erotica,
		ContentRating.suggestive,
		ContentRating.pornographic
	];

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
			.ToArray() ?? [];
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
	/// Adds all of the base MangaDex services to the given service collection
	/// </summary>
	/// <param name="services">The service collection to inject to</param>
	/// <returns>The service collection for chaining</returns>
	internal static IServiceCollection AddBaseMangaDex(this IServiceCollection services)
	{
		return services
			.AddHttpClient()
			.AddTransient<IMangaDex, MangaDex>()
			.AddTransient<IMdApiService, MdApiService>()
			.AddTransient<IMdJsonService, MdJsonService>()
			.AddTransient<IMdCacheService, MdCacheService>()
			.AddTransient<IMdEventsService, MdEventsService>()

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
			.AddTransient<IMangaDexCaptchaService, MangaDexMiscService>()
			
			.AddTransient<IMangaDexAuthService, MangaDexAuthService>()
			.AddTransient<IMangaDexApiClientService, MangaDexApiClientService>();
	}
	
	/// <summary>
	/// Adds the MangaDex API to the given service collection
	/// </summary>
	/// <param name="services">The service collection to inject into</param>
	/// <returns>The service collection for chaining</returns>
	/// <remarks>You should only call this once for the service collection</remarks>
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
	/// <param name="throwOnError">Whether or not to throw an exception if the API returns an error</param>
	/// <param name="authUrl">The url for the authentication service for auth.mangadex.org (see <see cref="AUTH_URL"/> or <see cref="AUTH_DEV_URL"/>)</param>
	/// <param name="clientId">The client ID for the authorization endpoint</param>
	/// <param name="clientSecret">The client secret for the authorization endpoint</param>
	/// <param name="username">The username for the password grant OAuth2 requests</param>
	/// <param name="password">The password for the password grant OAuth2 requests</param>
    /// <returns>The service collection for chaining</returns>
	/// <remarks>You should only call this once for the service collection</remarks>
    public static IServiceCollection AddMangaDex(
		this IServiceCollection services, 
		string token, 
		string? apiUrl = null, 
		string? userAgent = null, 
		bool throwOnError = false,
        string? authUrl = null,
        string? clientId = null,
        string? clientSecret = null,
        string? username = null,
        string? password = null)
	{
		return services
			.AddMangaDex(new HardCodedCredentialsService(
				token, apiUrl, userAgent, throwOnError, 
				authUrl, clientId, clientSecret, username, password));
	}

    /// <summary>
    /// Adds the MangaDex API with the associated credential service
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ICredentialsService"/></typeparam>
    /// <param name="services">The service collection to inject into</param>
    /// <param name="transient">Whether the credential service should be registered as a transient (true) or singleton (false) service</param>
    /// <returns>The service collection for chaining</returns>
    /// <remarks>You should only call this once for the service collection</remarks>
    public static IServiceCollection AddMangaDex<T>(this IServiceCollection services, bool transient = true) where T: class, ICredentialsService
	{
		if (transient)
			services.AddTransient<ICredentialsService, T>();
		else
			services.AddSingleton<ICredentialsService, T>();

		return services
			.AddBaseMangaDex();
	}

    /// <summary>
    /// Adds the MangaDex API with the associated credential service instance
    /// </summary>
    /// <typeparam name="T">The type of <see cref="ICredentialsService"/></typeparam>
    /// <param name="services">The service collection to inject into</param>
    /// <param name="instance">The instance of the <see cref="ICredentialsService"/></param>
    /// <returns>The service collection for chaining</returns>
    /// <remarks>You should only call this once for the service collection</remarks>
    public static IServiceCollection AddMangaDex<T>(this IServiceCollection services, T instance) where T : ICredentialsService
	{
		return services
			.AddBaseMangaDex()
			.AddSingleton<ICredentialsService>(instance);
	}

    /// <summary>
    /// Adds an event watcher to MangaDexSharp
    /// </summary>
    /// <typeparam name="T">The type of event watcher</typeparam>
    /// <param name="services">The service collection to inject into</param>
	/// <param name="transient">Whether or not to register the service as a transient (true) or singleton (false)</param>
    /// <returns>The service collection for chaining</returns>
	/// <remarks>
	/// You can create and inject multiple of these and they will be run in the order they're added.
	/// You will still need to call one of the other <see cref="AddMangaDex(IServiceCollection)"/> methods to add the base services.
	/// </remarks>
    public static IServiceCollection AddMangaDexEvents<T>(this IServiceCollection services, bool transient = true) where T : class, IMdEventService
	{
        if (transient)
            services.AddTransient<IMdEventService, T>();
        else
            services.AddSingleton<IMdEventService, T>();

		return services;
    }
}