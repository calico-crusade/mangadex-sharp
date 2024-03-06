namespace MangaDexSharp.Cli;

public class TokenData : TokenResult
{
    [JsonPropertyName("executed")]
    public DateTime Executed { get; set; }

    [JsonIgnore]
    public bool AccessExpired => Executed.AddSeconds(ExpiresIn ?? 0) < DateTime.UtcNow;

    [JsonIgnore]
    public bool RefreshExpired => Executed.AddSeconds(RefreshExpiresIn ?? 0) < DateTime.UtcNow;

    public static TokenData From(TokenResult result)
    {
        return new()
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken,
            ExpiresIn = result.ExpiresIn,
            RefreshExpiresIn = result.RefreshExpiresIn,
            TokenType = result.TokenType,
            NotBeforePolicy = result.NotBeforePolicy,
            SessionState = result.SessionState,
            Scope = result.Scope,
            ClientType = result.ClientType,
            Executed = DateTime.UtcNow
        };
    }
}
