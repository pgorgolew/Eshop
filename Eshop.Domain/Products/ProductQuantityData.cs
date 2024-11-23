using Eshop.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Domain.Products;

public class ProductQuantityData: ValueObject
{
    private ProductQuantityData()
    {

    }

    public ProductQuantityData(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public void IncrementQuantity()
    {
        Quantity++;
    }
    
    public void DecrementQuantity()
    {
        Quantity--;
    }
}