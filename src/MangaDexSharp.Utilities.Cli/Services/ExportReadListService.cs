namespace MangaDexSharp.Utilities.Cli.Services;

using CardboardBox.Extensions;
using System.Text;
using Writers;
using MangaStatus = (Manga manga, ReadStatus status, Chapter? chapter);

public interface IExportReadListService
{
    Task WriteUsersMangaStatusToFile(string file, ReadStatus? status, bool includeChapters, string lang, CancellationToken token);
}

internal class ExportReadListService(
    IRateLimitService _api,
    ILogger<ExportReadListService> _logger) : IExportReadListService
{
    public static Dictionary<string, Func<string, IRecordWriter<MangaStatus>>> GetWriters(string lang)
    {
        return new()
        {
            ["csv"] = p => new CsvRecordWriter<MangaStatus, CsvManga>(p, t => CsvManga.FromManga(t, lang)),
            ["json"] = p => new JsonRecordWriter<MangaStatus, CsvManga>(p, t => CsvManga.FromManga(t, lang)),
        };
    }

    public async IAsyncEnumerable<MangaStatus> GetUsersMangaStatus(ReadStatus? status, bool includeChapters, string lang,
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
                yield return await PopChapter(m, ms, includeChapters, lang, token);
            }
        }
    }

    public async Task<MangaStatus> PopChapter(Manga manga, ReadStatus status, bool chaps, string lang, CancellationToken token)
    {
        if (!chaps) return (manga, status, null);

        var filter = new ChaptersFilter
        {
            Manga = manga.Id,
            Limit = 1,
            TranslatedLanguage = string.IsNullOrEmpty(lang) ? [] : [lang],
            Order = new()
            {
                [ChaptersFilter.OrderKey.chapter] = OrderValue.desc
            },
            
        };
        var chapters = await _api.Request(t => t.Chapter.List(filter), token);
        chapters.ThrowIfError();
        var chapter = chapters.Data.FirstOrDefault();
        return (manga, status, chapter);
    }

    public async Task WriteUsersMangaStatusToFile(string file, ReadStatus? status, bool includeChapters, string lang, CancellationToken token)
    {
        var ext = Path.GetExtension(file).Trim('.').ToLower();
        if (!GetWriters(lang).TryGetValue(ext, out var fetch))
        {
            _logger.LogError("No writer found for {Ext}", ext);
            return;
        }

        using var writer = fetch(file);
        var manga = GetUsersMangaStatus(status, includeChapters, lang, token);
        await writer.Write(manga);
        _logger.LogInformation("Wrote read list to {File}", file);
    }

    internal record class CsvManga(
        string Id,
        string Title,
        string CoverUrl,
        string TitleUrl,
        string Status,
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] string? LatestChapterId,
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] string? LatestChapterTitle,
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] string? LatestChapterUrl)
    {
        public static CsvManga FromManga(MangaStatus m, string lang)
        {
            string? DetermineChapterTitle()
            {
                if (m.chapter is null) return null;

                var bob = new StringBuilder();
                if (!string.IsNullOrEmpty(m.chapter.Attributes?.Volume))
                    bob.Append($"Vol.{m.chapter.Attributes.Volume} ");
                if (!string.IsNullOrEmpty(m.chapter.Attributes?.Chapter))
                    bob.Append($"Ch.{m.chapter.Attributes.Chapter} ");
                if (!string.IsNullOrEmpty(m.chapter.Attributes?.Title))
                    bob.Append(m.chapter.Attributes.Title);
                return bob.ToString().Trim();
            }

            string? DetermineChapterUrl()
            {
                if (m.chapter is null) return null;

                if (!string.IsNullOrEmpty(m.chapter.Attributes?.ExternalUrl))
                    return m.chapter.Attributes.ExternalUrl;

                return $"https://mangadex.org/chapter/{m.chapter.Id}";
            }

            var cover = m.manga.CoverArt().FirstOrDefault()?.Attributes;
            var coverUrl = cover is null
                ? string.Empty
                : $"https://uploads.mangadex.org/covers/{m.manga.Id}/{cover.FileName}";

            return new CsvManga(
                m.manga.Id,
                m.manga.Attributes?.Title?.PreferredOrFirst(t => t.Key == lang).Value ?? string.Empty,
                coverUrl,
                $"https://mangadex.org/title/{m.manga.Id}",
                m.status.ToString(),
                m.chapter?.Id,
                DetermineChapterTitle(),
                DetermineChapterUrl());
        }
    }
}
