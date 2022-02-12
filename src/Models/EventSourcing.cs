using MongoDB.Bson.Serialization.Attributes;

namespace Demo.MongoDB.Transactions.Models;

public class EventSourcing
{
    [BsonElement("_id")]
    public Guid Id { get; set; }

    public string Description { get; set; } = default!;

    public EventSourcing() => Id = Guid.NewGuid();
}
