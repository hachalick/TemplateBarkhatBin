using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Orders.DTOs;

namespace Template.Application.Orders.Queries.GetOrders
{
    public sealed record GetOrdersQuery
        : IRequest<List<OrderDto>>;
}
