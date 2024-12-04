using Eshop.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace Eshop.API.Examples;

public class CustomerRequestExample : IExamplesProvider<CustomerRequest>
{
    public CustomerRequest GetExamples()
    {
        return new CustomerRequest
        (
            "customerName"
        );
    }
}