namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests on the /statistics endpoints
/// </summary>
public interface IMangaDexStatisticsService
{
    /// <summary>
    /// Gets the statistics for the given chapter
    /// </summary>
    /// <param name="chapterId">The ID of the chapter</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The chapter statistics</returns>
    Task<CommentStatistics> Chapter(string chapterId, string? token = null);

    /// <summary>
    /// Gets the statistics for the given chapters
    /// </summary>
    /// <param name="chapterIds">The IDs of the chapters</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The chapters statistics</returns>
    Task<CommentStatistics> Chapters(string[] chapterIds, string? token = null);

    /// <summary>
    /// Gets the statistics for the given scanlation group
    /// </summary>
    /// <param name="groupId">The ID of the group</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The group statistics</returns>
    Task<CommentStatistics> ScanlationGroup(string groupId, string? token = null);

    /// <summary>
    /// Gets the statistics for the given scanlation groups
    /// </summary>
    /// <param name="groupIds">The IDs of the groups</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The group statistics</returns>
    Task<CommentStatistics> ScanlationGroups(string[] groupIds, string? token = null);

    /// <summary>
    /// Gets the statistics for the given manga
    /// </summary>
    /// <param name="mangaId">The ID of the manga</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The group statistics</returns>
    Task<MangaStatistics> Manga(string mangaId, string? token = null);

    /// <summary>
    /// Gets the statistics for the given manga
    /// </summary>
    /// <param name="mangaIds">The IDs of the manga</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The group statistics</returns>
    Task<MangaStatistics> Manga(string[] mangaIds, string? token = null);
}

internal class MangaDexStatisticsService(
    IMdApiService _api) : IMangaDexStatisticsService
{
    public string Root => "statistics";

    public async Task<CommentStatistics> Chapter(string chapterId, string? token = null)
    {
        var c = await _api.Auth(token, true);
        return await _api.Get<CommentStatistics>($"{Root}/chapter/{chapterId}", c) ?? new() { Result = "error" };
    }

    public async Task<CommentStatistics> Chapters(string[] chapterIds, string? token = null)
    {
        var c = await _api.Auth(token, true);
        var pars = string.Join("&", chapterIds.Select(id => $"chapter[]={id}"));
        return await _api.Get<CommentStatistics>($"{Root}/chapter?{pars}", c) ?? new() { Result = "error" };
    }

    public async Task<CommentStatistics> ScanlationGroup(string groupId, string? token = null)
    {
        var c = await _api.Auth(token, true);
        return await _api.Get<CommentStatistics>($"{Root}/group/{groupId}", c) ?? new() { Result = "error" };
    }

    public async Task<CommentStatistics> ScanlationGroups(string[] groupIds, string? token = null)
    {
        var c = await _api.Auth(token, true);
        var pars = string.Join("&", groupIds.Select(id => $"group[]={id}"));
        return await _api.Get<CommentStatistics>($"{Root}/group?{pars}", c) ?? new() { Result = "error" };
    }

    public async Task<MangaStatistics> Manga(string mangaId, string? token = null)
    {
        var c = await _api.Auth(token, true);
        return await _api.Get<MangaStatistics>($"{Root}/manga/{mangaId}", c) ?? new() { Result = "error" };
    }

    public async Task<MangaStatistics> Manga(string[] mangaIds, string? token = null)
    {
        var c = await _api.Auth(token, true);
        var pars = string.Join("&", mangaIds.Select(id => $"manga[]={id}"));
        return await _api.Get<MangaStatistics>($"{Root}/manga?{pars}", c) ?? new() { Result = "error" };
    }
}
