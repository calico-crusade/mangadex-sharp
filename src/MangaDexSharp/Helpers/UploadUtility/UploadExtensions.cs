namespace MangaDexSharp;

using Helpers.UploadUtility;

/// <summary>
/// Extensions for the upload utilities
/// </summary>
public static class UploadExtensions
{
    /// <summary>
    /// Creates an instance of the given type with errors
    /// </summary>
    /// <typeparam name="T">The type of object to create</typeparam>
    /// <param name="message">The message to use for the error</param>
    /// <param name="detail">The optional detailed version of the error message</param>
    /// <param name="status">The optional status code of the error</param>
    /// <returns>The errored result</returns>
    internal static T CreateError<T>(string message, string? detail = null, int status = 500)
        where T : MangaDexRoot, new()
    {
        return new T
        {
            Result = MangaDexRoot.RESULT_ERROR,
            Errors = [ new MangaDexError
            {
                Status = status,
                Title = message,
                Detail = detail ?? message
            }]
        };
    }

    /// <summary>
    /// Returns the minimum delay for the given time span
    /// </summary>
    /// <param name="settings">The settings to use for the check</param>
    /// <param name="timeSpan">The timespan to use for the check</param>
    /// <returns>The higher of the two spans</returns>
    internal static TimeSpan MinDelay(UploadSettings settings, TimeSpan timeSpan)
    {
        if (timeSpan < settings.MinRetryTimeout)
            return settings.MinRetryTimeout;
        return timeSpan;
    }

    /// <summary>
    /// Ensures that the given rate limits are not limited
    /// </summary>
    /// <param name="limit">The rate limits to check</param>
    /// <param name="settings">The settings to use for handling the request</param>
    internal static async Task EnsureNotLimited(RateLimit? limit, UploadSettings settings)
    {
        //If the rate limits aren't set, skip it.
        if (limit is null ||
            !limit.HasRateLimits ||
            !limit.IsLimited ||
            limit.RetryPassed()) return;

        //Get the retry time
        var retry = limit.RetryAfter!.Value;
        //Calculate how long we have to wait for it to pass
        var span = MinDelay(settings, retry - DateTime.UtcNow);
        //No timeout? skip it
        if (span.TotalMilliseconds <= 0) return;
        //Send out the event if possible
        settings.RateLimit(limit, span);
        //Wait for the time to pass
        await Task.Delay(span, settings.Token);
        //Send out the event if possible
        settings.RateLimitPassed(limit);
    }

    /// <summary>
    /// Makes a request to the API that handles rate-limits and retries
    /// </summary>
    /// <typeparam name="T">The type of return result</typeparam>
    /// <param name="request">The request to make</param>
    /// <param name="limits">The current rate limits object</param>
    /// <param name="current">The current retry count</param>
    /// <param name="settings">The settings for the upload</param>
    /// <returns>The return result of the request</returns>
    internal static async Task<T> RateLimitRequest<T>(Func<string?, Task<T>> request, RateLimit? limits, int current, UploadSettings settings)
        where T : MangaDexRoot, new()
    {
        if (current > settings.MaxRetries)
            return CreateError<T>("Max retry count exceeded", $"Max retry count exceeded: {current}", 500);

        await EnsureNotLimited(limits, settings);
        var token = await settings.GetAuthToken();
        var result = await request(token);
        settings.ApiResponse(result);

        if (!result.ErrorOccurred) return result;

        var isTooMany = result.Errors.Any(e => e.Status == 429);
        var newLimits = result.RateLimit.HasRateLimits ? result.RateLimit : limits;
        if (isTooMany)
            return await RateLimitRequest(request, newLimits, current + 1, settings);

        return result;
    }

    /// <summary>
    /// Creates/gets an upload session for the given request
    /// </summary>
    /// <param name="api">The API to use to manage the session</param>
    /// <param name="request">The request that makes or gets the session</param>
    /// <param name="config">The settings for the upload session</param>
    /// <param name="initialLimits">The initial rate limits to use for requests</param>
    /// <returns>The instance of the upload utility</returns>
    internal static async Task<IUploadInstance> UploadSession(
        IMangaDex api, Func<string?, Task<MangaDexRoot<UploadSession>>> request,
        Action<IUploadSettings>? config, RateLimit? initialLimits)
    {
        //Initialize the settings and limits
        var settings = new UploadSettings();
        config?.Invoke(settings);
        var limits = initialLimits ?? new RateLimit();
        //Create / get the upload session
        var session = await RateLimitRequest(request, limits, 0, settings);
        //Throw an error if the session is invalid
        session.ThrowIfError();
        //Get the files from the session
        var files = session.Data.Relationship<UploadSessionFile>().ToList();
        //Update the limits based on the ones from the session request
        limits = session.RateLimit;
        //Return the instance of the upload
        var instance = new UploadInstance(settings, session.Data, files, limits, api);
        settings.SetInstance(instance);
        return instance;
    }

    /// <summary>
    /// Creates a new instance of the upload session
    /// </summary>
    /// <param name="api">The API to use to manage the session</param>
    /// <param name="manga">The manga to create the session for</param>
    /// <param name="groups">The groups that are uploading the manga</param>
    /// <param name="config">The settings for the upload session</param>
    /// <param name="initialLimits">The initial rate limits to use for requests</param>
    /// <returns>The instance of the upload utility</returns>
    public static Task<IUploadInstance> NewUploadSession(
        this IMangaDex api, string manga, string[] groups,
        Action<IUploadSettings>? config = null, RateLimit? initialLimits = null)
    {
        return UploadSession(api,
            token => api.Upload.Begin(manga, groups, token),
            config, initialLimits);
    }

    /// <summary>
    /// Fetches and continues an existing upload session
    /// </summary>
    /// <param name="api">The API to use to manage the session</param>
    /// <param name="config">The settings for the upload session</param>
    /// <param name="initialLimits">The initial rate limits to use for requests</param>
    /// <returns>The instance of the upload utility</returns>
    public static Task<IUploadInstance> ContinueUploadSession(this IMangaDex api,
        Action<IUploadSettings>? config = null, RateLimit? initialLimits = null)
    {
        return UploadSession(api,
            token => api.Upload.Get(null, token),
            config, initialLimits);
    }

    /// <summary>
    /// Creates an upload session for editing a chapter
    /// </summary>
    /// <param name="api">The API to use to manage the session</param>
    /// <param name="chapterId">The ID of the chapter to use</param>
    /// <param name="version">The version number of the chapter</param>
    /// <param name="config">The settings for the upload session</param>
    /// <param name="initialLimits">The initial rate limits to use for requests</param>
    /// <returns>The instance of the upload utility</returns>
    public static Task<IUploadInstance> EditChapterSession(
        this IMangaDex api, string chapterId, int version,
        Action<IUploadSettings>? config = null, RateLimit? initialLimits = null)
    {
        return UploadSession(api,
            token => api.Upload.EditChapter(chapterId, version, null, token),
            config, initialLimits);
    }
}
