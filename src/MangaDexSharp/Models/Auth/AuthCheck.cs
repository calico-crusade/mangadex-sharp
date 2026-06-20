namespace MangaDexSharp;

/// <summary>
/// Represents the permissions associated with an access token
/// </summary>
public class AuthCheck : MangaDexRoot
{
	/// <summary>
	/// Whether or not the access token is authenticated
	/// </summary>
	[JsonPropertyName("isAuthenticated")]
	public bool IsAuthenticated { get; set; }

	/// <summary>
	/// All permissions attached to the access token
	/// </summary>
	[JsonPropertyName("permissions")]
	public string[] Permissions { get; set; } = [];

	/// <summary>
	/// All roles attached to the access token
	/// </summary>
	[JsonPropertyName("roles")]
	public string[] Roles { get; set; } = [];
}

/// <summary>
/// Represents the result of logging out of the MangaDex API
/// </summary>
public class AuthLogout : MangaDexRoot { }
