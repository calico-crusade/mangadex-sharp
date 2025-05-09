
namespace MangaDexSharp.Cli.Services;

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

    public async Task<(TokenResult? result, DateTime? executed)> GetCache()
    {
        if (!File.Exists(CREDS_FILE_PATH)) return (null, null);

        var data = await Get();
        return (data, data?.Executed);
    }

    public async Task Set(TokenData data)
    {
        using var fs = File.Create(CREDS_FILE_PATH);
        await _json.Serialize(data, fs);
        await fs.FlushAsync();
    }

    public async Task SetCache(TokenResult? result, DateTime? executed)
    {
        if (result is null) return;

        var data = TokenData.From(result);
        if (executed.HasValue)
            data.Executed = executed.Value;
        await Set(data);
    }
}
