using CardboardBox.Extensions;
using System.IO;
using System.IO.Compression;

namespace MangaDexSharp.Utilities.Download.Archives;

internal class CbzArchiveInstance(
    IDownloadSettings _settings,
    string _directory,
    Func<string?, int, string> _nameFactory) : ArchiveInstance(_settings)
{
    private string? _subDir = null;

    public override Task Initialize()
    {
        if (!Directory.Exists(_directory))
            Directory.CreateDirectory(_directory);

        return base.Initialize();
    }

    public string GetImagePath(DownloadFile file)
    {
        var directory = string.IsNullOrEmpty(_subDir)
            ? _directory
            : Path.Combine(_directory, _subDir);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var name = GetImageName(file);
        return Path.Combine(directory, name);
    }

    public override Task GroupingChanged(string? key, int index)
    {
        ClearArchive();

        _subDir = _nameFactory(key, index);
        var next = Path.Combine(_directory, _subDir);
        if (Directory.Exists(next))
            Directory.Delete(next, true);
            
        Directory.CreateDirectory(next);

        return Task.CompletedTask;
    }

    public void ClearArchive()
    {
        if (string.IsNullOrEmpty(_subDir)) return;

        var dir = Path.Combine(_directory, _subDir);
        if (!Directory.Exists(dir)) return;

        var files = Directory.GetFiles(dir);
        if (files.Length == 0)
        {
            Directory.Delete(dir, true);
            return;
        }

        var output = Path.Combine(_directory, $"{_subDir}.cbz");
        if (File.Exists(output))
            File.Delete(output);

        ZipFile.CreateFromDirectory(dir, output, CompressionLevel.Optimal, false);
        Directory.Delete(dir, true);
        Settings?.ArchiveCreated(output);
    }

    public override Task Finished()
    {
        ClearArchive();
        return Task.CompletedTask;
    }

    public override Task AddFile(DownloadFile file)
    {
        if (file.Status != DownloadStatus.Completed)
        {
            Settings?.Log(LogLevel.Warning, $"File {file.Name} is not completed, it is {file.Status}, skipping archive addition.");
            return Task.CompletedTask;
        }

        var image = GetImagePath(file);
        File.Copy(file.Output!, image, true);
        return Task.CompletedTask;
    }
}
