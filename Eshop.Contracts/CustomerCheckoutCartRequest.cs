using Eshop.Contracts.Shared;

namespace Eshop.Contracts;

public class CustomerCheckoutCartRequest(List<ProductDto>? products)
{
    public List<ProductDto> Products { get; } = products ?? [];
}