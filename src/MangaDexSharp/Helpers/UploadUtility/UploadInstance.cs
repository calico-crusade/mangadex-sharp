using System.IO;
using System.Threading;

namespace MangaDexSharp.Helpers.UploadUtility;

/// <summary>
/// Represents an upload session
/// </summary>
public interface IUploadInstance : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// The attributes for the current session
    /// </summary>
    UploadSession.UploadSessionAttributesModel Attributes { get; }

    /// <summary>
    /// The ID of the current session
    /// </summary>
    string SessionId { get; }

    /// <summary>
    /// Whether or not the session has already been committed
    /// </summary>
    bool IsCommitted { get; }

    /// <summary>
    /// Whether or not the session has already been processed
    /// </summary>
    bool IsProcessed { get; }

    /// <summary>
    /// Whether or not the session has already been deleted
    /// </summary>
    bool IsDeleted { get; }

    /// <summary>
    /// Whether or not the session is still alive.
    /// If this is true, it means files can still be uploaded to the session.
    /// </summary>
    /// <remarks>
    /// This is a macro that makes sure that <see cref="IsCommitted"/>, 
    /// <see cref="IsProcessed"/> and <see cref="IsDeleted"/> are all false.
    /// </remarks>
    bool IsAlive { get; }

    /// <summary>
    /// All of the files that have already been uploaded to MangaDex
    /// </summary>
    IEnumerable<UploadSessionFile> Uploads { get; }

    /// <summary>
    /// The next batch of files that will be uploaded
    /// </summary>
    IReadOnlyCollection<IFileUpload> NextBatch { get; }

    /// <summary>
    /// The number of batches that has already been uploaded
    /// </summary>
    int UploadedBatches { get; }

    /// <summary>
    /// Upload a collection of files to the session
    /// </summary>
    /// <param name="files">The files to upload</param>
    Task UploadFiles(IEnumerable<IFileUpload> files) => UploadFiles(files.ToAsyncEnumerable());

    /// <summary>
    /// Upload a collection of files to the session
    /// </summary>
    /// <param name="files">The files to upload</param>
    Task UploadFiles(params IFileUpload[] files) => UploadFiles(files.ToAsyncEnumerable());

    /// <summary>
    /// Upload a collection of files to the session
    /// </summary>
    /// <param name="files">The files to upload</param>
    Task UploadFiles(IAsyncEnumerable<IFileUpload> files);

    /// <summary>
    /// Queues a single file for upload to the session
    /// </summary>
    /// <param name="file">The file to be uploaded</param>
    Task UploadFile(IFileUpload file);

    /// <summary>
    /// Queues a single file for upload to the session
    /// </summary>
    /// <param name="data">The data of the file to upload</param>
    /// <param name="fileName">The name of the file to upload</param>
    Task UploadFile(byte[] data, string fileName);

    /// <summary>
    /// Queues a single file from the disk for upload to the session
    /// </summary>
    /// <param name="path">The path to the file to upload</param>
    /// <param name="buffer">Whether or not to load the file data into memory first</param>
    Task UploadFile(string path, bool buffer = false);

    /// <summary>
    /// Queues a single file to be uploaded to the session
    /// </summary>
    /// <param name="stream">The stream representing the file</param>
    /// <param name="fileName">The name of the file</param>
    /// <param name="buffer">Whether or not to load the stream data into memory first</param>
    /// <param name="leaveOpen">Whether or not to leave the given stream open after buffering</param>
    /// <remarks>
    /// When the given stream is a <see cref="MemoryStream"/> then neither <paramref name="buffer"/> 
    /// nor <paramref name="leaveOpen"/> parameters are used.
    /// </remarks>
    Task UploadFile(Stream stream, string fileName, bool buffer = true, bool leaveOpen = false);

    /// <summary>
    /// Uploads a collection of files to the session
    /// </summary>
    /// <param name="buffer">Whether or not to load the file data into memory first</param>
    /// <param name="paths">The paths to the files to upload</param>
    Task UploadFiles(bool buffer, params string[] paths);

    /// <summary>
    /// Deletes files that have been uploaded to the session
    /// </summary>
    /// <param name="ids">The IDs of the <see cref="UploadSessionFile"/> to delete</param>
    Task DeleteUpload(params string[] ids);

    /// <summary>
    /// Deletes files that have been uploaded to the session
    /// </summary>
    /// <param name="files">The files to delete</param>
    Task DeleteUpload(params UploadSessionFile[] files) => DeleteUpload(files.Select(x => x.Id).ToArray());

    /// <summary>
    /// Abandon the current session and delete it
    /// </summary>
    /// <remarks>You will not be able to perform any further action on this session after it's abandoned</remarks>
    Task Abandon();

    /// <summary>
    /// Commits the current session to MangaDex
    /// </summary>
    /// <param name="data">The chapter data to use for the commit</param>
    /// <param name="pageOrder">The optional order of the pages (will use <see cref="IUploadSettings.PageOrderFactory"/> if null)</param>
    /// <returns>The chapter that was created if it succeeded</returns>
    Task<Chapter> Commit(ChapterDraft data, string[]? pageOrder = null);
}

