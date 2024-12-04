using AutoMapper;
using Eshop.Application.Configuration.Queries;
using Eshop.Contracts.Shared;
using Eshop.Domain.Orders;

namespace Eshop.Application.Orders.Customer.Queries;

public class GetCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    : IQueryHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
        return _mapper.Map<CustomerDto>(customer);
    }
}