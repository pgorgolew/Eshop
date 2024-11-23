using Eshop.Application.Configuration.Commands;

namespace Eshop.Application.Orders.CheckoutCart.Commands;

public class RemoveProductFromCheckoutCartCommand(
    Guid checkoutCartId,
    Guid customerId,
    Guid productId) : CommandBase<Guid>
{
    public Guid CheckoutCartId { get; } = checkoutCartId;

    public Guid CustomerId { get; } = customerId;

    public Guid ProductId { get; } = productId;
}