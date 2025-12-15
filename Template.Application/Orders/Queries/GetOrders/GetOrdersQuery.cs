using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities;

namespace Template.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery : IRequest<List<Order>>;
}
