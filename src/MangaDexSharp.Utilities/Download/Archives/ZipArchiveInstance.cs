using CardboardBox.Extensions;
using System.IO;
using System.IO.Compression;

namespace MangaDexSharp.Utilities.Download.Archives;

internal class ZipArchiveInstance(
    IDownloadSettings _settings,
    string _directory,
    Func<string?, int, string> _nameFactory) : ArchiveInstance(_settings)
{
    private Stream? _archiveStream;
    private ZipArchive? _archive;
    private string? _archiveName;

    public override Task Initialize()
    {
        if (!Directory.Exists(_directory))
            Directory.CreateDirectory(_directory);

        return base.Initialize();
    }

    public string GetArchiveName(string? key, int index)
    {
        return _nameFactory(key, index);
    }

    public override Task GroupingChanged(string? key, int index)
    {
        void Sync()
        {
            ClearArchive();

            _archiveName = Path.Combine(_directory, $"{GetArchiveName(key, index)}.zip");
            _archiveStream = File.Create(_archiveName);
            _archive = new ZipArchive(_archiveStream, ZipArchiveMode.Create, true);
        }

        Sync();
        return Task.CompletedTask;
    }

    public override async Task AddFile(DownloadFile file)
    {
        if (file.Status != DownloadStatus.Completed)
        {
            Settings?.Log(LogLevel.Warning, $"File {file.Name} is not completed, it is {file.Status}, skipping archive addition.");
            return;
        }

        if (_archive is null ||
            string.IsNullOrEmpty(_archiveName) ||
            _archiveStream is null)
            throw new NotSupportedException("Archive not initialized or already disposed.");

        var name = GetImageName(file);
        var entry = _archive.CreateEntry(name);
        #if NET8_0_OR_GREATER
        var nameParts = new List<string> { "MD Chapter" };
        if (!string.IsNullOrEmpty(file.Chapter.Attributes?.Volume))
            nameParts.Add($"vol {file.Chapter.Attributes.Volume}");
        if (!string.IsNullOrEmpty(file.Chapter.Attributes?.Chapter))
            nameParts.Add($"ch {file.Chapter.Attributes.Chapter}");
        if (!string.IsNullOrEmpty(file.Chapter.Attributes?.Title))
            nameParts.Add(file.Chapter.Attributes.Title);

        entry.Comment = string.Join(" - ", nameParts.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.PurgePathChars()));
        #endif
        entry.LastWriteTime = DateTimeOffset.Now;
        using var entryStream = entry.Open();
        using var fileStream = File.OpenRead(file.Output!);
        await fileStream.CopyToAsync(entryStream, Settings?.Token ?? CancellationToken.None);
    }

    public void ClearArchive()
    {
        if (_archive is null ||
            string.IsNullOrEmpty(_archiveName) ||
            _archiveStream is null)
            return;

        Settings?.ArchiveCreated(_archiveName);
        _archive.Dispose();
        _archive = null;
        _archiveStream.FlushAsync();
        _archiveStream.Dispose();
        _archiveStream = null;
        _archiveName = null;
    }

    public override Task Finished()
    {
        ClearArchive();
        return Task.CompletedTask;
    }
}
