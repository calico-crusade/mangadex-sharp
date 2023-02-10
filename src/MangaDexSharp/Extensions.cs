using System.Net.Http;

namespace MangaDexSharp;

public static class Extensions
{
	public const string API_ROOT = "https://api.mangadex.org";

	public static ContentRating[] ContentRatingsAll => new[]
	{
		ContentRating.safe,
		ContentRating.erotica,
		ContentRating.suggestive,
		ContentRating.pornographic
	};

	public static async Task<Action<HttpRequestMessage>> Auth(string? token, ICredentialsService creds)
	{
		token ??= await creds.GetToken();
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

	public static ScanlationGroupRelationship[] ScanlationGroups(this IRelationshipModel? source) => source.Relationship<ScanlationGroupRelationship>();

	public static UserRelationship[] Users(this IRelationshipModel? source)  => source.Relationship<UserRelationship>();

	private static IServiceCollection AddBaseMangaDex(this IServiceCollection services)
	{
		return services
			.AddCardboardHttp()
			.AddTransient<IMangaDex, MangaDex>()
			.AddTransient<IMangaDexMangaService, MangaDexMangaService>()
			.AddTransient<IMangaDexChapterService, MangaDexChapterService>()
			.AddTransient<IMangaDexPagesService, MangaDexPagesService>()
			.AddTransient<IMangaDexAuthorService, MangaDexAuthorService>()
			.AddTransient<IMangaDexCoverArtService, MangaDexCoverArtService>();
	}

	public static IServiceCollection AddMangaDex(this IServiceCollection services)
	{
		return services.AddMangaDex<ConfigurationCredentialsService>();
	}

	public static IServiceCollection AddMangaDex(this IServiceCollection services, string token)
	{
		return services
			.AddMangaDex(new HardCodedCredentialsService(token));
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