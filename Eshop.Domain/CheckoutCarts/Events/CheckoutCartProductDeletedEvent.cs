using Eshop.Domain.SeedWork;

namespace Eshop.Domain.CheckoutCarts.Events;

public class CheckoutCartProductDeletedEvent(Guid checkoutCartId, Guid productId) : DomainEventBase
{
    public Guid CheckoutCartId { get; } = checkoutCartId;

    public Guid ProductId { get; } = productId;
}