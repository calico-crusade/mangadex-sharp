using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests related to uploading chapters
/// </summary>
public interface IMangaDexUploadService
{
	/// <summary>
	/// Gets the current users active session
	/// </summary>
	/// <param name="include">The relationships that should be included in the request</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The active upload session</returns>
	Task<MangaDexRoot<UploadSession>> Get(UploadIncludes[]? include = null, string? token = null);

	/// <summary>
	/// Starts an upload session for the current user
	/// </summary>
	/// <param name="manga">The ID of the manga</param>
	/// <param name="groups">The scanlation groups doing the upload</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The new upload session</returns>
	Task<MangaDexRoot<UploadSession>> Begin(string manga, string[] groups, string? token = null);

    /// <summary>
    /// Starts editing a specific chapter
    /// </summary>
    /// <param name="chapterId">The ID of the chapter to edit</param>
    /// <param name="version">The version of the request</param>
    /// <param name="include">The relationships that should be included in the request</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <returns>The current upload session</returns>
    Task<MangaDexRoot<UploadSession>> EditChapter(string chapterId, int version = 1, UploadIncludes[]? include = null, string? token = null);

    /// <summary>
    /// Uploads files to the given session
    /// </summary>
    /// <param name="sessionId">The ID of the session to upload to</param>
    /// <param name="files">All of the files to upload to the chapter (limit 10 per request)</param>
    /// <returns>The uploaded file results</returns>
    Task<UploadSessionFileList> Upload(string sessionId, params IFileUpload[] files);

    /// <summary>
    /// Uploads files to the given session
    /// </summary>
    /// <param name="sessionId">The ID of the session to upload to</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <param name="files">All of the files to upload to the chapter (limit 10 per request)</param>
    /// <returns>The uploaded file results</returns>
    Task<UploadSessionFileList> Upload(string sessionId, string token, params IFileUpload[] files);

    /// <summary>
    /// Uploads files to the given session
    /// </summary>
    /// <param name="sessionId">The ID of the session to upload to</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
    /// <param name="cancel">The cancellation token for the upload</param>
    /// <param name="files">All of the files to upload to the chapter (limit 10 per request)</param>
    /// <returns>The uploaded file results</returns>
    Task<UploadSessionFileList> Upload(string sessionId, string? token, CancellationToken cancel, params IFileUpload[] files);

    /// <summary>
    /// Uploads files to the given session
    /// </summary>
    /// <param name="sessionId">The ID of the session to upload to</param>
    /// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <param name="contentType">The content type of the content-disposition header</param>
    /// <param name="cancel">The cancellation token for the upload</param>
    /// <param name="files">All of the files to upload to the chapter (limit 10 per request)</param>
    /// <returns>The uploaded file results</returns>
    Task<UploadSessionFileList> Upload(string sessionId, string? token, string? contentType, CancellationToken cancel, params IFileUpload[] files);

	/// <summary>
	/// Deletes a specific file from the upload session
	/// </summary>
	/// <param name="sessionId">The ID of the session</param>
	/// <param name="fileId">The ID of the file to delete</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> DeleteUpload(string sessionId, string fileId, string? token = null);

	/// <summary>
	/// Deletes multiple files from the upload session
	/// </summary>
	/// <param name="sessionId">The ID of the session</param>
	/// <param name="fileIds">The file IDs to delete</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> DeleteUpload(string sessionId, string[] fileIds, string? token = null);

	/// <summary>
	/// Abandons and deletes the upload session
	/// </summary>
	/// <param name="sessionId">The ID of the session to abandon</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Abandon(string sessionId, string? token = null);

	/// <summary>
	/// Commits the upload session
	/// </summary>
	/// <param name="sessionId">The ID of the session to commit</param>
	/// <param name="data">The data of the session</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The uploaded chapter</returns>
	Task<MangaDexRoot<Chapter>> Commit(string sessionId, UploadSessionCommit data, string? token = null);
}

internal class MangaDexUploadService(IMdApiService _api) : IMangaDexUploadService
{
	public const string CONTENT_TYPE_FILE = "application/octet-stream";

