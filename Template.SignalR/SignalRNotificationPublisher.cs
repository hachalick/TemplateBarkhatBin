using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;

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
            await _hub.Clients.All.SendAsync(type, payload);
        }
    }
}
