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
    public class OutboxRepository : IOutboxWriter, IOutboxStore
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

        async Task<List<OutboxMessage>> IOutboxStore.GetPendingAsync(int take, CancellationToken cancellationToken)
        {
            return await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null && x.OutboxStatus == (byte)EOutboxStatus.Pending && (x.OccurredOnUtc <= DateTime.UtcNow || x.OutboxStatus == (byte)EOutboxStatus.Failed))
                .OrderBy(x => x.OccurredOnUtc)
                .Take(take)
                .Select(x => new OutboxMessage
                {
                    Id = x.Id,
                    Type = x.Type,
                    Payload = x.Payload
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<OutboxMessageDto>> GetUnprocessedAsync(int take)
        {
            return await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null && x.OutboxStatus == (byte)EOutboxStatus.Pending && (x.OccurredOnUtc <= DateTime.UtcNow || x.OutboxStatus == (byte)EOutboxStatus.Failed))
                .OrderBy(x => x.OccurredOnUtc)
                .Take(take)
                .Select(x => new OutboxMessageDto
                {
                    Id = x.Id,
                    Type = x.Type,
                    Payload = x.Payload
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

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
