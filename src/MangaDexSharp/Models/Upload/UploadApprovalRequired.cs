namespace MangaDexSharp;

/// <summary>
/// Represents a request to check whether an upload needs moderation approval
/// </summary>
public class UploadApprovalRequiredRequest
{
	/// <summary>
	/// The manga ID to check
	/// </summary>
	[JsonPropertyName("manga")]
	public string Manga { get; set; } = string.Empty;

	/// <summary>
	/// The translated language locale to check
	/// </summary>
	[JsonPropertyName("locale")]
	public string Locale { get; set; } = string.Empty;
}

/// <summary>
/// Represents whether an upload needs moderation approval
/// </summary>
public class UploadApprovalRequired : MangaDexRoot
{
	/// <summary>
	/// Whether or not moderation approval is required
	/// </summary>
	[JsonPropertyName("requiresApproval")]
	public bool RequiresApproval { get; set; }
}
