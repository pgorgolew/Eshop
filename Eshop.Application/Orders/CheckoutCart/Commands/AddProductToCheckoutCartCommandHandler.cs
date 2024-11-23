using Eshop.Application.Configuration.Commands;
using Eshop.Domain.CheckoutCarts;
using Eshop.Domain.SeedWork;

namespace Eshop.Application.Orders.CheckoutCart.Commands;

public class AddProductToCheckoutCartCommandHandler(
    ICheckoutCartRepository checkoutCartRepository,
    IProductPriceDataApi productPriceDataApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddProductToCheckoutCartCommand, Guid>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository = checkoutCartRepository ?? throw new ArgumentNullException(nameof(checkoutCartRepository));
    private readonly IProductPriceDataApi _productPriceDataApi = productPriceDataApi ?? throw new ArgumentNullException(nameof(productPriceDataApi));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Guid> Handle(AddProductToCheckoutCartCommand request, CancellationToken cancellationToken)
    {
        EnsureProductExist(request.ProductId);

        _checkoutCartRepository.AddProduct(request.CheckoutCartId, request.CustomerId, request.ProductId);

        await _unitOfWork.CommitAsync(cancellationToken);

        return request.CheckoutCartId;
    }
    
    private void EnsureProductExist(Guid productId)
    {
        _productPriceDataApi.GetById(productId);
    }
}