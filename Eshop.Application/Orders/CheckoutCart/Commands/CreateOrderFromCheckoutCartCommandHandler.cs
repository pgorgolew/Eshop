using Eshop.Application.Configuration.Commands;
using Eshop.Domain.CheckoutCarts;
using Eshop.Domain.Orders;
using Eshop.Domain.SeedWork;

namespace Eshop.Application.Orders.CheckoutCart.Commands;

public class CreateOrderFromCheckoutCartCommandHandler(
    IOrderRepository orderCartRepository,
    ICheckoutCartRepository checkoutCartRepository,
    IProductPriceDataApi productPriceDataApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateOrderFromCheckoutCartCommand, Guid>
{
    private readonly IOrderRepository _orderRepository = orderCartRepository ?? throw new ArgumentNullException(nameof(orderCartRepository));
    private readonly ICheckoutCartRepository _checkoutCartRepository = checkoutCartRepository ?? throw new ArgumentNullException(nameof(checkoutCartRepository));
    private readonly IProductPriceDataApi _productPriceDataApi = productPriceDataApi ?? throw new ArgumentNullException(nameof(productPriceDataApi));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Guid> Handle(CreateOrderFromCheckoutCartCommand request, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCartRepository.GetByIdAsync(request.CheckoutCartId, request.CustomerId);
        var productsData = await _productPriceDataApi.Get();

        var order = Order.Create(
            checkoutCart, 
            productsData);

        _orderRepository.Add(order);
        _checkoutCartRepository.Delete(checkoutCart.Id, checkoutCart.CustomerId);

        await _unitOfWork.CommitAsync(cancellationToken);
        
        return order.Id;
    }
}