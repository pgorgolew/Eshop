using Eshop.Application.Configuration.Queries;
using Eshop.Contracts.Shared;

namespace Eshop.Application.Orders.CheckoutCart.Queries;

public class GetCheckoutCartQuery(Guid customerId, Guid checkoutCartId) : IQuery<CheckoutCartDto>
{
    public Guid CustomerId { get; } = customerId;

    public Guid CheckoutCartId { get; } = checkoutCartId;
}