namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests related to MangaDex user settings
/// </summary>
public interface IMangaDexSettingsService
{
	/// <summary>
	/// Gets the latest user settings template
	/// </summary>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The latest settings template</returns>
	Task<SettingsTemplate> LatestTemplate(string? token = null);

	/// <summary>
	/// Creates a new user settings template
	/// </summary>
	/// <param name="template">The JSON schema to use as the settings template</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The created settings template</returns>
	Task<SettingsTemplate> CreateTemplate(JsonElement template, string? token = null);

	/// <summary>
	/// Gets a specific user settings template by version
	/// </summary>
	/// <param name="version">The template version UUID</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The requested settings template</returns>
	Task<SettingsTemplate> GetTemplate(string version, string? token = null);

	/// <summary>
	/// Gets the current user's settings
	/// </summary>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The current user's settings</returns>
	Task<UserSettings> Get(string? token = null);

	/// <summary>
	/// Creates or updates the current user's settings
	/// </summary>
	/// <param name="settings">The settings update request</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The updated user settings</returns>
	Task<UserSettings> Update(UserSettingsUpdate settings, string? token = null);
}

internal class MangaDexSettingsService(IMdApiService _api) : IMangaDexSettingsService
{
	public string Root => "settings";

	public async Task<SettingsTemplate> LatestTemplate(string? token = null)
	{
		var c = await _api.Auth(token, true);
		return await _api.Get<SettingsTemplate>($"{Root}/template", c) ?? new() { Result = "error" };
	}

	public async Task<SettingsTemplate> CreateTemplate(JsonElement template, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<SettingsTemplate, JsonElement>($"{Root}/template", template, c) ?? new() { Result = "error" };
	}

	public async Task<SettingsTemplate> GetTemplate(string version, string? token = null)
	{
		var c = await _api.Auth(token, true);
		return await _api.Get<SettingsTemplate>($"{Root}/template/{version}", c) ?? new() { Result = "error" };
	}

	public async Task<UserSettings> Get(string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Get<UserSettings>(Root, c) ?? new() { Result = "error" };
	}

	public async Task<UserSettings> Update(UserSettingsUpdate settings, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<UserSettings, UserSettingsUpdate>(Root, settings, c) ?? new() { Result = "error" };
	}
}
