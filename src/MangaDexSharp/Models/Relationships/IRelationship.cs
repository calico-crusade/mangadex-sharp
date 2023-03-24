namespace MangaDexSharp;

/// <summary>
/// Represents the relationship between the parent item and the associated type
/// </summary>
[JsonConverter(typeof(MangaDexParser<IRelationship>))]
public interface IRelationship : IJsonType
{
	/// <summary>
	/// The ID of the child item
	/// </summary>
	string Id { get; set; }
}
