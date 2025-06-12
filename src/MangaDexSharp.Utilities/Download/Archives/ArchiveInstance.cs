using CardboardBox.Extensions;

namespace MangaDexSharp.Utilities.Download.Archives;

/// <summary>
/// Interface representing something that can create an archive instance
/// </summary>
public interface IArchiveInstance
{
    /// <summary>
    /// Adds a group of files to the instance
    /// </summary>
    /// <param name="files">The files to save</param>
    Task AddFiles(IAsyncGrouping<string?, DownloadFile> files);

    /// <summary>
    /// Indicates that the archive has been finished and no more files will be added.
    /// </summary>
    Task Finished();
}

internal abstract class ArchiveInstance(
    IDownloadSettings _settings) : IArchiveInstance
{
    private bool _initialized = false;
    private int _index = -1;

    /// <summary>
    /// The settings used for the archive instance
    /// </summary>
    public DownloadSettings? Settings => _settings as DownloadSettings;

    /// <summary>
    /// Initializes the archive instance. This method is called before any files are added to the archive.
    /// </summary>
    public virtual Task Initialize()
    {
        Settings?.Log(LogLevel.Information, $"Initializing archive instance for: {GetType().Name}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Indicates that the grouping of files has been completed
    /// </summary>
    /// <param name="key">The key of the grouping</param>
    /// <param name="index">The index of the grouping (tracked locally)</param>
    public virtual Task GroupingChanged(string? key, int index)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Adds a file to the archive instance.
    /// </summary>
    /// <param name="file">The file being added</param>
    public virtual Task AddFile(DownloadFile file) => Task.CompletedTask;

    /// <inheritdoc />
    public virtual async Task AddFiles(IAsyncGrouping<string?, DownloadFile> files)
    {
        if (!_initialized)
        {
            await Initialize();
            _initialized = true;
        }

        _index++;
        await GroupingChanged(files.Key, _index);

        await foreach (var file in files)
            await AddFile(file);
    }

    /// <inheritdoc />
    public virtual Task Finished()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the image file name based on the settings
    /// </summary>
    /// <param name="file">The file to get the name for</param>
    /// <returns>The name of the file</returns>
    public virtual string GetImageName(DownloadFile file)
    {
        var transform = new ImageNameTransform($"{file.Name}.{file.Extension}",
            file.Index,
            file.TotalPages,
            _settings);
        var fileName = _settings.ImageNameFactory?.Invoke(transform)
            ?? $"{file.Name}.{file.Extension}";
        var chapterOrdinal = double.TryParse(file.Chapter.Attributes?.Chapter, out var value)
            ? value
            : 1.0;
        var chapterStr = chapterOrdinal.ToString("0.00").PadLeft(6, '0');
        if (!string.IsNullOrEmpty(file.Chapter.Attributes?.Title))
            chapterStr += $"-{file.Chapter.Attributes.Title.PurgePathChars()}";
        return $"{chapterStr}-{fileName}";
    }
}