using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.DTOs;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Mappers;
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

        public async Task AddAsync(OutboxMessageDto message)
        {
            await _context.OutboxMessages.AddAsync(message.ToEntity());
        }

        public async Task<IReadOnlyList<OutboxMessageDto>> GetUnprocessedAsync(int take)
        {
            return await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .OrderBy(x => x.OccurredOnUtc)
                .Take(take)
                .Select(x => new OutboxMessageDto
                {
                    Id = x.Id,
                    Type = x.Type,
                    Content = x.Content
                })
                .ToListAsync();
        }

        public async Task MarkAsFailedAsync(Guid id, string error)
        {
            var entity = await _context.OutboxMessages.FindAsync(id);
            if (entity is null) return;

            entity.Error = error;
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsProcessedAsync(Guid id)
        {
            var entity = await _context.OutboxMessages.FindAsync(id);
            if (entity is null) return;

            entity.ProcessedOnUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
