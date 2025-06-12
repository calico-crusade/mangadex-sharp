namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// Represents the type of archive to use when downloading pages
/// </summary>
public enum ArchiveType
{
    /// <summary>
    /// EPub format, used for eBooks
    /// </summary>
    EPUB = 0,
    /// <summary>
    /// Zip format - .zip extension
    /// </summary>
    ZIP = 1,
    /// <summary>
    /// CBZ format - Comic Book Zip, used for comic books
    /// </summary>
    CBZ = 2,
    /// <summary>
    /// Saves pages to the file system as individual images
    /// </summary>
    DIR = 3
}