	public string Root => $"upload";

	public async Task<MangaDexRoot<UploadSession>> Get(UploadIncludes[]? include = null, string? token = null)
	{
		include ??= [UploadIncludes.upload_session_file];
		var filter = new FilterBuilder().Add("includes", include).Build();
		var c = await _api.Auth(token);
		return await _api.Get<MangaDexRoot<UploadSession>>($"{Root}?{filter}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<UploadSession>> Begin(string manga, string[] groups, string? token = null)
	{
		var c = await _api.Auth(token);
		var d = new UploadSessionCreate
		{
			Manga = manga,
			Groups = groups
		};
		return await _api.Post<MangaDexRoot<UploadSession>, UploadSessionCreate>($"{Root}/begin", d, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<UploadSession>> EditChapter(string chapterId, int version = 1, UploadIncludes[]? include = null, string? token = null)
    {
        include ??= [UploadIncludes.upload_session_file];
        var filter = new FilterBuilder().Add("includes", include).Build();
        var c = await _api.Auth(token);
		var d = new EditChapterCreate
		{
			Version = version
		};
		return await _api.Post<MangaDexRoot<UploadSession>, EditChapterCreate>($"{Root}/begin/{chapterId}?{filter}", d, c) ?? new() { Result = "error" };
	}

	public Task<UploadSessionFileList> Upload(string sessionId, params IFileUpload[] files) => Upload(sessionId, null, null, CancellationToken.None, files);

	public Task<UploadSessionFileList> Upload(string sessionId, string token, params IFileUpload[] files) => Upload(sessionId, token, null, CancellationToken.None, files);

	public Task<UploadSessionFileList> Upload(string sessionId, string? token, CancellationToken cancel, params IFileUpload[] files) => Upload(sessionId, token, null, cancel, files);

    public async Task<UploadSessionFileList> Upload(string sessionId, string? token, string? contentType, CancellationToken cancel, params IFileUpload[] files)
	{
		if (files.Length == 0)
			throw new ArgumentException("At least 1 file is required for an upload", nameof(files));

		if (files.Length > 10)
			throw new ArgumentException("There is a limit of 10 files at a time for uploads", nameof(files));

		var c = await _api.Auth(token);
		using var body = new MultipartFormDataContent();

		contentType ??= CONTENT_TYPE_FILE;

        for (var i = 0; i < files.Length; i++)
		{
			var file = files[i];
			var data = await file.Content(cancel);
			var dispositionName = $"file{i + 1}";
            var fileContent = new ByteArrayContent(data);
			fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
			fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
			{
				Name = dispositionName,
				FileName = file.FileName
			};
            body.Add(fileContent, dispositionName, file.FileName);

			foreach (var val in body.Headers.ContentType.Parameters.Where(t => t.Name == "boundary"))
				val.Value = val.Value.Replace("\"", string.Empty);
        }

        return await ((IHttpBuilder)_api.Create($"{Root}/{sessionId}", "POST", c, cancel)
			.BodyContent(body))
			.Result<UploadSessionFileList>() ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> DeleteUpload(string sessionId, string fileId, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Delete<MangaDexRoot>($"{Root}/{sessionId}/{fileId}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> DeleteUpload(string sessionId, string[] fileIds, string? token = null)
	{
		var c = await _api.Auth(token);

		return await ((IHttpBuilder)_api.Create($"{Root}/{sessionId}/batch", "DELETE", c, null)
			.Body(fileIds))
			.Result<MangaDexRoot>() ?? new() {  Result = "error" };
	}

	public async Task<MangaDexRoot> Abandon(string sessionId, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Delete<MangaDexRoot>($"{Root}/{sessionId}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Chapter>> Commit(string sessionId, UploadSessionCommit data, string? token = null)
	{
		var c = await _api.Auth(token);
		return await _api.Post<MangaDexRoot<Chapter>, UploadSessionCommit>($"{Root}/{sessionId}/commit", data, c) ?? new() { Result = "error" };
	}
}
