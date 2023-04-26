namespace MangaDexSharp;

/// <summary>
/// Represents an upload sessions
/// </summary>
public class UploadSession : MangaDexModel<UploadSession.UploadSessionAttributesModel>
{
	/// <summary>
	/// The properties for this upload session
	/// </summary>
	public class UploadSessionAttributesModel
	{
		/// <summary>
		/// Whether or not the session has been committed yet
		/// </summary>
		[JsonPropertyName("isCommitted")]
		public bool IsCommitted { get; set; }

		/// <summary>
		/// Whether or not the session has been processed
		/// </summary>
		[JsonPropertyName("isProcessed")]
		public bool IsProcessed { get; set; }

		/// <summary>
		/// Whether or not the session has been deleted
		/// </summary>
		[JsonPropertyName("isDeleted")]
		public bool IsDeleted { get; set; }

		/// <summary>
		/// The version of the request
		/// </summary>
		[JsonPropertyName("version")]
		public int Version { get; set; }

		/// <summary>
		/// When the session was created
		/// </summary>
		[JsonPropertyName("createdAt")]
		public DateTime? CreatedAt { get; set; }

		/// <summary>
		/// The the session was last updated
		/// </summary>
		[JsonPropertyName("updatedAt")]
		public DateTime? UpdatedAt { get; set; }
	}
}
