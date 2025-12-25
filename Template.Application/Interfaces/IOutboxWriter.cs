using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.DTOs;

namespace Template.Application.Interfaces
{
    public interface IOutboxWriter
    {
        Task AddAsync(OutboxMessageDto message);

        Task<IReadOnlyList<OutboxMessageDto>> GetUnprocessedAsync(int take);

        Task MarkAsProcessedAsync(Guid id);

        Task MarkAsFailedAsync(Guid id, string error);
    }
}
