using Eshop.Domain.SeedWork;

namespace Eshop.Domain.CheckoutCarts.Events;

public class CheckoutCartProductAddedEvent(Guid checkoutCartId, Guid productId) : DomainEventBase
{
    public Guid CheckoutCartId { get; } = checkoutCartId;

    public Guid ProductId { get; } = productId;
}