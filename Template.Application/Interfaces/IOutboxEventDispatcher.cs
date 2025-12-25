using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface IOutboxEventDispatcher
    {
        Task DispatchAsync(
            string eventType,
            string payload,
            CancellationToken cancellationToken);
    }
}
