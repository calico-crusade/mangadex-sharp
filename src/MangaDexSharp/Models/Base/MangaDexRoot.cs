namespace MangaDexSharp;

/// <summary>
/// Represents the return results of almost all MD api endpoints
/// </summary>
public class MangaDexRoot
{
	/// <summary>
	/// The result of the request was successful
	/// </summary>
	public const string RESULT_OK = "ok";
	
	/// <summary>
	/// The result of the request was an error
	/// </summary>
	public const string RESULT_ERROR = "error";

	/// <summary>
	/// The result of the request was a failure
	/// </summary>
	public const string RESULT_KO = "ko";

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

	/// <summary>
	/// This is a shorthand for <see cref="MangaDexRoot.Result"/> == "error"
	/// </summary>
	[JsonIgnore]
	public bool ErrorOccurred => Result.ToLower().Trim() == "error";
}
