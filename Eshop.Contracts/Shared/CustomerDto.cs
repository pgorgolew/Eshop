namespace Eshop.Contracts.Shared;

public class CustomerDto
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    private CustomerDto()
    {

    }

    public CustomerDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}