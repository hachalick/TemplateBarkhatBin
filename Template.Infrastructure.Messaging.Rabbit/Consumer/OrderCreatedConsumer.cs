using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Contracts.Events;

namespace Template.Infrastructure.Messaging.Rabbit.Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedIntegrationEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            return Task.CompletedTask;
        }
    }
}
