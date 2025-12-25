using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface IOutboxEventHandler
    {
        Task HandleAsync(string type, string payload, CancellationToken ct);
    }
}
