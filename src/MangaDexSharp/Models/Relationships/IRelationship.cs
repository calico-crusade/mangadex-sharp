namespace MangaDexSharp;

[JsonConverter(typeof(MangaDexParser<IRelationship>))]
public interface IRelationship : IJsonType
{
	string Id { get; set; }
}
