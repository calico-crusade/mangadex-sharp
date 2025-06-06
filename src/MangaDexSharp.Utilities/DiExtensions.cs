namespace MangaDexSharp;

using Utilities;
using Utilities.Upload;

/// <summary>
/// Extensions for dependency injection in MangaDexSharp
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Adds all of the utility services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add to</param>
    /// <returns>The services</returns>
    public static IServiceCollection AddMangaDexUtils(this IServiceCollection services)
    {
        return services
            .AddSingleton<IRateLimitService, RateLimitService>()
            .AddTransient<IUploadUtilityService, UploadUtilityService>();
    }

    /// <summary>
    /// Adds all of the utility services to the MangaDex builder.
    /// </summary>
    /// <param name="builder">The builder to add to</param>
    /// <returns>The builder for method chaining</returns>
    public static IMangaDexBuilder AddMangaDexUtils(this IMangaDexBuilder builder)
    {
        return builder
            .WithUtility<IRateLimitService, RateLimitService>()
            .WithUtility<IUploadUtilityService, UploadUtilityService>();
    }
}
