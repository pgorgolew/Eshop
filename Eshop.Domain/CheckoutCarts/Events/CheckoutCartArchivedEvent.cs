using Eshop.Domain.SeedWork;

namespace Eshop.Domain.CheckoutCarts.Events;

public class CheckoutCartArchivedEvent(Guid checkoutCartId) : DomainEventBase
{
    public Guid CheckoutCartId { get; } = checkoutCartId;
}