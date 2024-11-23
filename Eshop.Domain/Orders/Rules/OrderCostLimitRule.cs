﻿using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Orders.Rules;

public class OrderCostLimitRule(IReadOnlyCollection<OrderProduct> orderProducts) : IBusinessRule
{
    public bool IsBroken() => orderProducts.Select(op => op.Quantity * op.UnitPrice).Sum() > 15000;

    public string Message => "Order cost cannot be greater than 15000";
}