namespace MangaDexSharp.Utilities.Cli.Services;

using Writers;
using MangaStatus = (Manga manga, ReadStatus status);

public interface IExportReadListService
{
    Task WriteUsersMangaStatusToFile(string file, ReadStatus? status, CancellationToken token);
}

internal class ExportReadListService(
    IRateLimitService _api,
    ILogger<ExportReadListService> _logger) : IExportReadListService
{
    public static Dictionary<string, Func<string, IRecordWriter<MangaStatus>>> GetWriters()
    {
        return new()
        {
            ["csv"] = p => new CsvRecordWriter<MangaStatus, CsvManga>(p, CsvManga.FromManga),
            ["json"] = p => new JsonRecordWriter<MangaStatus, CsvManga>(p, CsvManga.FromManga),
        };
    }

    public async IAsyncEnumerable<MangaStatus> GetUsersMangaStatus(ReadStatus? status,
        [EnumeratorCancellation] CancellationToken token)
    {
        //Get all of the user's read statuses
        var readStatus = await _api.Request(t => t.Manga.Status(status), token);
        readStatus.ThrowIfError();

        //Validate that there are some present
        if (readStatus.Statuses is null ||
            readStatus.Statuses.Count == 0)
        {
            _logger.LogWarning("No read statuses found");
            yield break;
        }

        if (token.IsCancellationRequested) yield break;

        //Batch the requests by 100 Ids at a time
        var batches = readStatus.Statuses.Keys.Batch(100);
        foreach (var batch in batches)
        {
            if (token.IsCancellationRequested) yield break;
            //Request the batch of manga
            var filter = new MangaFilter { Ids = batch };
            var manga = await _api.Request(t => t.Manga.List(filter), token);
            manga.ThrowIfError();
            //Return each manga and it's read status
            foreach (var m in manga.Data)
            {
                if (token.IsCancellationRequested) yield break;
                var ms = readStatus.Statuses.TryGetValue(m.Id, out var s) ? s : ReadStatus.reading;
                yield return (m, ms);
            }
        }
    }

    public async Task WriteUsersMangaStatusToFile(string file, ReadStatus? status, CancellationToken token)
    {
        var ext = Path.GetExtension(file).Trim('.').ToLower();
        if (!GetWriters().TryGetValue(ext, out var fetch))
        {
            _logger.LogError("No writer found for {Ext}", ext);
            return;
        }

        using var writer = fetch(file);
        var manga = GetUsersMangaStatus(status, token);
        await writer.Write(manga);
        _logger.LogInformation("Wrote read list to {File}", file);
    }

    internal record class CsvManga(
        string Id,
        string Title,
        string CoverUrl,
        string TitleUrl,
        string Status)
    {
        public static CsvManga FromManga(MangaStatus m)
        {
            var cover = m.manga.CoverArt().FirstOrDefault()?.Attributes;
            var coverUrl = cover is null
                ? string.Empty
                : $"https://uploads.mangadex.org/covers/{m.manga.Id}/{cover.FileName}";

            return new CsvManga(
                m.manga.Id,
                m.manga.Attributes?.Title?.PreferedOrFirst(t => t.Key == "en").Value ?? string.Empty,
                coverUrl,
                $"https://mangadex.org/title/{m.manga.Id}",
                m.status.ToString());
        }
    }
}
