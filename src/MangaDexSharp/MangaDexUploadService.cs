using System.Net.Http;

namespace MangaDexSharp;

public interface IMangaDexUploadService
{
	Task<MangaDexRoot<UploadSession>> Get(string? token = null);
	Task<MangaDexRoot<UploadSession>> Begin(string manga, string[] groups, string? token = null);
	Task<MangaDexRoot<UploadSession>> EditChapter(string chapterId, int version = 1, string? token = null);
	Task<UploadSessionFileList> Upload(string sessionId, string? token = null, params IFileUpload[] files);
	Task<MangaDexRoot> DeleteUpload(string sessionId, string fileId, string? token = null);
	Task<MangaDexRoot> DeleteUpload(string sessionId, string[] fileIds, string? token = null);
	Task<MangaDexRoot> Abandon(string sessionId, string? token = null);
	Task<MangaDexRoot<Chapter>> Commit(string sessionId, UploadSessionCommit data, string? token = null);
}

public class MangaDexUploadService : IMangaDexUploadService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{_creds.ApiUrl}/upload";

	public MangaDexUploadService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<MangaDexRoot<UploadSession>> Get(string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Get<MangaDexRoot<UploadSession>>(Root, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<UploadSession>> Begin(string manga, string[] groups, string? token = null)
	{
		var c = await Auth(token, _creds);
		var d = new UploadSessionCreate
		{
			Manga = manga,
			Groups = groups
		};
		return await _api.Post<MangaDexRoot<UploadSession>, UploadSessionCreate>($"{Root}/begin", d, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<UploadSession>> EditChapter(string chapterId, int version = 1, string? token = null)
	{
		var c = await Auth(token, _creds);
		var d = new EditChapterCreate
		{
			Version = version
		};
		return await _api.Post<MangaDexRoot<UploadSession>, EditChapterCreate>($"{Root}/begin/{chapterId}", d, c) ?? new() { Result = "error" };
	}

	public async Task<UploadSessionFileList> Upload(string sessionId, string? token = null, params IFileUpload[] files)
	{
		if (files.Length == 0)
			throw new ArgumentException("At least 1 file is required for an upload", nameof(files));

		if (files.Length > 10)
			throw new ArgumentException("There is a limit of 10 files at a tile for uploads", nameof(files));

		var c = await Auth(token, _creds);
		using var body = new MultipartFormDataContent();

		for(var i = 0; i < files.Length; i++)
		{
			var file = files[i];
			var data = await file.Content();
			body.Add(new ByteArrayContent(data), "file" + (i + 1), file.FileName);
		}

		return await _api.Create($"{Root}/{sessionId}", "POST")
			.With(c)
			.BodyContent(body)
			.Result<UploadSessionFileList>() ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> DeleteUpload(string sessionId, string fileId, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{sessionId}/{fileId}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> DeleteUpload(string sessionId, string[] fileIds, string? token = null)
	{
		var c = await Auth(token, _creds);

		return await _api.Create($"{Root}/{sessionId}/batch", "DELETE")
			.With(c)
			.Body(fileIds)
			.Result<MangaDexRoot>() ?? new() {  Result = "error" };
	}

	public async Task<MangaDexRoot> Abandon(string sessionId, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Delete<MangaDexRoot>($"{Root}/{sessionId}", c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot<Chapter>> Commit(string sessionId, UploadSessionCommit data, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot<Chapter>, UploadSessionCommit>($"{Root}/{sessionId}/commit", data, c) ?? new() { Result = "error" };

	}
}
