using System.IO;

namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// The parameters for the image name transformation.
/// </summary>
public class ImageNameTransform(
    string path,
    int pageIndex,
    int total,
    IDownloadSettings settings)
{
    /// <summary>
    /// The original file name / path
    /// </summary>
    public string Original { get; } = path;

    /// <summary>
    /// The name of the file without the extension
    /// </summary>
    public string Name { get; } = Path.GetFileNameWithoutExtension(path);

    /// <summary>
    /// The file extension of the file
    /// </summary>
    public string Extension { get; } = Path.GetExtension(path).TrimStart('.');

    /// <summary>
    /// The index of the file in the chapter
    /// </summary>
    public int Index { get; } = pageIndex;

    /// <summary>
    /// The settings used for the download
    /// </summary>
    public IDownloadSettings Settings { get; } = settings;

    /// <summary>
    /// The total number of pages in the chapter.
    /// </summary>
    public int TotalPages { get; set; } = total;
}
