using System.Diagnostics;
using System.IO;

namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// Represents a file that will or has been downloaded.
/// </summary>
public class DownloadFile(
    string _url,
    Chapter _chapter,
    int _index,
    int _totalPages,
    Manga? _manga)
{
    private readonly Stopwatch _watch = new();

    /// <summary>
    /// The URL of the image that was downloaded
    /// </summary>
    public string Url { get; } = _url;

    /// <summary>
    /// The name of the file without it's extension
    /// </summary>
    public string Name => Path.GetFileNameWithoutExtension(Url);

    /// <summary>
    /// The extension of the file
    /// </summary>
    public string Extension => Path.GetExtension(Url).TrimStart('.');

    /// <summary>
    /// The chapter that the image belongs to
    /// </summary>
    public Chapter Chapter { get; } = _chapter;

    /// <summary>
    /// The total number of pages in the chapter
    /// </summary>
    public int TotalPages { get; } = _totalPages;

    /// <summary>
    /// The index of the image in the chapter
    /// </summary>
    public int Index { get; } = _index;

    /// <summary>
    /// The number of retry attempts made to download the image
    /// </summary>
    public int TotalRetries { get; set; } = 0;

    /// <summary>
    /// The file path where the image has been saved
    /// </summary>
    public string? Output { get; private set; } = null;

    /// <summary>
    /// How long the download operation took
    /// </summary>
    public TimeSpan Elapsed => _watch.Elapsed;

    /// <summary>
    /// An error message if the download failed
    /// </summary>
    public string? Error { get; private set; } = null;

    /// <summary>
    /// The manga that the chapter belongs to, if available.
    /// </summary>
    public Manga? Manga { get; } = _manga;

    /// <summary>
    /// The status of the download operation.
    /// </summary>
    public DownloadStatus Status
    {
        get
        {
            if (string.IsNullOrEmpty(Output) && !_watch.IsRunning)
                return DownloadStatus.Queued;

            if (_watch.IsRunning)
                return DownloadStatus.Downloading;

            if (!string.IsNullOrEmpty(Output))
                return DownloadStatus.Completed;

            if (!string.IsNullOrEmpty(Error))
                return DownloadStatus.Failed;

            return DownloadStatus.Unknown;
        }
    }

    /// <summary>
    /// Indicates that the download operation has started
    /// </summary>
    internal void Start()
    {
        _watch.Start();
    }

    /// <summary>
    /// Indicates that the download operation has completed
    /// </summary>
    /// <param name="path">The path to the image locally</param>
    internal void Complete(string path)
    {
        _watch.Stop();
        Output = path;
    }

    /// <summary>
    /// Indicates that the download operation has failed
    /// </summary>
    /// <param name="error">The exception that occurred</param>
    internal void Fail(string error)
    {
        _watch.Stop();
        Error = error;
    }
}
