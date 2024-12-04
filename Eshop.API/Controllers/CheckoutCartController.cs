using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Ardalis.GuardClauses;
using Eshop.API.Examples;
using Eshop.Application.Orders.CheckoutCart.Commands;
using Eshop.Application.Orders.CheckoutCart.Queries;
using Eshop.Contracts;
using Eshop.Contracts.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace Eshop.API.Controllers;

[ApiController]
[Route("api/v1/customers/{customerId:guid}/checkoutCarts")]
public class CheckoutCartController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = Guard.Against.Null(sender, nameof(sender));

    /// <summary>
    /// Adds a new checkoutCart.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="request">The request containing checkout card details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A newly created checkout cart ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadGateway)]
    [SwaggerRequestExample(typeof(CustomerCheckoutCartRequest), typeof(CustomerCheckoutCartRequestExample))]
    [SwaggerResponseExample((int)HttpStatusCode.BadGateway, typeof(ErrorDtoExample))]
    public async Task<IActionResult> AddCheckoutCart(
        [FromRoute] Guid customerId,
        [FromBody] CustomerCheckoutCartRequest request,
        CancellationToken cancellationToken = default)
    {
        var checkoutCartId =
            await _sender.Send(new CreateCheckoutCartCommand(customerId, request.Products), cancellationToken);
        return Created($"api/v1/customers/{customerId}/orders/{checkoutCartId}", checkoutCartId);
    }

    /// <summary>
    /// Retrieves the details of a specific checkout cart.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="checkoutCartId">The unique identifier of the checkout cart.</param>
    /// <returns>The details of the specified customer.</returns>
    [Route("{checkoutCartId:guid}")]
    [HttpGet]
    [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadGateway)]
    public async Task<IActionResult> GetCustomerOrderDetails([FromRoute] Guid customerId,
        [FromRoute] Guid checkoutCartId)
    {
        var checkoutCartDetails = await _sender.Send(new GetCheckoutCartQuery(customerId, checkoutCartId));
        return Ok(checkoutCartDetails);
    }

    /// <summary>
    /// Adds product to a specific checkout cart.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="checkoutCartId">The unique identifier of the checkout cart.</param>
    /// <param name="productId">The unique identifier of the product to add.</param>
    /// <returns>The unique identifier of the specified checkout cart.</returns>
    [Route("{checkoutCartId:guid}/products/{productId:guid}")]
    [HttpPut]
    [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadGateway)]
    public async Task<IActionResult> AddProductToCheckoutCart([FromRoute] Guid customerId,
        [FromRoute] Guid checkoutCartId, [FromRoute] Guid productId)
    {
        var checkoutCartDetails =
            await _sender.Send(new AddProductToCheckoutCartCommand(checkoutCartId, customerId, productId));
        return Ok(checkoutCartDetails);
    }

    /// <summary>
    /// Removes product from a specific checkout cart.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="checkoutCartId">The unique identifier of the checkout cart.</param>
    /// <param name="productId">The unique identifier of the product to remove.</param>
    /// <returns>The unique identifier of the specified checkout cart.</returns>
    [Route("{checkoutCartId:guid}/products/{productId:guid}")]
    [HttpDelete]
    [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadGateway)]
    public async Task<IActionResult> RemoveProductFromCheckoutCart([FromRoute] Guid customerId,
        [FromRoute] Guid checkoutCartId, [FromRoute] Guid productId)
    {
        var checkoutCartDetails =
            await _sender.Send(new RemoveProductFromCheckoutCartCommand(checkoutCartId, customerId, productId));
        return Ok(checkoutCartDetails);
    }

    /// <summary>
    /// Adds a new order for a specified customer.
    /// </summary>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <param name="checkoutCartId">The unique identifier of the checkout cart.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A newly created order ID.</returns>
    [Route("{checkoutCartId:guid}/order")]
    [HttpPost]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadGateway)]
    [SwaggerRequestExample(typeof(void), typeof(void))]
    [SwaggerResponseExample((int)HttpStatusCode.BadGateway, typeof(ErrorDtoExample))]
    public async Task<IActionResult> AddCustomerOrder(
        [FromRoute] Guid customerId,
        [FromRoute] Guid checkoutCartId,
        CancellationToken cancellationToken = default)
    {
        var orderId = await _sender.Send(new CreateOrderFromCheckoutCartCommand(customerId, checkoutCartId),
            cancellationToken);
        return Created($"api/v1/customers/{customerId}/orders/{orderId}", orderId);
    }
}