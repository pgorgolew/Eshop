using Eshop.Domain.Orders.Events;
using Eshop.Domain.Orders.Rules;
using Eshop.Domain.Products;
using Eshop.Domain.CheckoutCarts;
using Eshop.Domain.SeedWork;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Eshop.Domain.Orders;

public class Order : Entity, IAggregateRoot
{
    [BsonRepresentation(BsonType.String)]
    public Guid CustomerId { get; private set; }

    public List<OrderProduct> Products { get; private set; }

    private Order(Guid id, Guid customerId, List<OrderProduct> orderProducts) : base(id)
    {
        CustomerId = customerId;
        Products = orderProducts ?? throw new ArgumentNullException(nameof(orderProducts));
    }
    
    private Order(Guid customerId, List<OrderProduct> orderProducts) : base(Guid.NewGuid())
    {
        CustomerId = customerId;
        Products = orderProducts ?? throw new ArgumentNullException(nameof(orderProducts));

        AddDomainEvent(new OrderAddedEvent(Id, customerId));
    }

    public static Order Create(
        Guid customerId,
        List<ProductQuantityData> orderProductsData,
        List<ProductPriceData> allProductPriceData)
    {
        var orderProducts = CreateOrderProducts(orderProductsData, allProductPriceData);
        return new Order(customerId, orderProducts);
    }
    
    public static Order Create(CheckoutCart checkoutCart, List<ProductPriceData> allProductsPriceData)
    {
        CheckRule(new OrderCannotBeCreatedFromArchivedCheckoutCart(checkoutCart));
        
        var orderProducts = CreateOrderProducts(checkoutCart.Products, allProductsPriceData);
        return new Order(checkoutCart.CustomerId, orderProducts);
    }

    private static List<OrderProduct> CreateOrderProducts(List<ProductQuantityData> ordersProductData, List<ProductPriceData> allProductPriceDatas)
    {
        List<OrderProduct> orderProducts = [];

        foreach (var orderProductData in ordersProductData)
        {
            var productPriceData = allProductPriceDatas.First(x => x.ProductId == orderProductData.ProductId);

            var orderProduct = OrderProduct.Create(orderProductData.ProductId, orderProductData.Quantity, productPriceData.UnitPrice);

            orderProducts.Add(orderProduct);
        }

        CheckRule(new OrderMustHaveAtLeastOneProductRule(orderProducts));
        CheckRule(new OrderCostLimitRule(orderProducts));
        
        return orderProducts;
    }
}