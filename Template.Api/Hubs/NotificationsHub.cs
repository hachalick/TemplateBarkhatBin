using Microsoft.AspNetCore.SignalR;

namespace Template.Api.Hubs
{
    public class NotificationsHub : Hub
    {
        public Task SendToUser(string userId, string message) => Clients.User(userId).SendAsync("ReceiveNotification", message);

        public Task Send(string message) => Clients.All.SendAsync("Receive", message);
    }
}
