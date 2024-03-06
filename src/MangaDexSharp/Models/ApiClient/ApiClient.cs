namespace MangaDexSharp;

/// <summary>
/// Represents an api client item returned from the MD api
/// </summary>
public class ApiClient : MangaDexModel<ApiClient.ApiClientAttributesModel>, IRelationshipModel
{
    /// <summary>
    /// All of the relationship items related to this api client
    /// </summary>
    [JsonPropertyName("relationships")]
    public IRelationship[] Relationships { get; set; } = Array.Empty<IRelationship>();

    /// <summary>
    /// All of the properties on this api client
    /// </summary>
    public class ApiClientAttributesModel : ApiClientData
    {
        /// <summary>
        /// The client id of the API client
        /// </summary>
        [JsonPropertyName("externalClientId")]
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// Whether or not the client is active
        /// </summary>
        [JsonPropertyName("isActive")]
        public bool Active { get; set; }

        /// <summary>
        /// The state of the API client
        /// </summary>
        [JsonPropertyName("state")]
        public ApiClientState State { get; set; }

        /// <summary>
        /// The date the API client was created
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date the API client was last updated
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
