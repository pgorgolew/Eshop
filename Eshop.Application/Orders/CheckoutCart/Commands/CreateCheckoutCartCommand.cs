using Eshop.Application.Configuration.Commands;
using Eshop.Contracts.Shared;

namespace Eshop.Application.Orders.CheckoutCart.Commands;

public class CreateCheckoutCartCommand(
    Guid customerId,
    List<ProductDto>? products) : CommandBase<Guid>
{
    public Guid CustomerId { get; } = customerId;

    public List<ProductDto> Products { get; } = products ?? [];
}