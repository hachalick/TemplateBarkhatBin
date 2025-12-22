using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Common;

namespace Template.Application.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> events);
    }
}
