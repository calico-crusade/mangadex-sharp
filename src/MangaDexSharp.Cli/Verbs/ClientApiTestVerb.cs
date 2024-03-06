namespace MangaDexSharp.Cli.Verbs;

[Verb("client-api-test", HelpText = "Test the client API endpoints")]
internal class ClientApiTestVerbOptions 
{
    public const string DEFAULT_NAME = "MD# Test Client - Ignore";
    public const string DEFAULT_DESC = "You can ignore this client. It's just a test and will be removed shortly.";

    [Option('n', "name", HelpText = "The name of the test client", Default = DEFAULT_NAME)]
    public string Name { get; set; } = DEFAULT_NAME;

    [Option('d', "description", HelpText = "The description of the test client", Default = DEFAULT_DESC)]
    public string Description { get; set; } = DEFAULT_DESC;
}

internal class ClientApiTestVerb(
    IMangaDex _api,
    ILogger<ClientApiTestVerb> logger) : BooleanVerb<ClientApiTestVerbOptions>(logger)
{
    public override async Task<bool> Execute(ClientApiTestVerbOptions options, CancellationToken token)
    {
        var client = await Create(options);
        if (client is null) return false;

        await Task.Delay(1000, token);

        client = await Fetch(client.Id);
        if (client is null) return false;

        await Task.Delay(1000, token);

        if (client.Attributes?.State == ApiClientState.approved ||
            client.Attributes?.State == ApiClientState.autoapproved)
        {
            var secret = await Secret(client.Id);
            if (secret is null) return false;
        }

        if (!await Delete(client.Id)) return false;

        await List();
        return true;
    }

    public async Task<ApiClient?> Create(ClientApiTestVerbOptions options)
    {
        var res = await _api.ApiClient.Create(new()
        {
            Name = options.Name,
            Description = options.Description
        });

        if (res.IsError(out string error, out var client))
        {
            _logger.LogError("Failed to create test client: {Error}", error);
            return null;
        }

        _logger.LogInformation("Test client created: [#{Id}] {Name} - {Description}\r\nType: {Profile}. Status: {Status}. Active: {Active}", 
            client.Id, client.Attributes?.Name, client.Attributes?.Description, client.Attributes?.Profile, client.Attributes?.State, client.Attributes?.Active);
        return client;
    }

    public async Task<ApiClient?> Fetch(string id)
    {
        var res = await _api.ApiClient.Get(id);
        if (res.IsError(out string error, out var client))
        {
            _logger.LogError("Failed to fetch client: {Error}", error);
            return null;
        }

        _logger.LogInformation("Fetched client: [#{Id}] {Name} - {Description}\r\nType: {Profile}. Status: {Status}. Active: {Active}", 
            client.Id, client.Attributes?.Name, client.Attributes?.Description, client.Attributes?.Profile, client.Attributes?.State, client.Attributes?.Active);
        return client;
    }

    public async Task<bool> Delete(string id)
    {
        var res = await _api.ApiClient.Delete(id);
        if (res.IsError(out string error))
        {
            _logger.LogError("Failed to delete client: {Error}", error);
            return false;
        }

        _logger.LogInformation("Deleted client: {Id}", id);
        return true;
    }

    public async Task<string?> Secret(string id)
    {
        var res = await _api.ApiClient.Secret(id);
        if (res.IsError(out string error, out var secret))
        {
            _logger.LogError("Failed to fetch client secret: {Error}", error);
            return null;
        }

        _logger.LogInformation("Fetched client secret: {Secret}", secret);
        return secret;
    }

    public async Task List()
    {
        var clients = _api.ApiClient.MineAll();

        await foreach(var client in clients)
        {
            _logger.LogInformation("Fetched client: [#{Id}] {Name} - {Description}\r\nType: {Profile}. Status: {Status}. Active: {Active}",
                client.Id, client.Attributes?.Name, client.Attributes?.Description, client.Attributes?.Profile, client.Attributes?.State, client.Attributes?.Active);
        }
    }
}
