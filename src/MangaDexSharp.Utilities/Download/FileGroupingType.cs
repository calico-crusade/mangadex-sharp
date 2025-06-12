namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// How to group chapters when creating archives
/// </summary>
public enum FileGroupingType
{
    /// <summary>
    /// Everything gets put into a single file/directory
    /// </summary>
    SingleFile = 0,
    /// <summary>
    /// All of the chapters from the same volume go into the same file/directory
    /// </summary>
    Volumes = 1,
    /// <summary>
    /// All of the chapters go into their own file/directory
    /// </summary>
    Chapters = 2,
}
