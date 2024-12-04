using Eshop.Application.Configuration.Commands;

namespace Eshop.Application.Orders.CheckoutCart.Commands;

public class CreateOrderFromCheckoutCartCommand(
    Guid customerId,
    Guid checkoutCartId) : CommandBase<Guid>
{
    public Guid CustomerId { get; } = customerId;

    public Guid CheckoutCartId { get; } = checkoutCartId;
}