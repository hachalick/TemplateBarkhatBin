using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.OutboxMessages
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly ApplicationDbContextSqlServerTemplate _context;

        public OutboxRepository(ApplicationDbContextSqlServerTemplate context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessage message)
        {
            _context.OutboxMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OutboxMessage>> GetUnprocessedAsync(int take)
        {
            return await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .OrderBy(x => x.OccurredOnUtc)
                .Take(take)
                .ToListAsync();
        }

        public async Task MarkAsProcessedAsync(OutboxMessage message)
        {
            message.ProcessedOnUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
