
namespace Eshop.Domain.CheckoutCarts;

public interface ICheckoutCartRepository
{
    Task<CheckoutCart> GetByIdAsync(Guid checkoutCartId, Guid customerId);

    void Create(CheckoutCart cart);

    void AddProduct(Guid cartId, Guid customerId, Guid productId);

    void DeleteProduct(Guid cartId, Guid customerId, Guid productId);
    
    void Delete(Guid checkoutCartId, Guid customerId);
}