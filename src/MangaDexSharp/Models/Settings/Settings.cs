namespace MangaDexSharp;

/// <summary>
/// Represents a MangaDex settings template
/// </summary>
public class SettingsTemplate : MangaDexRoot
{
	/// <summary>
	/// The JSON schema for MangaDex user settings
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, JsonElement> Schema { get; set; } = [];
}

/// <summary>
/// Represents a user's MangaDex settings
/// </summary>
public class UserSettings : MangaDexRoot
{
	/// <summary>
	/// The last time the settings were updated
	/// </summary>
	[JsonPropertyName("updatedAt")]
	public DateTime? UpdatedAt { get; set; }

	/// <summary>
	/// The settings JSON object
	/// </summary>
	[JsonPropertyName("settings")]
	public JsonElement Settings { get; set; }

	/// <summary>
	/// The settings template UUID used to validate the settings
	/// </summary>
	[JsonPropertyName("template")]
	public string Template { get; set; } = string.Empty;
}

/// <summary>
/// Represents a request to update a user's MangaDex settings
/// </summary>
public class UserSettingsUpdate
{
	/// <summary>
	/// The settings JSON object
	/// </summary>
	[JsonPropertyName("settings")]
	public JsonElement Settings { get; set; }

	/// <summary>
	/// The last time the settings were updated
	/// </summary>
	[JsonPropertyName("updatedAt")]
	public DateTime? UpdatedAt { get; set; }
}
