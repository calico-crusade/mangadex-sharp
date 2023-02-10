namespace MangaDexSharp;

public class UploadSessionFile : MangaDexModel<UploadSessionFile.AttributesModel>
{
	public class AttributesModel
	{
		[JsonPropertyName("originalFileName")]
		public string OriginalFileName { get; set; } = string.Empty;

		[JsonPropertyName("fileHash")]
		public string FileHash { get; set; } = string.Empty;

		[JsonPropertyName("fileSize")]
		public int FileSize { get; set; }

		[JsonPropertyName("mimeType")]
		public string MimeType { get; set; } = string.Empty;

		[JsonPropertyName("source")]
		public string Source { get; set; } = string.Empty;

		[JsonPropertyName("version")]
		public int Version { get; set; }
	}
}

public class UploadSessionFileList : MangaDexCollection<UploadSessionFile> { }