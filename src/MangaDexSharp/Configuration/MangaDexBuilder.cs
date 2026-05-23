namespace MangaDexSharp;

/// <summary>
/// A service for building the MangaDex API client configuration
/// </summary>
public interface IMangaDexBuilder
{
    /// <summary>
    /// Uses the given api configuration for the MangaDex API client
    /// </summary>
    /// <typeparam name="T">The instance of the api configuration</typeparam>
    /// <param name="transient">Whether or not to register the service as a transient or singleton</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithApiConfig<T>(bool transient = true) where T : class, IConfigurationApi;

    /// <summary>
    /// Uses the given api configuration for the MangaDex API client
    /// </summary>
    /// <param name="config">The configuration to use</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithApiConfig(IConfigurationApi config);

	/// <summary>
	/// Uses the given configuration for the MangaDex API client
	/// </summary>
	/// <param name="apiUrl">The URL for the MangaDex API</param>
	/// <param name="userAgent">The User-Agent header to send with requests</param>
	/// <param name="throwOnError">Whether or not to throw an exception if the API returns an error</param>
	/// <param name="rateLimits">Whether or not to observe and handle rate limits</param>
	/// <param name="conservative">Whether or not to use conservative rate limits</param>
	/// <returns>The current builder for fluent method chaining</returns>
	/// <remarks>
	/// Enabling <paramref name="conservative"/> limits will use one less request per period
	/// </remarks>
	IMangaDexBuilder WithApiConfig(
        string? apiUrl = null, string? userAgent = null, 
        bool throwOnError = false, bool rateLimits = true, 
        bool conservative = true);

    /// <summary>
    /// Uses the given configuration for the MangaDex API client
    /// </summary>
    /// <param name="config">The configuration</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithApiConfig(Action<MangaDexApiConfigBuilder> config);

    /// <summary>
    /// Uses the given configuration for the MangaDex auth OIDC client
    /// </summary>
    /// <typeparam name="T">The instance of the OIDC configuration</typeparam>
    /// <param name="transient">Whether or not to register the service as a transient or singleton</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithAuthConfig<T>(bool transient = true) where T : class, IConfigurationOIDC;

    /// <summary>
    /// Uses the given configuration for the MangaDex auth OIDC client
    /// </summary>
    /// <param name="config">The configuration to use</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithAuthConfig(IConfigurationOIDC config);

