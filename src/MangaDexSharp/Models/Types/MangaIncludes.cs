namespace MangaDexSharp;

/// <summary>
/// Relationships that can be included in manga and chapter return results
/// </summary>
[JsonConverter(typeof(MangaDexEnumParser<MangaIncludes>))]
public enum MangaIncludes
{
	/// <summary>
	/// Includes any related manga objects
	/// </summary>
	manga,
	/// <summary>
	/// Returns any related chapter objects
	/// </summary>
	chapter,
	/// <summary>
	/// Returns any cover-art objects
	/// </summary>
	cover_art,
	/// <summary>
	/// Returns any author objects
	/// </summary>
	author,
	/// <summary>
	/// Returns any artist objects
	/// </summary>
	artist,
	/// <summary>
	/// Returns any scanlation group objects
	/// </summary>
	scanlation_group,
	/// <summary>
	/// Returns any tag objects
	/// </summary>
	tag,
	/// <summary>
	/// Returns any user objects
	/// </summary>
	user,
	/// <summary>
	/// Returns any custom list objects
	/// </summary>
	custom_list,
	/// <summary>
	/// Returns any user objects for the creator
	/// </summary>
	creator,
}
