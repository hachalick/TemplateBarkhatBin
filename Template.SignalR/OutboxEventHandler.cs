using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Domain.Files.Events;

namespace Template.SignalR
{
    public sealed class OutboxEventHandler : IOutboxEventHandler
    {
        private readonly IHubContext<NotificationsHub> _hub;

        public OutboxEventHandler(IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
        }

        public async Task HandleAsync(string type, string payload, CancellationToken ct)
        {
            if (type == nameof(FileProgressChangedDomainEvent))
            {
                var evt = JsonSerializer.Deserialize<FileProgressChangedDomainEvent>(payload)!;

                await _hub.Clients
                    .Group(evt.JobId.ToString())
                    .SendAsync("fileProgress", evt.Progress, ct);
            }
        }
    }
}
