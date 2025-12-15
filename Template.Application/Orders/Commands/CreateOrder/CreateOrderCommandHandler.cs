using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities;
using Template.Infrastructure.Persistence.Repository;

namespace Template.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Name = request.CustomerName
            };

            await _repository.AddAsync(order);
            return order.Id;
        }
    }
}
