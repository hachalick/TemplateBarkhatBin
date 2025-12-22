using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Domain.Files.Events;

namespace Template.SignalR
{
    public class SignalRNotificationPublisher
        : INotificationPublisher
    {
        private readonly IHubContext<NotificationsHub> _hub;

        public SignalRNotificationPublisher(
            IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
        }

        public async Task PublishAsync(string type, string payload)
        {
            if (type == nameof(FileProgressChangedDomainEvent))
            {
                var evt =
                  JsonSerializer.Deserialize<FileProgressChangedDomainEvent>(payload)!;

                await _hub.Clients
                    .Group(evt.JobId.ToString())
                    .SendAsync("fileProgress", evt.Progress);
            }
        }
    }
}
