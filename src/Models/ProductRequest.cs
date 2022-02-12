namespace Demo.MongoDB.Transactions.Models;

public class ProductRequest
{
    public string Name { get; set; } = default!;
    public uint Quantity { get; set; }
}
