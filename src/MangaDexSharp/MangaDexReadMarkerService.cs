namespace MangaDexSharp;

/// <summary>
/// Represents all requests related to read status markers
/// </summary>
public interface IMangaDexReadMarkerService
{
	/// <summary>
	/// Requests a list of all of the read chapters of the given manga for the current user
	/// </summary>
	/// <param name="mangaId">The ID of the manga</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of read chapters</returns>
	Task<ReadMarkerList> Read(string mangaId, string? token = null);

	/// <summary>
	/// Requests a list of all of the read chapters of the given manga for the current user
	/// </summary>
	/// <param name="mangaIds">The IDs of the manga</param>
	/// <param name="grouped">Whether to group the results by manga or not</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of the read chapters</returns>
	Task<ReadMarkerList> Read(string[] mangaIds, bool grouped = true, string? token = null);

	/// <summary>
	/// Requests to mark all of the give chapters as read or unread for the given manga
	/// </summary>
	/// <param name="mangaId">The ID of the manga</param>
	/// <param name="chapterIds">The IDs of the chapters</param>
	/// <param name="read">Whether or not to mark the chapters are read or undread</param>
	/// <param name="updateHistory">Whether or not to update the read history</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> BatchUpdate(string mangaId, string[] chapterIds, bool read, bool updateHistory = true, string? token = null);

	/// <summary>
	/// Requests a batch update of the read markers for the given manga
	/// </summary>
	/// <param name="mangaId">The ID of the manga</param>
	/// <param name="update">The update to request</param>
	/// <param name="updateHistory">Whether or not to update the read history</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> BatchUpdate(string mangaId, ReadMarkerBatchUpdate update, bool updateHistory = true, string? token = null);
}

internal class MangaDexReadMarkerService : IMangaDexReadMarkerService
{
	private readonly IMdApiService _api;

	public MangaDexReadMarkerService(IMdApiService api)
	{
		_api = api;
	}

	public async Task<ReadMarkerList> Read(string mangaId, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Get<ReadMarkerList>($"manga/{mangaId}/read", c) ?? new() { Result = "error" };
	}

	public async Task<ReadMarkerList> Read(string[] mangaIds, bool grouped = true, string? token = null)
	{
		var c = await _api.Auth(token);
		var bob = new FilterBuilder()
			.Add("ids", mangaIds)
			.Add("grouped", grouped)
			.Build();
		var url = $"manga/read?{bob}";
		return await _api.Get<ReadMarkerList>(url, c) ?? new() { Result = "error" };
	}

	public Task<MangaDexRoot> BatchUpdate(string mangaId, string[] chapterIds, bool read, bool updateHistory = true, string? token = null)
	{
		var d = new ReadMarkerBatchUpdate
		{
			ChapterIdsRead = read ? chapterIds : Array.Empty<string>(),
			ChapterIdsUnread = read ? Array.Empty<string>() : chapterIds
		};
		return BatchUpdate(mangaId, d, updateHistory, token);
	}

	public async Task<MangaDexRoot> BatchUpdate(string mangaId, ReadMarkerBatchUpdate update, bool updateHistory = true, string? token = null)
	{
		var c = await _api.Auth(token);
		var url = $"manga/{mangaId}/read?updateHistory={updateHistory}";
		return await _api.Post<MangaDexRoot, ReadMarkerBatchUpdate>(url, update, c) ?? new() { Result = "error" };
	}
}
