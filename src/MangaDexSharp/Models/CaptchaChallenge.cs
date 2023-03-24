namespace MangaDexSharp;

/// <summary>
/// Represents a captcha challenge result
/// </summary>
public class CaptchaChallenge
{
	/// <summary>
	/// The contents of the captcha
	/// </summary>
	[JsonPropertyName("captchaChallenge")]
	public string Challenge { get; set; } = string.Empty;
}
