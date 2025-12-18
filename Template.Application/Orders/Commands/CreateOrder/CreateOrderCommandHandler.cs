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

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = Order.Create(request.CustomerName);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            
            return order.Id;
        }
    }

}
