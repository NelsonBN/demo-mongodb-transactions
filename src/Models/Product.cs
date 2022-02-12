using MongoDB.Bson.Serialization.Attributes;

namespace Demo.MongoDB.Transactions.Models;

public class Product
{
    [BsonElement("_id")]
    public Guid Id { get; set; }
    public uint Number { get; set; }

    public string Name { get; set; } = default!;
    public uint Quantity { get; set; }

    public Product() => Id = Guid.NewGuid();
}
