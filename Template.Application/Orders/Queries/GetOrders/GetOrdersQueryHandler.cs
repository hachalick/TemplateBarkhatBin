using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities;
using Template.Infrastructure.Persistence.Repository;

namespace Template.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<Order>>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Order>> Handle(
            GetOrdersQuery request,
            CancellationToken cancellationToken)
        {
            return _repository.GetAllAsync();
        }
    }
}
