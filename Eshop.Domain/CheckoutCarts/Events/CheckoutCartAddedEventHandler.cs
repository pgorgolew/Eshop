using Eshop.Domain.Orders.Events;
using MediatR;

namespace Eshop.Domain.CheckoutCarts.Events;

internal class CheckoutCartAddedEventHandler : INotificationHandler<CheckoutCartAddedEvent>
{
    public Task Handle(CheckoutCartAddedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}