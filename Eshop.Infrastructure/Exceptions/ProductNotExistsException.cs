namespace Eshop.Infrastructure.Exceptions;

public class ProductNotExistsException(Guid id) : Exception($"Product with ID '{id}' does not exist.")
{
    public Guid Id { get; } = id;
}