using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface INotificationPublisher
    {
        Task PublishAsync(string type, string payload);
    }
}
