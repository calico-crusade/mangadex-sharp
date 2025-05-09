namespace MangaDexSharp;

/// <summary>
/// Represents a file in an upload session
/// </summary>
public class UploadSessionFile : MangaDexModel<UploadSessionFile.UploadSessionFileAttributesModel>, IRelationship
{
	/// <summary>
	/// The properties of the file
	/// </summary>
	public class UploadSessionFileAttributesModel
	{
		/// <summary>
		/// The original file name
		/// </summary>
		[JsonPropertyName("originalFileName")]
		public string OriginalFileName { get; set; } = string.Empty;

		/// <summary>
		/// A hash of the files content
		/// </summary>
		[JsonPropertyName("fileHash")]
		public string FileHash { get; set; } = string.Empty;

		/// <summary>
		/// The size of the file
		/// </summary>
		[JsonPropertyName("fileSize")]
		public int FileSize { get; set; }

		/// <summary>
		/// The file's mime-type
		/// </summary>
		[JsonPropertyName("mimeType")]
		public string MimeType { get; set; } = string.Empty;

		/// <summary>
		/// The source of the file
		/// </summary>
		[JsonPropertyName("source")]
		public string Source { get; set; } = string.Empty;

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

/// <summary>
/// Represents a list of files during an upload session
/// </summary>
public class UploadSessionFileList : MangaDexCollection<UploadSessionFile> { }