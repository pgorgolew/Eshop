namespace Eshop.Contracts.Shared;

public class CheckoutCartDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public List<ProductDto> Products { get; set; }

    private CheckoutCartDto()
    {
        Products = [];
    }

    public CheckoutCartDto(Guid id, Guid customerId, List<ProductDto> products)
    {
        Id = id;
        CustomerId = customerId;
        Products = products ?? throw new ArgumentNullException(nameof(products));
    }
}