    /// <summary>
    /// Uses the given configuration for the MangaDex auth OIDC client
    /// </summary>
    /// <param name="clientId">The client ID for the authorization endpoint</param>
    /// <param name="clientSecret">The client secret for the authorization endpoint</param>
    /// <param name="username">The username for password grant requests</param>
    /// <param name="password">The password for password grant requests</param>
    /// <param name="authUrl">The Auth URL service for MangaDex</param>
    /// <param name="realmPath">The portion of the URL that indicates the realm to use</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithAuthConfig(
        string clientId, string clientSecret, 
        string username, string password, 
        string? authUrl = null, string? realmPath = null);

    /// <summary>
    /// Uses the given configuration for the MangaDex auth OIDC client
    /// </summary>
    /// <param name="config">The configuration to use</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithAuthConfig(Action<MangaDexOIDCConfigBuilder> config);

    /// <summary>
    /// Uses the given credentials service for the MangaDex API client
    /// </summary>
    /// <typeparam name="T">The instance of the credentials</typeparam>
    /// <param name="transient">Whether or not to register the service as a transient or singleton</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithCredentials<T>(bool transient = true) where T : class, ICredentialsService;

    /// <summary>
    /// Uses the given credentials service for the MangaDex API client
    /// </summary>
    /// <param name="credentials">The credentials to use</param>
    /// <returns></returns>
    IMangaDexBuilder WithCredentials(ICredentialsService credentials);

    /// <summary>
    /// Uses the given credentials service for the MangaDex API client
    /// </summary>
    /// <param name="config"></param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithCredentials(Func<IServiceProvider, ICredentialsService> config);

    /// <summary>
    /// Uses the given access token for the MangaDex API client
    /// </summary>
    /// <param name="token">The access token</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithAccessToken(string token);

    /// <summary>
    /// Uses the access token from the configuration
    /// </summary>
    /// <returns>The current builder for fluent method chaining</returns>
    /// <remarks>This uses the <see cref="ConfigurationCredentialsService"/></remarks>
    IMangaDexBuilder WithAccessTokenFromConfig();

    /// <summary>
    /// Adds caching for access token requests when using <see cref="PersonalCredentialsService"/>
    /// </summary>
    /// <typeparam name="T">The instance of the token caching service</typeparam>
    /// <param name="transient">Whether or not to register the service as a transient or singleton</param>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithAccessTokenCaching<T>(bool transient = true) where T : class, ITokenCacheService;

    /// <summary>
    /// Adds an event watcher to the MangaDex API client
    /// </summary>
    /// <typeparam name="T">The type of event watcher</typeparam>
    /// <param name="transient">Whether or not to register the service as a transient or singleton</param>
    /// <returns>The current builder for fluent method chaining</returns>
	/// <remarks>
	/// You can create and inject multiple of these and they will be run in the order they're added.
	/// </remarks>
    IMangaDexBuilder WithEventTracking<T>(bool transient = true) where T : class, IMdEventService;

    /// <summary>
    /// Registers an MD utility service with the builder.
    /// </summary>
    /// <typeparam name="TInterface">The utility's interface</typeparam>
    /// <typeparam name="TConcrete">The utility's implementation</typeparam>
    /// <returns>The current builder for fluent method chaining</returns>
    IMangaDexBuilder WithUtility<TInterface, TConcrete>()
        where TInterface : class, IMdUtil
        where TConcrete : class, TInterface;

	/// <summary>
	/// Adds a rate limiter to the MangaDex API client.
	/// </summary>
	/// <typeparam name="T">The type of rate limiter</typeparam>
	/// <param name="transient">Whether or not to register the service as a transient or singleton</param>
	/// <returns>The current builder for fluent method chaining</returns>
	IMangaDexBuilder WithRatelimiter<T>(bool transient = false) where T : class, IMdRateLimiter;
}

internal class MangaDexBuilder(IServiceCollection _services) : IMangaDexBuilder
{
    public void Build()
    {
        bool IsRegistered<TService>() => _services.Any(s => s.ServiceType == typeof(TService));

		void AddBaseServices()
        {
            _services
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
                .AddTransient<IMangaDexStatisticsService, MangaDexStatisticsService>()

                .AddTransient<IMangaDexMiscService, MangaDexMiscService>()
                .AddTransient<IMangaDexPageService, MangaDexMiscService>()
                .AddTransient<IMangaDexRatingService, MangaDexMiscService>()
                .AddTransient<IMangaDexThreadsService, MangaDexMiscService>()
                .AddTransient<IMangaDexCaptchaService, MangaDexMiscService>()
                .AddTransient<IMangaDexTakedownService, MangaDexMiscService>()

                .AddTransient<IMangaDexAuthService, MangaDexAuthService>()
                .AddTransient<IMangaDexApiClientService, MangaDexApiClientService>()
                .AddTransient<IOIDCService, OIDCService>();
        }

        void AddDefaultApiConfig()
        {
            if (IsRegistered<IConfigurationApi>()) return;

            _services.AddSingleton(p =>
            {
                var config = p.GetService<IConfiguration>();
                return config is null
                    ? ConfigurationApi.FromHardCoded()
                    : ConfigurationApi.FromConfiguration(config);
            });
        }

        void AddDefaultOIDCConfig()
        {
            if (IsRegistered<IConfigurationOIDC>()) return;

            _services.AddSingleton(p =>
            {
                var config = p.GetService<IConfiguration>();
                return config is null
                    ? ConfigurationOIDC.FromHardCoded()
                    : ConfigurationOIDC.FromConfiguration(config);
            });
        }

        void AddDefaultCredentials()
        {
            if (IsRegistered<ICredentialsService>()) return;
            _services.AddSingleton<ICredentialsService, PersonalCredentialsService>();
        }

        void AddDefaultRateLimiter()
		{
			if (IsRegistered<IMdRateLimiter>()) return;
			_services.AddSingleton<IMdRateLimiter, MdRateLimiter>();
		}

		AddBaseServices();
        AddDefaultApiConfig();
        AddDefaultOIDCConfig();
        AddDefaultCredentials();
        AddDefaultRateLimiter();
    }

