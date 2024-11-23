using Eshop.Domain.SeedWork;

namespace Eshop.Domain.CheckoutCarts.Rules;

public class CheckoutCartCannotBeArchived(EntityStatus checkoutCartStatus) : IBusinessRule
{
    public bool IsBroken() => checkoutCartStatus == EntityStatus.Archived;

    public string Message => "Checkout Cart cannot be archived.";
}