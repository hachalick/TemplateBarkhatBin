using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Contracts.Orders.Events;

namespace Template.Infrastructure.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedIntegrationEvent>
    {
        // inject services (e.g., IHubContext, repos) via constructor if needed
        public OrderCreatedConsumer()
        {
        }

        public Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            var msg = context.Message;
            // TODO: real logic (notify, update read model, etc.)
            Console.WriteLine($"Consumed OrderCreated: {msg.OrderId} - {msg.Total}");
            return Task.CompletedTask;
        }
    }
}
