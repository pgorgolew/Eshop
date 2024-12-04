namespace Eshop.Infrastructure.Exceptions;

public class CheckoutCartNotExistsException(Guid id) : Exception($"Checkout cart with ID '{id}' does not exist.")
{
    public Guid Id { get; } = id;
}