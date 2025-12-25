
using Template.Application.DTOs;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.OutboxMessages
{
    public class OutboxService : IOutboxService
    {
        private readonly ApplicationDbContextSqlServerTemplate _context;

        public OutboxService(ApplicationDbContextSqlServerTemplate context)
        {
            _context = context;
        }

        public async Task AddAsync(OutboxMessageDto dto)
        {
            var entity = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = dto.Type,
                Payload = dto.Payload,
                OccurredOnUtc = DateTime.UtcNow,
                RetryCount = 0,
                ProcessedOnUtc = DateTime.UtcNow,
                OutboxStatus = (byte)EOutboxStatus.Pending
            };

            _context.OutboxMessages.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
