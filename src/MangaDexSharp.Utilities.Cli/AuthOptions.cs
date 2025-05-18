namespace MangaDexSharp.Utilities.Cli;

public class AuthOptions
{
    [Option('a', "access-token", HelpText = "Your account access token, if you have one. (You can get this from your browser's dev tools.)")]
    public string? AccessToken { get; set; }

    [Option('c', "client-id", HelpText = "The client ID to use for the request. (You can get this from: https://mangadex.org/settings > API Clients)")]
    public string? ClientId { get; set; }

    [Option('s', "client-secret", HelpText = "The client secret to use for the request. (You can get this from: https://mangadex.org/settings > API Clients)")]
    public string? ClientSecret { get; set; }

    [Option('u', "username", HelpText = "The username of your MangaDex account.")]
    public string? Username { get; set; }

    [Option('p', "password", HelpText = "The password of your MangaDex account.")]
    public string? Password { get; set; }
}
