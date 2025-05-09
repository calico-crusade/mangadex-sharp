namespace MangaDexSharp.Cli.Verbs;

[Verb("example", HelpText = "An example of using the MD# api with Dependency Injection")]
public class ExampleVerbOptions { }

internal class ExampleVerb(
    IMangaDex _api, 
    ILogger<ExampleVerb> logger) 
        : BooleanVerb<ExampleVerbOptions>(logger)
{
    public override async Task<bool> Execute(ExampleVerbOptions opts, CancellationToken token)
    {
        await Manga();
        await Chapters();
        await Groups();
        //await OAuth2Login();
        //await LegacyLogin();
        return true;
    }

    public async Task Manga()
    {
        //Example of fetching a manga by ID
        var manga = await _api.Manga.Get("fc0a7b86-992e-4126-b30f-ca04811979bf");
        _logger.LogInformation("Title: {title}", manga.Data.Attributes?.Title.FirstOrDefault().Value);
    }

    public async Task Chapters()
    {
        //Example of fetching a chapter by ID
        var chapter = await _api.Chapter.Get("158f54ed-9983-406d-b882-208858288874");
        _logger.LogInformation("Chapter: {title}", chapter.Data.Attributes?.Title);

        //Example of using any of the ListAll endpoints
        //These are useful for fetching all of the results of a specific paginated request
        var allChaps = _api.Chapter.ListAll(new ChaptersFilter
        {
            Limit = 10,
            Offset = 0,
            Manga = "fc0a7b86-992e-4126-b30f-ca04811979bf"
        });

        await foreach (var chap in allChaps)
        {
            _logger.LogInformation("Chap: {chapter} - vol {volume} - {title}", chap.Attributes?.Chapter, chap.Attributes?.Volume, chap.Attributes?.Title);
        }
    }

    public async Task Groups()
    {
        //Fetch a group by name
        var group = await _api.ScanlationGroup.List(new ScanlationGroupFilter
        {
            Name = "europaisacoolmoon"
        });

        _logger.LogInformation("Group: {name}", group?.Data?.FirstOrDefault()?.Attributes?.Name);
    }

    public async Task OAuth2Login()
    {
        //Example of using the personal client auth method
        string clientId = "Your client ID from https://mangadex.org/settings",
               clientSecret = "Your client secret from https://mangadex.org/settings";

        string username = "Your account username for https://mangadex.org",
               password = "Your account password for https://mangadex.org";

        var auth = await _api.Auth.Personal(clientId, clientSecret, username, password);
        _logger.LogInformation("Token: {accessToken}", auth.AccessToken);

        var refresh = await _api.Auth.Refresh(auth.RefreshToken, clientId, clientSecret);
        _logger.LogInformation("Refreshed Token: {accessToken}", refresh.AccessToken);
    }

    [Obsolete]
    public async Task LegacyLogin()
    {
        string username = "Your account username for https://mangadex.org",
               password = "Your account password for https://mangadex.org";

        //Example of using the old login method to fetch the current user's token
        var result = await _api.User.Login(username, password); //While it says the method is obsolete, it will still work (for now)
        var token = result.Data.Session;

        //You can either pass the token to the authenticated endpoints
        var me = await _api.User.Me(token);
        //Or you can create a authenticated API
        var authedApi = MangaDex.Create(c => c.WithAccessToken(token));
        var me2 = await authedApi.User.Me();

        _logger.LogInformation("Login Success: {username}", me.Data.Attributes?.Username);
    }
}
