namespace MangaDexSharp;

public class CaptchaChallenge
{
	[JsonPropertyName("captchaChallenge")]
	public string Challenge { get; set; } = string.Empty;
}
