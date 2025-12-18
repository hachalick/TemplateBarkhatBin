using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.OutboxMessages
{
    public interface IOutboxRepository
    {
        Task AddAsync(OutboxMessage message);

        Task<List<OutboxMessage>> GetUnprocessedAsync(int take);

        Task MarkAsProcessedAsync(OutboxMessage message);
    }
}
