using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Domain.Files.Events;

namespace Template.SignalR
{
    public sealed class SignalROutboxEventHandler
            : IOutboxEventHandler
    {
        private readonly IHubContext<NotificationsHub> _hub;

        public SignalROutboxEventHandler(
            IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
        }

        public async Task HandleAsync(
            string eventType,
            string payload,
            CancellationToken cancellationToken)
        {
            switch (eventType)
            {
                case nameof(FileProgressChangedDomainEvent):
                    {
                        var evt = JsonSerializer.Deserialize<FileProgressChangedDomainEvent>(payload)!;

                        await _hub.Clients
                            .Group(evt.JobId.ToString())
                            .SendAsync(
                                "fileProgress",
                                new { evt.JobId, evt.Progress },
                                cancellationToken);
                        break;
                    }

                case nameof(FileProcessedDomainEvent):
                    {
                        var evt = JsonSerializer.Deserialize<FileProcessedDomainEvent>(payload)!;

                        await _hub.Clients
                            .Group(evt.JobId.ToString())
                            .SendAsync(
                                "fileCompleted",
                                new { evt.JobId, evt.Status },
                                cancellationToken);
                        break;
                    }
            }
        }
    }
}
