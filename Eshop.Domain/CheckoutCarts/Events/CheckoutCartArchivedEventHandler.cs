using MediatR;

namespace Eshop.Domain.CheckoutCarts.Events;

internal class CheckoutCartArchivedEventHandler : INotificationHandler<CheckoutCartArchivedEvent>
{
    public Task Handle(CheckoutCartArchivedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}