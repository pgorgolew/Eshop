using Eshop.Application.Orders.CustomerOrder.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ardalis.GuardClauses;
using Eshop.API.Examples;
using Eshop.Application.Orders.Customer.Commands;
using Eshop.Application.Orders.Customer.Queries;
using Eshop.Contracts;
using Eshop.Contracts.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace Eshop.API.Controllers;

[ApiController]
[Route("api/v1/customers")]
public class CustomerController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = Guard.Against.Null(sender, nameof(sender));

    /// <summary>
    /// Adds a new customer.
    /// </summary>
    /// <param name="request">The request containing customer details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A newly created customer ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadGateway)]
    [SwaggerRequestExample(typeof(CustomerRequest), typeof(CustomerRequestExample))]
    [SwaggerResponseExample((int)HttpStatusCode.BadGateway, typeof(ErrorDtoExample))]
    public async Task<IActionResult> AddCustomerOrder(
        [FromBody] CustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        var customerId = await _sender.Send(new CreateCustomerCommand(request.Name), cancellationToken);
        return Created($"api/v1/customers", customerId);
    }
    
    /// <summary>
    /// Retrieves the details of a specific customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>The details of the specified customer.</returns>
    [Route("{customerId:guid}")]
    [HttpGet]
    [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadGateway)]
    public async Task<IActionResult> GetCustomerOrderDetails([FromRoute] Guid customerId)
    {
        var customerDetails = await _sender.Send(new GetCustomerQuery(customerId));
        return Ok(customerDetails);
    }
}