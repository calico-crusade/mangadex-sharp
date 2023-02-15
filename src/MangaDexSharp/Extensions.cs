using System.Net.Http;

namespace MangaDexSharp;

public static class Extensions
{
	public const string API_ROOT = "https://api.mangadex.org";
	public const string API_DEV_ROOT = "https://api.mangadex.dev";

	public static ContentRating[] ContentRatingsAll => new[]
	{
		ContentRating.safe,
		ContentRating.erotica,
		ContentRating.suggestive,
		ContentRating.pornographic
	};

	public static async Task<Action<HttpRequestMessage>> Auth(string? token, ICredentialsService creds, bool optional = false)
	{
		token ??= await creds.GetToken();
		if (string.IsNullOrEmpty(token) && optional) return c => { };

		if (string.IsNullOrEmpty(token))
			throw new ArgumentException("No token provided by credentials service", nameof(token));

		return c => c.Headers.Add("Authorization", "Bearer " + token);
	}

	public static T[] Relationship<T>(this IRelationshipModel? source) where T: IRelationship
	{
		return source?
			.Relationships
			.Where(t => t is T)
			.Select(t => (T)t)
			.ToArray() ?? Array.Empty<T>();
	}

	public static CoverArtRelationship[] CoverArt(this IRelationshipModel? source) => source.Relationship<CoverArtRelationship>();

	public static PersonRelationship[] People(this IRelationshipModel? source) => source.Relationship<PersonRelationship>();

	public static RelatedDataRelationship[] Manga(this IRelationshipModel? source) => source.Relationship<RelatedDataRelationship>();

	public static ScanlationGroup[] ScanlationGroups(this IRelationshipModel? source) => source.Relationship<ScanlationGroup>();

	public static User[] Users(this IRelationshipModel? source)  => source.Relationship<User>();

	private static IServiceCollection AddBaseMangaDex(this IServiceCollection services)
	{
		return services
			.AddCardboardHttp()
			.AddTransient<IMangaDex, MangaDex>()

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

	public static IServiceCollection AddMangaDex(this IServiceCollection services)
	{
		return services.AddMangaDex<ConfigurationCredentialsService>();
	}

	public static IServiceCollection AddMangaDex(this IServiceCollection services, string token, string? apiUrl = null)
	{
		return services
			.AddMangaDex(new HardCodedCredentialsService(token, apiUrl));
	}

	public static IServiceCollection AddMangaDex<T>(this IServiceCollection services) where T: class, ICredentialsService
	{
		return services
			.AddBaseMangaDex()
			.AddTransient<ICredentialsService, T>();
	}

	public static IServiceCollection AddMangaDex<T>(this IServiceCollection services, T instance) where T : ICredentialsService
	{
		return services
			.AddBaseMangaDex()
			.AddSingleton<ICredentialsService>(instance);
	}
}