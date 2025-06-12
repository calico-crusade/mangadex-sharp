namespace MangaDexSharp.Utilities.Upload;

/// <summary>
/// Service for starting upload sessions
/// </summary>
public interface IUploadUtilityService : IMdUtil
{
    /// <summary>
    /// Closes/abandons any existing upload session
    /// </summary>
    /// <param name="config">The settings for the upload session</param>
    Task CloseExisting(Action<IUploadSettings>? config = null);

    /// <summary>
    /// Creates a new instance of the upload session
    /// </summary>
    /// <param name="manga">The manga to create the session for</param>
    /// <param name="groups">The groups that are uploading the manga</param>
    /// <param name="config">The settings for the upload session</param>
    /// <returns>The instance of the upload utility</returns>
    Task<IUploadInstance> New(string manga, string[] groups, Action<IUploadSettings>? config = null);

    /// <summary>
    /// Fetches and continues an existing upload session
    /// </summary>
    /// <param name="config">The settings for the upload session</param>
    /// <returns>The instance of the upload utility</returns>
    Task<IUploadInstance> Continue(Action<IUploadSettings>? config = null);

    /// <summary>
    /// Creates an upload session for editing a chapter
    /// </summary>
    /// <param name="chapterId">The ID of the chapter to use</param>
    /// <param name="version">The version number of the chapter</param>
    /// <param name="config">The settings for the upload session</param>
    /// <returns>The instance of the upload utility</returns>
    Task<IUploadInstance> Edit(string chapterId, int version, Action<IUploadSettings>? config = null);

    /// <summary>
    /// Creates an upload session for editing a chapter
    /// </summary>
    /// <param name="chapter">The chapter to edit</param>
    /// <param name="config">The settings for the upload session</param>
    /// <returns>The instance of the upload utility</returns>
    Task<IUploadInstance> Edit(Chapter chapter, Action<IUploadSettings>? config = null);
}

internal class UploadUtilityService(
    IRateLimitService _rates,
    IMdJsonService _json,
    ILogger<UploadUtilityService> _logger) : IUploadUtilityService
{
    /// <summary>
    /// Makes a request to the MangaDex API with the given settings and returns the result
    /// </summary>
    /// <typeparam name="T">The type being fetched</typeparam>
    /// <param name="request">The request to make</param>
    /// <param name="settings">The upload settings for the request</param>
    /// <returns>The returned results</returns>
    public Task<T> MakeRequest<T>(Func<string?, IMangaDex, Task<T>> request, UploadSettings settings)
        where T : MangaDexRoot, new()
    {
        return _rates.Request(async (api) =>
        {
            var token = await settings.GetAuthToken();
            return await request(token, api);
        }, settings.Token);
    }

    /// <summary>
    /// Creates/gets an upload session for the given request
    /// </summary>
    /// <param name="request">The request that makes or gets the session</param>
    /// <param name="config">The settings for the upload session</param>
    /// <returns>The instance of the upload utility</returns>
    public async Task<IUploadInstance> UploadSession(
        Func<string?, IMangaDex, Task<MangaDexRoot<UploadSession>>> request,
        Action<IUploadSettings>? config)
    {
        //Initialize the settings and limits
        var settings = new UploadSettings();
        config?.Invoke(settings);
        //Create / get the upload session
        var session = await MakeRequest(request, settings);
        //Throw an error if the session is invalid
        session.ThrowIfError();
        //Get the files from the session
        var files = session.Data.Relationship<UploadSessionFile>().ToList();
        //Return the instance of the upload
        var instance = new UploadInstance(settings, session.Data, files, _rates);
        instance.InitReader();
        settings.SetInstance(instance);
        return instance;
    }

    /// <inheritdoc />
    public async Task CloseExisting(Action<IUploadSettings>? config = null)
    {
        //Initialize the settings and limits
        var settings = new UploadSettings();
        config?.Invoke(settings);
        //Fetch the existing upload session
        var session = await MakeRequest((token, api) => api.Upload.Get(null, token), settings);
        if (session.IsError())
        {
            _logger.LogInformation("No existing sessions detected! {data}", _json.Pretty(session));
            return;
        }

        //Close the existing sessions
        _logger.LogInformation("Found existing session: {data}", _json.Pretty(session));
        await MakeRequest((token, api) => api.Upload.Abandon(session.Data.Id), settings);
        _logger.LogInformation("Abandoned session: {id}", session.Data.Id);
    }

    /// <inheritdoc />
    public Task<IUploadInstance> New(string manga, string[] groups, Action<IUploadSettings>? config = null)
    {
        return UploadSession((token, api) => api.Upload.Begin(manga, groups, token), config);
    }

    /// <inheritdoc />
    public Task<IUploadInstance> Continue(Action<IUploadSettings>? config = null)
    {
        return UploadSession((token, api) => api.Upload.Get(null, token), config);
    }

    /// <inheritdoc />
    public Task<IUploadInstance> Edit(string chapterId, int version, Action<IUploadSettings>? config = null)
    {
        return UploadSession((token, api) => api.Upload.EditChapter(chapterId, version, null, token), config);
    }

    /// <inheritdoc />
    public Task<IUploadInstance> Edit(Chapter chapter, Action<IUploadSettings>? config = null)
    {
        return Edit(chapter.Id, (chapter.Attributes?.Version ?? 1) + 1, config);
    }
}
