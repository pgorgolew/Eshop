using Eshop.Contracts.Shared;

namespace Eshop.Contracts;

public class CustomerRequest(string name)
{
    public string Name { get; } = name is null
        ? throw new ArgumentNullException(nameof(name))
        : name;
}