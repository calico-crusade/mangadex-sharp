namespace MangaDexSharp;

public class UploadSession : MangaDexModel<UploadSession.AttributesModel>
{
	public class AttributesModel
	{
		[JsonPropertyName("isCommitted")]
		public bool IsCommitted { get; set; }

		[JsonPropertyName("isProcessed")]
		public bool IsProcessed { get; set; }

		[JsonPropertyName("isDeleted")]
		public bool IsDeleted { get; set; }

		[JsonPropertyName("version")]
		public int Version { get; set; }

		[JsonPropertyName("createdAt")]
		public DateTime? CreatedAt { get; set; }

		[JsonPropertyName("updatedAt")]
		public DateTime? UpdatedAt { get; set; }
	}
}
