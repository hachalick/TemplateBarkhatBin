using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Application.Orders.DTOs;

namespace Template.Application.Orders.Queries.GetOrders
{
    public sealed class GetOrdersQueryHandler
        : IRequestHandler<GetOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderDto>> Handle(
            GetOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _repository.GetAllAsync();

            return orders.Select(o =>
                new OrderDto(o.Id, o.CustomerName)
            ).ToList();
        }
    }
}
