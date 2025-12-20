using MassTransit;
using MassTransit.Transports;
using MediatR;
using System.Text.Json;
using Template.Application.DTOs;
using Template.Application.Interfaces;
using Template.Contracts.Orders.Events;
using Template.Domain.Orders;
using Template.Domain.Orders.Events;

namespace Template.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler
        : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOutboxService _outboxService;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            IOutboxService outboxService)
        {
            _orderRepository = orderRepository;
            _outboxService = outboxService;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Create(request.CustomerName);

            await _orderRepository.AddAsync(order);

            return order.Id;
        }
    }
}
