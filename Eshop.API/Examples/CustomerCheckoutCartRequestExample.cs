using Eshop.Contracts;
using Eshop.Contracts.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace Eshop.API.Examples;

public class CustomerCheckoutCartRequestExample : IExamplesProvider<CustomerCheckoutCartRequest>
{
    public CustomerCheckoutCartRequest GetExamples()
    {
        return new CustomerCheckoutCartRequest
        (
            [
                new ProductDto(Guid.Parse("514f6265-a9b8-46da-a31d-50f4f4c20911"), 2),
                new ProductDto(Guid.Parse("514f6265-a9b8-46da-a31d-50f4f4c20912"), 1)
            ]
        );
    }
}