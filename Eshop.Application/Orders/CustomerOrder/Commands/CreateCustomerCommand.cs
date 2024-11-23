﻿using Eshop.Application.Configuration.Commands;

namespace Eshop.Application.Orders.CustomerOrder.Commands;

public class CreateCustomerCommand(
    string name) : CommandBase<Guid>
{
    public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
}