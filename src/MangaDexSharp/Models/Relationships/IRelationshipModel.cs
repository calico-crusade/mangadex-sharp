namespace MangaDexSharp;

/// <summary>
/// Represents an item that can have a relationship with another item
/// </summary>
public interface IRelationshipModel
{
	/// <summary>
	/// All of the relationships between the parent item and it's children
	/// </summary>
	IRelationship[] Relationships { get; set; }
}
