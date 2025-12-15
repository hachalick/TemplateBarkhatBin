using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(string CustomerName) : IRequest<int>;
}
