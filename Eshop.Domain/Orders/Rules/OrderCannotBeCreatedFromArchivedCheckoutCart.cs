using Eshop.Domain.CheckoutCarts;
using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Orders.Rules;

public class OrderCannotBeCreatedFromArchivedCheckoutCart(CheckoutCart checkoutCart) : IBusinessRule
{
    public bool IsBroken() => checkoutCart.Status == EntityStatus.Archived;

    public string Message => "Order cannot be created from archived Checkout Cart";
}