    public IMangaDexBuilder WithAccessToken(string token)
    {
        return WithCredentials(new HardCodedCredentialsService(token));
    }

    public IMangaDexBuilder WithAccessTokenFromConfig()
    {
        return WithCredentials<ConfigurationCredentialsService>();
    }

    public IMangaDexBuilder WithApiConfig(IConfigurationApi config)
    {
        _services.AddSingleton(config);
        return this;
    }

    public IMangaDexBuilder WithApiConfig(
        string? apiUrl, string? userAgent, 
        bool throwOnError, bool rateLimits, 
        bool conservative)
    {
        return WithApiConfig(c =>
        {
            c.WithApiUrl(apiUrl)
             .WithUserAgent(userAgent)
             .WithAutoRateLimits(rateLimits, conservative);
            if (throwOnError)
                c.ThrowExceptionOnError();
            else
                c.FailGracefully();
        });
    }

    public IMangaDexBuilder WithApiConfig(Action<MangaDexApiConfigBuilder> config)
    {
        var bob = new MangaDexApiConfigBuilder();
        config(bob);
        return WithApiConfig(bob.Build());
    }

    public IMangaDexBuilder WithApiConfig<T>(bool transient = true) where T : class, IConfigurationApi
    {
        if (transient)
            _services.AddTransient<IConfigurationApi, T>();
        else
            _services.AddSingleton<IConfigurationApi, T>();

        return this;
    }

    public IMangaDexBuilder WithAuthConfig<T>(bool transient = true) where T : class, IConfigurationOIDC
    {
        if (transient)
            _services.AddTransient<IConfigurationOIDC, T>();
        else
            _services.AddSingleton<IConfigurationOIDC, T>();
        return this;
    }

    public IMangaDexBuilder WithAuthConfig(IConfigurationOIDC config)
    {
        _services.AddSingleton(config);
        return this;
    }

    public IMangaDexBuilder WithAuthConfig(
        string clientId, string clientSecret, 
        string username, string password, 
        string? authUrl = null, string? realmPath = null)
    {
        return WithAuthConfig(c => 
            c.WithClientId(clientId)
             .WithClientSecret(clientSecret)
             .WithUsername(username)
             .WithPassword(password)
             .WithAuthUrl(authUrl)
             .WithRealmPath(realmPath));
    }

    public IMangaDexBuilder WithAuthConfig(Action<MangaDexOIDCConfigBuilder> config)
    {
        var bob = new MangaDexOIDCConfigBuilder();
        config(bob);
        return WithAuthConfig(bob.Build());
    }

    public IMangaDexBuilder WithCredentials<T>(bool transient = true) where T : class, ICredentialsService
    {
        if (transient)
            _services.AddTransient<ICredentialsService, T>();
        else
            _services.AddSingleton<ICredentialsService, T>();
        return this;
    }

    public IMangaDexBuilder WithCredentials(ICredentialsService credentials)
    {
        return WithCredentials(_ => credentials);
    }

    public IMangaDexBuilder WithCredentials(Func<IServiceProvider, ICredentialsService> config)
    {
        _services.AddSingleton(config);
        return this;
    }

    public IMangaDexBuilder WithAccessTokenCaching<T>(bool transient = true) where T : class, ITokenCacheService
    {
        if (transient)
            _services.AddTransient<ITokenCacheService, T>();
        else
            _services.AddSingleton<ITokenCacheService, T>();
        return this;
    }

    public IMangaDexBuilder WithEventTracking<T>(bool transient) where T : class, IMdEventService
    {
        if (transient)
            _services.AddTransient<IMdEventService, T>();
        else
            _services.AddSingleton<IMdEventService, T>();
        return this;
    }

    public IMangaDexBuilder WithUtility<TInterface, TConcrete>()
        where TInterface : class, IMdUtil
        where TConcrete : class, TInterface
    {
        _services.AddTransient<TInterface, TConcrete>();
        return this;
    }

	public IMangaDexBuilder WithRatelimiter<T>(bool transient) where T : class, IMdRateLimiter
	{
		if (transient)
			_services.AddTransient<IMdRateLimiter, T>();
		else
			_services.AddSingleton<IMdRateLimiter, T>();
        return this;
	}
}
