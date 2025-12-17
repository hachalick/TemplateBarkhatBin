using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : class;
    }
}
