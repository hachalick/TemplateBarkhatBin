using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.SignalR
{
    public class NotificationsHub : Hub
    {
        public Task SendToUser(string userId, string message) => Clients.User(userId).SendAsync("ReceiveNotification", message);

        public Task Send(string message) => Clients.All.SendAsync("Receive", message);
    }
}
