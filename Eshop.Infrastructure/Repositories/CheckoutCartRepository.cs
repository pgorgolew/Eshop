using Ardalis.GuardClauses;
using Eshop.Domain.CheckoutCarts;
using Eshop.Infrastructure.Database;
using Eshop.Infrastructure.Exceptions;
using MongoDB.Driver;

namespace Eshop.Infrastructure.Repositories;

internal class CheckoutCartRepository(OrdersContext context, IEntityTracker entityTracker) : ICheckoutCartRepository
{
    private readonly OrdersContext _context = context ?? throw new ArgumentNullException(nameof(context));

    private readonly IEntityTracker _entityTracker =
        entityTracker ?? throw new ArgumentNullException(nameof(entityTracker));

    public async Task<CheckoutCart> GetByIdAsync(Guid checkoutCartId, Guid customerId)
    {
        var checkoutCart = _entityTracker.Find<CheckoutCart>(checkoutCartId);
        if (checkoutCart != null) return checkoutCart;

        checkoutCart = await _context.CheckoutCarts.Find(c => c.Id == checkoutCartId && c.CustomerId == customerId).FirstOrDefaultAsync();

        if (checkoutCart == null)
        {
            throw new CheckoutCartNotExistsException(checkoutCartId);
        }

        _entityTracker.Track(checkoutCart);

        return checkoutCart;
    }

    public void Create(CheckoutCart checkoutCart)
    {
        Guard.Against.Null(checkoutCart, nameof(checkoutCart), "Checkout cart is required.");
        _entityTracker.Track(checkoutCart);
    }

    public void AddProduct(Guid cartId, Guid customerId, Guid productId)
    {
        var checkoutCart = GetByIdAsync(cartId, customerId).Result;
        checkoutCart.AddProduct(productId);
        _entityTracker.Track(checkoutCart);
    }

    public void DeleteProduct(Guid cartId, Guid customerId, Guid productId)
    {
        var checkoutCart = GetByIdAsync(cartId, customerId).Result;
        checkoutCart.RemoveProduct(productId);
        _entityTracker.Track(checkoutCart);
    }

    public void Delete(Guid checkoutCartId, Guid customerId)
    {
        var checkoutCart = GetByIdAsync(checkoutCartId, customerId).Result;
        checkoutCart.Archive();
        _entityTracker.Track(checkoutCart);
    }
}