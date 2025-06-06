namespace MangaDexSharp.Utilities.Upload;

/// <summary>
/// An interface for editing upload settings
/// </summary>
public interface IUploadSettings
{
    /// <summary>
    /// An event triggered whenever a request is made to the API that is initiated by an upload instance
    /// </summary>
    event UploadDelegate<MangaDexRoot> OnApiResponse;

    /// <summary>
    /// An event triggered whenever a rate-limits are exceeded and a delay is required
    /// </summary>
    event UploadDelegate<RateLimit, TimeSpan> OnRateLimited;

    /// <summary>
    /// An event triggered whenever a rate-limit has been observed and is no longer in effect
    /// </summary>
    event UploadDelegate<RateLimit> OnRateLimitPassed;

    /// <summary>
    /// An event triggered whenever a batch of files starts uploading
    /// </summary>
    event UploadDelegate<IFileUpload[]> OnBatchUploadStarted;

    /// <summary>
    /// An event triggered whenever a batch of files has finished uploading
    /// </summary>
    event UploadDelegate<UploadSessionFile[]> OnBatchUploaded;

    /// <summary>
    /// An event triggered whenever the session is abandoned.
    /// </summary>
    /// <remarks>
    /// The bool parameter is whether or not it was abandoned automatically
    /// when the upload session was disposed.
    /// </remarks>
    event UploadDelegate<bool> OnSessionAbandoned;

    /// <summary>
    /// An event triggered whenever the session is committed
    /// </summary>
    event UploadDelegate<UploadSessionCommit, UploadSessionFile[]> OnSessionCommitStarted;

    /// <summary>
    /// An event triggered whenever the session is committed and the chapter data is available
    /// </summary>
    event UploadDelegate<Chapter> OnSessionCommitted;

    /// <summary>
    /// An event triggered whenever files are deleted from the upload session
    /// </summary>
    event UploadDelegate<UploadSessionFile[]> OnFilesDeleted;

    /// <summary>
    /// Whether or not to abandon the session when the upload instance is disposed
    /// and the session has not been committed
    /// </summary>
    /// <remarks>
    /// If this is false, the session will be orphaned and you will need to abandon it yourself.
    /// The upload instance cannot automatically commit the upload because it would be lacking chapter information.
    /// </remarks>
    bool AbandonSessionOnDispose { get; set; }

    /// <summary>
    /// The max number of files to buffer before uploading
    /// </summary>
    /// <remarks>This should never exceed 10 - as that is the max MD supports</remarks>
    int MaxBatchSize { get; set; }

    /// <summary>
    /// The cancellation token to use for the upload session
    /// </summary>
    /// <remarks>If cancellation is requested and <see cref="AbandonSessionOnDispose"/> is true, the session will be abandoned.</remarks>
    CancellationToken Token { get; set; }

    /// <summary>
    /// The function to use to order the files in the upload session when committing the chapter
    /// </summary>
    Func<IEnumerable<UploadSessionFile>, string[]> PageOrderFactory { get; set; }

    /// <summary>
    /// The function to use to get the auth token for the upload session
    /// </summary>
    Func<Task<string>>? AuthTokenFactory { get; set; }

    /// <summary>
    /// Set the value of <see cref="AbandonSessionOnDispose"/>
    /// </summary>
    /// <param name="enabled">Whether to enable or disable the setting</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings DoNotAbandonSessionOnDispose(bool enabled = false);

    /// <summary>
    /// Sets the value for the auth token
    /// </summary>
    /// <param name="token">The token to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings WithAuthToken(string token);

    /// <summary>
    /// Sets the factory for the auth token
    /// </summary>
    /// <param name="token">The factory to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings WithAuthToken(Func<Task<string>> token);

    /// <summary>
    /// Sets the max number of files to buffer before uploading
    /// </summary>
    /// <param name="size">The size of the batch</param>
    /// <returns>The current settings for fluent method chaining</returns>
    /// <remarks>This should never exceed 10 - as that is the max MD supports</remarks>
    IUploadSettings WithMaxBatchSize(int size);

    /// <summary>
    /// Disables batch buffering, so files will be immediately uploaded when they're available
    /// </summary>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings WithNoBatchBuffering() => WithMaxBatchSize(0);

    /// <summary>
    /// Sets the cancellation token to use for the upload session
    /// </summary>
    /// <param name="token">The cancellation token to use for the upload session</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings WithCancellationToken(CancellationToken token);

    /// <summary>
    /// Sets the function to use to order the files in the upload session when committing the chapter
    /// </summary>
    /// <param name="factory">The factory to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings WithPageOrderFactory(Func<IEnumerable<UploadSessionFile>, string[]> factory);

    /// <summary>
    /// Sets the function to use to order the files in the upload session when committing the chapter
    /// </summary>
    /// <param name="factory">The factory to use</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings WithPageOrderFactory(Func<IEnumerable<UploadSessionFile>, IOrderedEnumerable<UploadSessionFile>> factory);

    /// <summary>
    /// Hooks into all of the events for the upload session and forwards them to the given logger
    /// </summary>
    /// <param name="logger">The logger to write to</param>
    /// <returns>The current settings for fluent method chaining</returns>
    IUploadSettings WithLogging(ILogger logger);
}

internal class UploadSettings : IUploadSettings
{
    private int _maxBatchSize = 10;
    private IUploadInstance? _instance = null;

