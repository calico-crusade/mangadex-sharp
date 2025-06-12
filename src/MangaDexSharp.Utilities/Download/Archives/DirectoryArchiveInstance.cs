using CardboardBox.Extensions;
using System.IO;

namespace MangaDexSharp.Utilities.Download.Archives;

internal class DirectoryArchiveInstance(
    IDownloadSettings _settings,
    string _directory,
    Func<string?, int, string> _dirFactory) : ArchiveInstance(_settings)
{
    private bool _firstGroup = true;
    private bool _firstNoDir = false;
    private string? _subDir = null;

    public override Task Initialize()
    {
        if (!Directory.Exists(_directory))
            Directory.CreateDirectory(_directory);

        return base.Initialize();
    }

    public string GetSubDirectory(string? key, int index)
    {
        return _dirFactory(key, index);
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
        void Sync()
        {
            //If this is the first group and the key is null then we might be
            //dealing with everything in a single directory, so skip adding
            //the sub directory until we know more
            if (_firstGroup && string.IsNullOrEmpty(key))
            {
                _firstNoDir = true;
                _firstGroup = false;
                return;
            }

            //Set the sub directory based on the key and index
            _subDir = GetSubDirectory(key, index);
            //If this is the first group, then we don't need to do anything else
            if (_firstGroup)
            {
                _firstGroup = false;
                _firstNoDir = false;
                return;
            }

            //If there are no files in the root directory,
            //we don't need to do anything else
            if (!_firstNoDir)
                return;

            //If we are here, it means we thought there was no sub directory,
            //but it turns out we need one, so we'll need to move the files
            //in the root directory into a new sub-directory specifically for
            //the first chapter.
            var firstDir = GetSubDirectory(key, index - 1);
            var dir = Path.Combine(_directory, firstDir);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            //Get all of the files in the root
            var files = Directory.GetFiles(_directory);
            //Move all of the files into the new sub-directory
            foreach (var file in files)
            {
                var destination = Path.Combine(dir, Path.GetFileName(file));
                if (File.Exists(destination))
                    File.Delete(destination);
                File.Move(file, destination);
            }
            //Make sure we don't do this check again
            _firstNoDir = false;
        }

        Sync();
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
