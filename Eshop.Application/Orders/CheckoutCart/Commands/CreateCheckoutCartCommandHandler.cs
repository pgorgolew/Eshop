using AutoMapper;
using Eshop.Application.Configuration.Commands;
using Eshop.Contracts.Shared;
using Eshop.Domain.CheckoutCarts;
using Eshop.Domain.Products;
using Eshop.Domain.SeedWork;

namespace Eshop.Application.Orders.CheckoutCart.Commands;

public class CreateCheckoutCartCommandHandler(
    ICheckoutCartRepository checkoutCartRepository,
    IProductPriceDataApi productPriceDataApi,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : ICommandHandler<CreateCheckoutCartCommand, Guid>
{
    private readonly ICheckoutCartRepository _checkoutCartRepository = checkoutCartRepository ?? throw new ArgumentNullException(nameof(checkoutCartRepository));
    private readonly IProductPriceDataApi _productPriceDataApi = productPriceDataApi ?? throw new ArgumentNullException(nameof(productPriceDataApi));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Guid> Handle(CreateCheckoutCartCommand request, CancellationToken cancellationToken)
    {
        EnsureProductsExist(request.Products);
        
        var checkoutCart = Domain.CheckoutCarts.CheckoutCart.Create(request.CustomerId, request.Products.Select(_mapper.Map<ProductQuantityData>).ToList());

        _checkoutCartRepository.Create(checkoutCart);

        await _unitOfWork.CommitAsync(cancellationToken);

        return checkoutCart.Id;
    }

    private void EnsureProductsExist(List<ProductDto> products)
    {
        products.Select(product => _productPriceDataApi.GetById(product.Id)).ToList().EnsureCapacity(products.Count);
    }
}