    public event UploadDelegate<MangaDexRoot> OnApiResponse = delegate { };
    public event UploadDelegate<RateLimit, TimeSpan> OnRateLimited = delegate { };
    public event UploadDelegate<RateLimit> OnRateLimitPassed = delegate { };
    public event UploadDelegate<IFileUpload[]> OnBatchUploadStarted = delegate { };
    public event UploadDelegate<UploadSessionFile[]> OnBatchUploaded = delegate { };
    public event UploadDelegate<bool> OnSessionAbandoned = delegate { };
    public event UploadDelegate<UploadSessionCommit, UploadSessionFile[]> OnSessionCommitStarted = delegate { };
    public event UploadDelegate<Chapter> OnSessionCommitted = delegate { };
    public event UploadDelegate<UploadSessionFile[]> OnFilesDeleted = delegate { };

    public bool AbandonSessionOnDispose { get; set; } = true;

    public int MaxBatchSize
    {
        get => _maxBatchSize;
        set
        {
            if (value > 10)
                throw new ArgumentException("MaxBatchSize cannot exceed 10");
            if (value < 1)
                throw new ArgumentException("MaxBatchSize cannot be less than 1");
            _maxBatchSize = value;
        }
    }

    public CancellationToken Token { get; set; } = CancellationToken.None;

    public Func<Task<string>>? AuthTokenFactory { get; set; } = null;

    public Func<IEnumerable<UploadSessionFile>, string[]> PageOrderFactory { get; set; } = DefaultPageOrder;

    public async Task<string?> GetAuthToken()
    {
        if (AuthTokenFactory is null)
            return null;
        return await AuthTokenFactory();
    }

    public void SetInstance(IUploadInstance instance)
    {
        _instance = instance;
    }

    public static string[] DefaultPageOrder(IEnumerable<UploadSessionFile> files)
    {
        return files.Select(t => t.Id).ToArray();
    }

    public void ApiResponse(MangaDexRoot result)
    {
        if (_instance is not null)
            OnApiResponse(_instance, result);
    }

    public void RateLimit(RateLimit limits, TimeSpan span)
    {
        if (_instance is not null)
            OnRateLimited(_instance, limits, span);
    }

    public void RateLimitPassed(RateLimit limits)
    {
        if (_instance is not null)
            OnRateLimitPassed(_instance, limits);
    }

    public void BatchUploadStarted(IFileUpload[] files)
    {
        if (_instance is not null)
            OnBatchUploadStarted(_instance, files);
    }

    public void BatchUploaded(UploadSessionFile[] files)
    {
        if (_instance is not null)
            OnBatchUploaded(_instance, files);
    }

    public void SessionAbandoned(bool autoAbandon)
    {
        if (_instance is not null)
            OnSessionAbandoned(_instance, autoAbandon);
    }

    public void SessionCommitStarted(UploadSessionCommit commit, UploadSessionFile[] files)
    {
        if (_instance is not null)
            OnSessionCommitStarted(_instance, commit, files);
    }

    public void SessionCommitted(Chapter chapter)
    {
        if (_instance is not null)
            OnSessionCommitted(_instance, chapter);
    }

    public void FilesDeleted(UploadSessionFile[] files)
    {
        if (_instance is not null)
            OnFilesDeleted(_instance, files);
    }

    public IUploadSettings DoNotAbandonSessionOnDispose(bool enabled = false)
    {
        AbandonSessionOnDispose = enabled;
        return this;
    }

    public IUploadSettings WithAuthToken(string token)
    {
        return WithAuthToken(() => Task.FromResult(token));
    }

    public IUploadSettings WithAuthToken(Func<Task<string>> token)
    {
        AuthTokenFactory = token;
        return this;
    }

    public IUploadSettings WithMaxBatchSize(int size)
    {
        MaxBatchSize = size;
        return this;
    }

    public IUploadSettings WithCancellationToken(CancellationToken token)
    {
        Token = token;
        return this;
    }

    public IUploadSettings WithPageOrderFactory(Func<IEnumerable<UploadSessionFile>, string[]> factory)
    {
        PageOrderFactory = factory;
        return this;
    }

    public IUploadSettings WithPageOrderFactory(Func<IEnumerable<UploadSessionFile>, IOrderedEnumerable<UploadSessionFile>> factory)
    {
        return WithPageOrderFactory(t => factory(t).Select(t => t.Id).ToArray());
    }

    public IUploadSettings WithLogging(ILogger logger)
    {
        var json = new MdJsonService();
        OnApiResponse += (instance, result) =>
        {
            logger.LogInformation("API Response: {Result}", json.Pretty(result));
        };

        OnRateLimited += (instance, limits, span) =>
        {
            logger.LogWarning("Rate limited: {Span} sec >> {Limits}", json.Pretty(limits), span.TotalSeconds);
        };

        OnRateLimitPassed += (instance, limits) =>
        {
            logger.LogInformation("Rate limit passed: {Limits}", json.Pretty(limits));
        };

        OnBatchUploadStarted += (instance, files) =>
        {
            logger.LogInformation("Batch upload started: {Files}", string.Join(", ", files.Select(t => t.FileName)));
        };

        OnBatchUploaded += (instance, files) =>
        {
            logger.LogInformation("Batch upload completed: {Files}", string.Join(", ", files.Select(t => t.Attributes?.OriginalFileName)));
        };

        OnSessionAbandoned += (instance, autoAbandon) =>
        {
            logger.LogWarning("Session abandoned: {AutoAbandon}", autoAbandon ? "disposed" : "manual");
        };

        OnSessionCommitStarted += (instance, commit, files) =>
        {
            logger.LogInformation("Session commit started: {Commit} \r\n {Files}", json.Pretty(commit), string.Join(", ", files.Select(t => t.Attributes?.OriginalFileName)));
        };

        OnSessionCommitted += (instance, chapter) =>
        {
            logger.LogInformation("Session committed: {Chapter}", json.Pretty(chapter));
        };

        OnFilesDeleted += (instance, files) =>
        {
            logger.LogInformation("Files deleted: {Files}", string.Join(", ", files.Select(t => t.Attributes?.OriginalFileName)));
        };

        return this;
    }
}
