using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.OutboxMessages
{
    public interface IOutboxStore
    {
        Task<List<OutboxMessage>> GetPendingAsync(
            int take,
            CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
