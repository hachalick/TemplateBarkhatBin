using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;

namespace Template.Worker.Outbox
{
    public sealed class OutboxEventDispatcher : IOutboxEventDispatcher
    {
        private readonly IOutboxEventHandler _handler;

        public OutboxEventDispatcher(IOutboxEventHandler handler)
        {
            _handler = handler;
        }

        public async Task DispatchAsync(
            string eventType,
            string payload,
            CancellationToken cancellationToken)
        {
            await _handler.HandleAsync(
                eventType,
                payload,
                cancellationToken);
        }
    }
}
