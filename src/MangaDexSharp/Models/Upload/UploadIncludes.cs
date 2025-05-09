namespace MangaDexSharp;

/// <summary>
/// Relationships that can be included when fetching upload sessions
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<UploadIncludes>))]
public enum UploadIncludes
{
    /// <summary>
    /// The manga that is being uploaded to
    /// </summary>
    manga,
    /// <summary>
    /// The scanlation group that is being uploaded to
    /// </summary>
    scanlation_group,
    /// <summary>
    /// The user that is uploading the chapter
    /// </summary>
    user,
    /// <summary>
    /// The file that is being uploaded
    /// </summary>
    upload_session_file,
    /// <summary>
    /// The chapter being edited
    /// </summary>
    chapter,
}
