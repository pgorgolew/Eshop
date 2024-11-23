using Eshop.Application.Configuration.Queries;
using Eshop.Contracts.Shared;

namespace Eshop.Application.Orders.CustomerOrder.Queries;

public class GetCustomerQuery(Guid customerId) : IQuery<CustomerDto>
{
    public Guid CustomerId { get; } = customerId;
}