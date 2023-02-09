namespace MangaDexSharp;

public class MangaDexRoot
{
	[JsonPropertyName("result")]
	public string Result { get; set; } = string.Empty;
}

public class MangaDexRoot<T> : MangaDexRoot where T : new()
{
	[JsonPropertyName("response")]
	public string Response { get; set; } = string.Empty;

	[JsonPropertyName("data")]
	public T Data { get; set; } = new();
}
