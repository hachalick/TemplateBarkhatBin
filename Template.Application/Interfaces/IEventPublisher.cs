using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync(object @event, CancellationToken cancellationToken);
    }
}
