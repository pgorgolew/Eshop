using Eshop.Application.Configuration.Commands;
using Eshop.Domain.CheckoutCarts;
using Eshop.Domain.SeedWork;

namespace Eshop.Application.Orders.CheckoutCart.Commands;

public class RemoveProductFromCheckoutCartCommandHandler(
    ICheckoutCartRepository checkoutCartRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveProductFromCheckoutCartCommand, Guid>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository = checkoutCartRepository ?? throw new ArgumentNullException(nameof(checkoutCartRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Guid> Handle(RemoveProductFromCheckoutCartCommand request, CancellationToken cancellationToken)
    {
        _checkoutCartRepository.DeleteProduct(request.CheckoutCartId, request.CustomerId, request.ProductId);

        await _unitOfWork.CommitAsync(cancellationToken);

        return request.CheckoutCartId;
    }
}