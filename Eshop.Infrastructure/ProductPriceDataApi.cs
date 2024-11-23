using Eshop.Application.Orders;
using Eshop.Domain.Products;
using Eshop.Infrastructure.Exceptions;

namespace Eshop.Infrastructure;

internal class ProductPriceDataApi : IProductPriceDataApi
{
    private readonly List<ProductPriceData> _productPriceData = [
        new ProductPriceData(Guid.Parse("514f6265-a9b8-46da-a31d-50f4f4c20911"), 10),
        new ProductPriceData(Guid.Parse("514f6265-a9b8-46da-a31d-50f4f4c20912"), 20),
        new ProductPriceData(Guid.Parse("514f6265-a9b8-46da-a31d-50f4f4c20913"), 30),
        new ProductPriceData(Guid.Parse("514f6265-a9b8-46da-a31d-50f4f4c20914"), 40)
    ];
    
    public Task<List<ProductPriceData>> Get()
    {
        return Task.FromResult(_productPriceData);
    }

    public Task<ProductPriceData> GetById(Guid productId)
    {
        var product = _productPriceData.FirstOrDefault(productPriceData => productPriceData.ProductId == productId, null);
        
        if (product == null)
        {
            throw new ProductNotExistsException(productId);
        }
        
        return Task.FromResult(product);
    }
}