using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Contracts.Orders.Events;

namespace Template.Infrastructure.Messaging.Rabbit.Consumer
{
    public sealed class OrderCreatedConsumer
        : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(
            ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(
            ConsumeContext<OrderCreatedEvent> context)
        {
            _logger.LogInformation(
                "Order Created | Id: {Id} | Customer: {Customer}",
                context.Message.CustomerName
            );

            return Task.CompletedTask;
        }
    }
}
