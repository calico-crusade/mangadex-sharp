namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// The status of a download operation
/// </summary>
public enum DownloadStatus
{
    /// <summary>
    /// The image is queued for downloading
    /// </summary>
    Queued = 0,
    /// <summary>
    /// The image is currently being downloaded
    /// </summary>
    Downloading = 1,
    /// <summary>
    /// The image was downloaded successfully and the download operation is complete
    /// </summary>
    Completed = 2,
    /// <summary>
    /// The image failed to download for some reason
    /// </summary>
    Failed = 3,
    /// <summary>
    /// The status of the download is unknown, possibly due to an error or uninitialized state
    /// </summary>
    Unknown = 4,
}
