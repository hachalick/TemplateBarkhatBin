using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using Template.Contracts.Events;

namespace Template.Contracts.Events
{
    public class OrderCreatedIntegrationEventConsumer : IConsumer<OrderCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            var evt = context.Message;
            // handle: send email, analytics, inventory decrement...
        }
    }
}
