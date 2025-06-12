namespace MangaDexSharp.Cli.Verbs;

using Utilities.Upload;

[Verb("upload", HelpText = "Tests uploading a chapter to mangadex using the upload utility")]
public class UploadVerbOptions
{

}

internal class UploadVerb(
    IMdJsonService _json,
    IUploadUtilityService _upload,
    ILogger<UploadVerb> logger) : BooleanVerb<UploadVerbOptions>(logger)
{
    public override async Task<bool> Execute(UploadVerbOptions options, CancellationToken token)
    {
        await _upload.CloseExisting();

        string mangaId = "f9c33607-9180-4ba6-b85c-e4b5faee7192"; //Official "Test" Manga
        string[] groups = ["e11e461b-8c3a-4b5c-8b07-8892c2dcf449"]; //Cardboard test

        string[] files =
        [
            @"F:\Pictures\lewds.png",
            @"F:\Pictures\HUH.png",
            @"F:\Pictures\ugly-bastard-handsome.png",
            @"F:\Pictures\Stupid-detector.jpg",
            @"F:\Pictures\yoda.jpg",
        ];

        _logger.LogInformation("Creating sessions!");
        await using var session = await _upload.New(
            mangaId, groups,  c => c
                .WithCancellationToken(token)
                .WithMaxBatchSize(3)
                .WithLogging(_logger));

        _logger.LogInformation("Session ID: {id}", session.SessionId);

        await Task.WhenAll(files.Select(t => session.UploadFile(t)));

        //foreach (var file in files)
        //{
        //    await session.UploadFile(file);
        //}

        var chapter = await session.Commit(new ChapterDraft
        {
            Chapter = "1338",
            Title = "Automated Test Chapter - MDSharp",
            TranslatedLanguage = "en",
            Volume = "69"
        });

        _logger.LogInformation("Took {batches} batches", session.UploadedBatches);
        _logger.LogInformation("Chapter: {chapter}", _json.Pretty(chapter));

        foreach(var upload in session.Uploads)
        {
            _logger.LogInformation("Uploaded File: {upload}", _json.Pretty(upload));
        }
        return true;
    }
}
