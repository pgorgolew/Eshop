using Eshop.Domain.CheckoutCarts.Events;
using Eshop.Domain.CheckoutCarts.Rules;
using Eshop.Domain.Orders;
using Eshop.Domain.Products;
using Eshop.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Domain.CheckoutCarts;

public enum EntityStatus
{
    Active,
    Archived
}

public class CheckoutCart : Entity, IAggregateRoot
{
    [BsonRepresentation(BsonType.String)]
    public Guid CustomerId { get; private set; }

    public List<ProductQuantityData> Products { get; private set; }
    
    [BsonRepresentation(BsonType.String)]
    public EntityStatus Status { get; private set; }
    
    private CheckoutCart(Guid customerId, List<ProductQuantityData> products) : base(Guid.NewGuid())
    {
        CustomerId = customerId;
        Products = products ?? throw new ArgumentNullException(nameof(products));
        Status = EntityStatus.Active;
        AddDomainEvent(new CheckoutCartAddedEvent(Id, customerId));
    }
    
    public static CheckoutCart Create(Guid customerId, List<ProductQuantityData> products)
    {
        return new CheckoutCart(customerId, products);
    }
    
    public void AddProduct(Guid productId)
    {
        ValidateRules();
        
        var existingProduct = Products.FirstOrDefault(product => product.ProductId == productId, null);

        if (existingProduct != null)
        {
            existingProduct.IncrementQuantity();
        }
        else
        {
            Products.Add(new ProductQuantityData(productId, 1));
        }
        
        AddDomainEvent(new CheckoutCartProductAddedEvent(Id, productId));
    }
    
    public void RemoveProduct(Guid productId)
    {
        ValidateRules();
        
        var existingProduct = Products.FirstOrDefault(product => product.ProductId == productId, null);

        if (existingProduct != null && existingProduct.Quantity > 1)
        {
            existingProduct.DecrementQuantity();
        }
        else if (existingProduct != null)
        {
            Products.RemoveAll(product => product.ProductId == productId);
        }
        
        AddDomainEvent(new CheckoutCartProductDeletedEvent(Id, productId));
    }

    public void Archive()
    {
        Status = EntityStatus.Archived;
        AddDomainEvent(new CheckoutCartArchivedEvent(Id));
    }

    private void ValidateRules()
    {
        CheckRule(new CheckoutCartCannotBeArchived(Status));
    }
}