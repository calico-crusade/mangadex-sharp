namespace MangaDexSharp;

public interface ICredentialsService
{
	string ApiUrl { get; }
	Task<string> GetToken();
}

public class ConfigurationCredentialsService : ICredentialsService
{
	private readonly IConfiguration _config;

	public static string TokenPath { get; set; } = "Mangadex:Token";
	public static string ApiPath { get; set; } = "Mangadex:ApiUrl";

	public string Token => _config[TokenPath];

	public string ApiUrl => string.IsNullOrEmpty(_config[ApiPath]) ? API_ROOT : _config[ApiPath];

	public ConfigurationCredentialsService(IConfiguration config)
	{
		_config = config;
	}

	public Task<string> GetToken()
	{
		return Task.FromResult(Token);
	}
}

public class HardCodedCredentialsService : ICredentialsService
{
	public string Token { get; set; }

	public string ApiUrl { get; set; }

	public HardCodedCredentialsService(string token, string? apiUrl = null)
	{
		Token = token;
		ApiUrl = apiUrl ?? API_ROOT;
	}

	public Task<string> GetToken()
	{
		return Task.FromResult(Token);
	}
}
