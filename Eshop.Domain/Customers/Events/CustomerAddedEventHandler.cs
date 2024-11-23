using MediatR;

namespace Eshop.Domain.Customers.Events;

internal class CustomerAddedEventHandler : INotificationHandler<CustomerAddedEvent>
{
    public Task Handle(CustomerAddedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}