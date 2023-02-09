namespace MangaDexSharp;

public interface ICredentialsService
{
	Task<string> GetToken();
}

public class ConfigurationCredentialsService : ICredentialsService
{
	private readonly IConfiguration _config;

	public static string TokenPath { get; set; } = "Mangadex:Token";

	public string Token => _config[TokenPath];

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

	public HardCodedCredentialsService(string token)
	{
		Token = token;
	}

	public Task<string> GetToken()
	{
		return Task.FromResult(Token);
	}
}
