using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface IOutboxProcessor
    {
        Task ProcessAsync(CancellationToken cancellationToken);
    }
}
