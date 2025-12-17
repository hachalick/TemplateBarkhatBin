using MassTransit;
using MassTransit.Transports;
using MediatR;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Contracts.Orders.Events;
using Template.Domain.Orders;

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

        public async Task<int> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = new Order(
                request.CustomerName,
                request.TotalPrice);

            await _orderRepository.AddAsync(order);

            await _outboxService.AddAsync(
                new OrderCreatedEvent(
                    order.Id,
                    order.CustomerName,
                    order.TotalPrice));

            return order.Id;
        }
    }

}
