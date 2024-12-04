using Eshop.Domain.SeedWork;

namespace Eshop.Domain.CheckoutCarts.Events;

public class CheckoutCartAddedEvent(Guid checkoutCartId, Guid customerId) : DomainEventBase
{
    public Guid CheckoutCartId { get; } = checkoutCartId;

    public Guid CustomerId { get; } = customerId;
}