internal class UploadInstance(
    UploadSettings _settings,
    UploadSession _session,
    List<UploadSessionFile> _startingFiles,
    RateLimit _initialLimits,
    IMangaDex _api) : IUploadInstance
{
    private bool _isDeleted = false;
    private bool _isCommitted = false;
    private RateLimit? _rateLimits = _initialLimits;
    private readonly SemaphoreSlim _uploadSemaphore = new(1, 1);
    private readonly List<IFileUpload> _currentBatch = [];
    private readonly List<UploadSessionFile> _uploadedFiles = [];

    public UploadSession.UploadSessionAttributesModel Attributes 
        => _session?.Attributes 
        ?? throw new InvalidOperationException("Session attributes are null");

    public string SessionId => _session.Id;
    public bool IsCommitted => _isCommitted || Attributes.IsCommitted;
    public bool IsProcessed => Attributes.IsProcessed;
    public bool IsDeleted => _isDeleted || Attributes.IsDeleted;
    public bool IsAlive => !IsCommitted && !IsProcessed && !IsDeleted;

    public IEnumerable<UploadSessionFile> Uploads => _startingFiles.Concat(_uploadedFiles);

    public IReadOnlyCollection<IFileUpload> NextBatch => _currentBatch.AsReadOnly();

    public int UploadedBatches { get; private set; } = 0;

    public async Task<T> MakeRequest<T>(Func<string?, Task<T>> request)
        where T : MangaDexRoot, new()
    {
        var result = await UploadExtensions.RateLimitRequest(request, _rateLimits, 0, _settings);
        _rateLimits = result.RateLimit;
        return result;
    }

    public void EnsureAlive()
    {
        if (IsAlive) return;

        var message = "not alive";
        if (IsCommitted) message = "already committed, you will need to start a chapter edit session.";
        else if (IsProcessed) message = "already processed, you will need to start a chapter edit session.";
        else if (IsDeleted) message = "already deleted, you will need to start a new session.";

        throw new InvalidOperationException("Session is not active. It is " + message);
    }

    public async Task UploadFiles(IAsyncEnumerable<IFileUpload> files)
    {
        EnsureAlive();
        //Iterate through all of the files
        await foreach (var file in files)
            await UploadFile(file);
    }

    public async Task UploadFile(IFileUpload file)
    {
        //Make sure the file is valid
        if (file is null) throw new NullReferenceException("File being uploaded is null");
        //Add the file to the current batch of files
        _currentBatch.Add(file);
        //If the batch isn't full, continue until it is.
        if (_currentBatch.Count < _settings.MaxBatchSize) return;
        //Upload the batch of files
        await EnsureBatchUpload();
    }

    public async Task UploadFiles(bool buffer, params string[] paths)
    {
        foreach(var file in paths)
            await UploadFile(file, buffer);
    }

    public Task UploadFile(byte[] data, string fileName)
    {
        return UploadFile(new RawFileUpload(fileName, data));
    }

    public async Task UploadFile(string path, bool buffer = false)
    {
        static async Task<IFileUpload> GetUpload(string path, bool buffer)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("File not found", path);

            var filename = Path.GetFileName(path);

            if (!buffer) return new PathFileUpload(path);

            var stream = new MemoryStream();
            using var fs = File.OpenRead(path);
            await fs.CopyToAsync(stream);
            stream.Position = 0;
            return new MemoryStreamFileUpload(filename, stream);
        }

        var file = await GetUpload(path, buffer);
        await UploadFile(file);
    }

    public async Task UploadFile(Stream stream, string fileName, bool buffer = true, bool leaveOpen = false)
    {
        IFileUpload file;
        if (stream is MemoryStream ms)
            file = new MemoryStreamFileUpload(fileName, ms);
        else if (!buffer)
            file = new StreamFileUpload(fileName, stream, leaveOpen);
        else
        {
            var io = new MemoryStream();
            await stream.CopyToAsync(io);
            io.Position = 0;
            if (!leaveOpen)
                await stream.DisposeAsync();
            file = new MemoryStreamFileUpload(fileName, io);
        }

        await UploadFile(file);
    }

    public async Task EnsureBatchUpload()
    {
        //Ensure the session is available for uploads
        EnsureAlive();
        //Ensure there are files to upload
        if (_currentBatch.Count == 0) return;
        //Ensure we are the only one currently uploading
        await _uploadSemaphore.WaitAsync();
        //Get the batch of files to be uploaded
        IFileUpload[] batch = [.. _currentBatch];
        //Trigger the event for the batch
        _settings.BatchUploadStarted(batch);
        //Upload the files to the api
        var result = await MakeRequest(token => _api.Upload.Upload(SessionId, token, _settings.Token, batch));
        //Ensure the result is valid
        result.ThrowIfError();
        var uploaded = result.Data.ToArray();
        //Trigger the event for the upload succeeding
        _settings.BatchUploaded(uploaded);
        //Add the files to the list of uploaded files
        _uploadedFiles.AddRange(uploaded);
        //Dispose of all of the files that have been uploaded
        foreach (var file in batch)
            file.Dispose();
        //Increment the number of batches uploaded
        UploadedBatches++;
        //Clear the current batch
        _currentBatch.Clear();
        //Release the semaphore to allow other uploads
        _uploadSemaphore.Release();
    }

    public async Task DeleteUpload(params string[] ids)
    {
        static void RemoveFiles(List<UploadSessionFile> list, string[] ids, out UploadSessionFile[] removals)
        {
            removals = list
                .Where(x => ids.Contains(x.Id))
                .ToArray();
            foreach (var file in removals)
                list.Remove(file);
        }

        //Ensure the session is available for uploads
        EnsureAlive();
        //If there are no files, skip the deletion
        if (ids.Length == 0) return;
        //Determine whether this is a single request or a batch of deletes
        Func<string?, Task<MangaDexRoot>> request = ids.Length == 1
            ? token => _api.Upload.DeleteUpload(SessionId, ids.First(), token)
            : token => _api.Upload.DeleteUpload(SessionId, ids, token);
        //Make the request
        var result = await MakeRequest(request);
        //Ensure the request was valid
        result.ThrowIfError();
        //Remove the files from the source lists
        RemoveFiles(_startingFiles, ids, out var sr);
        RemoveFiles(_uploadedFiles, ids, out var ur);
        var removed = sr.Concat(ur).ToArray();
        _settings.FilesDeleted(removed);
    }

    public Task Abandon()
    {
        return InternalAbandon(false);
    }

    public async Task<Chapter> Commit(ChapterDraft data, string[]? pageOrder = null)
    {
        //Ensure all files are uploaded and available
        await EnsureBatchUpload();
        //Get the commit data
        pageOrder ??= _settings.PageOrderFactory(_uploadedFiles);
        var commit = new UploadSessionCommit
        {
            Chapter = data,
            PageOrder = pageOrder,
        };
        //Trigger the event for starting the commit of the session
        _settings.SessionCommitStarted(commit, [.. Uploads]);
        //Make the request to commit the session
        var result = await MakeRequest(token => _api.Upload.Commit(SessionId, commit, token));
        //Ensure the result is valid
        result.ThrowIfError();
        //Trigger the event for committing the session
        _settings.SessionCommitted(result.Data);
        _isCommitted = true;
        return result.Data;
    }

    public async Task InternalAbandon(bool fromDispose)
    {
        if (!IsAlive) return;
        //Abandon the session
        await MakeRequest(token => _api.Upload.Abandon(SessionId, token));
        //Trigger the event for abandoning the session
        _settings.SessionAbandoned(fromDispose);
        _isDeleted = true;
    }

    public async ValueTask DisposeAsync()
    {
        //If the session is alive and we aren't abandoning the session,
        //make sure we flush any queued files to the server
        if (IsAlive && !_settings.AbandonSessionOnDispose)
            await EnsureBatchUpload();
        //Clear all of our data
        _currentBatch.Clear();
        _uploadSemaphore.Dispose();
        _uploadedFiles.Clear();
        //If the session isn't alive or we are abandoning the session,
        //we can just skip the rest of the dispose
        if (!_settings.AbandonSessionOnDispose ||
            !IsAlive) return;
        //Abandon the session
        await InternalAbandon(true);
    }

    public void Dispose()
    {
        DisposeAsync().AsTask().Wait();
        GC.SuppressFinalize(this);
    }
}
