namespace MangaDexSharp;

public class MangaDexRoot
{
	[JsonPropertyName("result")]
	public string Result { get; set; } = string.Empty;

	[JsonPropertyName("errors")]
	public MangaDexError[] Errors { get; set; } = Array.Empty<MangaDexError>();
}

public class MangaDexRoot<T> : MangaDexRoot where T : new()
{
	[JsonPropertyName("response")]
	public string Response { get; set; } = string.Empty;

	[JsonPropertyName("data")]
	public T Data { get; set; } = new();
}

public class MangaDexError
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	[JsonPropertyName("status")]
	public int Status { get; set; }

	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	[JsonPropertyName("detail")]
	public string Detail { get; set; } = string.Empty;
}
