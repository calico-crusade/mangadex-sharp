namespace MangaDexSharp;

/// <summary>
/// Represents the return results of almost all MD api endpoints
/// </summary>
public class MangaDexRoot
{
	/// <summary>
	/// A message from the API telling us whether a request was successful or not
	/// </summary>
	[JsonPropertyName("result")]
	public string Result { get; set; } = string.Empty;

	/// <summary>
	/// A collection of errors from the MD api
	/// </summary>
	[JsonPropertyName("errors")]
	public MangaDexError[] Errors { get; set; } = Array.Empty<MangaDexError>();
}

/// <summary>
/// Represents the return results of the MD api endpoints
/// </summary>
/// <typeparam name="T"></typeparam>
public class MangaDexRoot<T> : MangaDexRoot where T : new()
{
	/// <summary>
	/// A response code for the results
	/// </summary>
	[JsonPropertyName("response")]
	public string Response { get; set; } = string.Empty;

	/// <summary>
	/// The return results of the endpoint
	/// </summary>
	[JsonPropertyName("data")]
	public T Data { get; set; } = new();
}

/// <summary>
/// Represents an error returned by the MD api
/// </summary>
public class MangaDexError
{
	/// <summary>
	/// The ID of the error message (Can be used for debugging within #dev-talk-api)
	/// </summary>
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// The status code for the error
	/// </summary>
	[JsonPropertyName("status")]
	public int Status { get; set; }

	/// <summary>
	/// A brief description of the error
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// A more detailed description of the error
	/// </summary>
	[JsonPropertyName("detail")]
	public string Detail { get; set; } = string.Empty;
}
