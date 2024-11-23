using Eshop.Domain.Orders.Events;
using MediatR;

namespace Eshop.Domain.CheckoutCarts.Events;

internal class CheckoutCartProductDeletedEventHandler : INotificationHandler<CheckoutCartProductDeletedEvent>
{
    public Task Handle(CheckoutCartProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}