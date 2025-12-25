using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;

namespace Template.Worker.Outbox
{
    public sealed class CompositeOutboxEventHandler : IOutboxEventHandler
    {
        private readonly IEnumerable<IOutboxEventHandler> _handlers;

        public CompositeOutboxEventHandler(
            IEnumerable<IOutboxEventHandler> handlers)
        {
            _handlers = handlers;
        }

        public async Task HandleAsync(
            string eventType,
            string payload,
            CancellationToken cancellationToken)
        {
            foreach (var handler in _handlers)
            {
                await handler.HandleAsync(eventType, payload, cancellationToken);
            }
        }
    }
}
