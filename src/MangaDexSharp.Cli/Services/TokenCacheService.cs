namespace MangaDexSharp.Cli.Services;

public interface ITokenCacheService
{
    Task<TokenData?> Get();

    Task Set(TokenData data);
}

internal class TokenCacheService(
    IMdJsonService _json) : ITokenCacheService
{
    private const string CREDS_FILE_PATH = "personal.json";

    public async Task<TokenData?> Get()
    {
        if (!File.Exists(CREDS_FILE_PATH)) return null;

        var json = await File.ReadAllTextAsync(CREDS_FILE_PATH);
        return _json.Deserialize<TokenData>(json);
    }

    public async Task Set(TokenData data)
    {
        using var fs = File.Create(CREDS_FILE_PATH);
        await _json.Serialize(data, fs);
        await fs.FlushAsync();
    }
}
