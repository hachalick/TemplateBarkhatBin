using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Orders.Commands.CreateOrder;

namespace Template.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateAsync(CreateOrderCommand command);
    }
}
