using AutoMapper;
using Eshop.Application.Configuration.Queries;
using Eshop.Contracts.Shared;
using Eshop.Domain.CheckoutCarts;

namespace Eshop.Application.Orders.CheckoutCart.Queries;

public class GetCheckoutCartQueryHandler(ICheckoutCartRepository checkoutCartRepository, IMapper mapper)
    : IQueryHandler<GetCheckoutCartQuery, CheckoutCartDto>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ICheckoutCartRepository _checkoutCartRepository = checkoutCartRepository ?? throw new ArgumentNullException(nameof(checkoutCartRepository));

    public async Task<CheckoutCartDto> Handle(GetCheckoutCartQuery request, CancellationToken cancellationToken)
    {
        var checkoutCart = await _checkoutCartRepository.GetByIdAsync(request.CheckoutCartId, request.CustomerId);
        return _mapper.Map<CheckoutCartDto>(checkoutCart);
    }
}