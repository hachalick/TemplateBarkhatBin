using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface IOutboxService
    {
        Task AddAsync<T>(T @event) where T : class;
    }
}
