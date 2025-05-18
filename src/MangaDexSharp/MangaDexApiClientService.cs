namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests related to API Clients
/// </summary>
public interface IMangaDexApiClientService
{
    /// <summary>
    /// Gets a list of the user's API clients
    /// </summary>
    /// <param name="filter">The filter criteria for the request</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The list of API clients</returns>
    Task<ApiClientList> Mine(ApiClientFilter? filter = null, string? token = null);

    /// <summary>
    /// Requests all of the <see cref="ApiClient"/>s that match the filter
    /// </summary>
    /// <param name="filter">How to filter the API Clients</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <param name="delay">How many milliseconds to wait after hitting the rate-cap</param>
    /// <param name="rateCap">How many requests to do before waiting</param>
    /// <returns>A list of all API clients</returns>
    IAsyncEnumerable<ApiClient> MineAll(ApiClientFilter? filter = null, string? token = null, int? delay = null, int? rateCap = null);

    /// <summary>
    /// Create a new API client
    /// </summary>
    /// <param name="data">The API client data</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The result of the creation request</returns>
    Task<MangaDexRoot<ApiClient>> Create(ApiClientData data, string? token = null);

    /// <summary>
    /// Fetches the API client with the given ID
    /// </summary>
    /// <param name="id">The ID of the Api Client</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <param name="includes">What relationship options to include in the return results</param>
    /// <returns>The API client</returns>
    Task<MangaDexRoot<ApiClient>> Get(string id, string? token = null, ApiClientIncludes[]? includes = null);

    /// <summary>
    /// Updates the API client data
    /// </summary>
    /// <param name="id">The ID of the API client</param>
    /// <param name="data">The data to update</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The API client that was updated</returns>
    Task<MangaDexRoot<ApiClient>> Update(string id, ApiClientUpdateData data, string? token = null);

    /// <summary>
    /// Delete the API client with the given ID
    /// </summary>
    /// <param name="id">The ID of the API client</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <param name="version">The (optional) version of the API client </param>
    /// <returns>The result of the request</returns>
    Task<MangaDexRoot> Delete(string id, string? token = null, int? version = null);

    /// <summary>
    /// Gets the Client Secret for the given API client
    /// </summary>
    /// <param name="id">The ID of the API client</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The client secret</returns>
    Task<MangaDexStruct<string>> Secret(string id, string? token = null);

    /// <summary>
    /// Regenerates the Client Secret for the given API client
    /// </summary>
    /// <param name="id">The ID of the API client</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The client secret</returns>
    Task<MangaDexStruct<string>> Regenerate(string id, string? token = null);
}

internal class MangaDexApiClientService : IMangaDexApiClientService
{
    private readonly IMdApiService _api;

    public string Root => "client";

    public MangaDexApiClientService(IMdApiService api)
    {
        _api = api;
    }

    public async Task<ApiClientList> Mine(ApiClientFilter? filter = null, string? token = null)
    {
        var c = await _api.Auth(token);
        filter ??= new();
        return await _api.Get<ApiClientList>($"{Root}?{filter.BuildQuery()}", c) ?? new() { Result = "error" };
    }

    public IAsyncEnumerable<ApiClient> MineAll(ApiClientFilter? filter = null, string? token = null, int? delay = null, int? rateCap = null)
    {
        filter ??= new();
        var util = new PaginationUtility<ApiClientList, ApiClient, ApiClientFilter>(filter, (t, _) => Mine(t, token));
        if (delay != null) util.Delay = delay.Value;
        if (rateCap != null) util.Cap = rateCap.Value;
        return util.All();
    }

    public async Task<MangaDexRoot<ApiClient>> Create(ApiClientData data, string? token = null)
    {
        var c = await _api.Auth(token);
        return await _api.Post<MangaDexRoot<ApiClient>, ApiClientData>(Root, data, c) ?? new() { Result = "error" };
    }

    public async Task<MangaDexRoot<ApiClient>> Get(string id, string? token = null, ApiClientIncludes[]? includes = null)
    {
        includes ??= [ApiClientIncludes.creator];
        var bob = new FilterBuilder()
            .Add("includes", includes)
            .Build();
        var c = await _api.Auth(token);
        return await _api.Get<MangaDexRoot<ApiClient>>($"{Root}/{id}?{bob}", c) ?? new() { Result = "error" };
    }

    public async Task<MangaDexRoot<ApiClient>> Update(string id, ApiClientUpdateData data, string? token = null)
    {
        var c = await _api.Auth(token);
        return await _api.Post<MangaDexRoot<ApiClient>, ApiClientUpdateData>($"{Root}/{id}", data, c) ?? new() { Result = "error" };
    }

    public async Task<MangaDexRoot> Delete(string id, string? token = null, int? version = null)
    {
        var c = await _api.Auth(token);
        var bob = new FilterBuilder()
            .Add("version", version)
            .Build();
        return await _api.Delete<MangaDexRoot>($"{Root}/{id}?{bob}", c) ?? new() { Result = "error" };
    }

    public async Task<MangaDexStruct<string>> Secret(string id, string? token = null)
    {
        var c = await _api.Auth(token);
        return await _api.Get<MangaDexStruct<string>>($"{Root}/{id}/secret", c) ?? new() { Result = "error" };
    }

    public async Task<MangaDexStruct<string>> Regenerate(string id, string? token = null)
    {
        var c = await _api.Auth(token);
        return await _api.Post<MangaDexStruct<string>, object>($"{Root}/{id}/secret", new { }, c) ?? new() { Result = "error" };
    }
}
