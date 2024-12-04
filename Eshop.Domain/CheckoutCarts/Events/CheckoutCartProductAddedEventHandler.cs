using Eshop.Domain.Orders.Events;
using MediatR;

namespace Eshop.Domain.CheckoutCarts.Events;

internal class CheckoutCartProductAddedEventHandler : INotificationHandler<CheckoutCartProductAddedEvent>
{
    public Task Handle(CheckoutCartProductAddedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}