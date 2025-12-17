using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;

namespace Template.Infrastructure.Messaging.Rabbit
{
    public sealed class MassTransitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publish;

        public MassTransitEventBus(IPublishEndpoint publish)
        {
            _publish = publish;
        }

        public Task PublishAsync<T>(T @event) where T : class
            => _publish.Publish(@event);
    }
}
