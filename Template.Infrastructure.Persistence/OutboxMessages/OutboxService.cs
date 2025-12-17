using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Models.Entities;

namespace Template.Infrastructure.Persistence.OutboxMessages
{
    public sealed class OutboxService : IOutboxService
    {
        private readonly TemplateBarkhatBinContext _context;

        public OutboxService(TemplateBarkhatBinContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T @event) where T : class
        {
            var message = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = @event.GetType().AssemblyQualifiedName!,
                Content = JsonSerializer.Serialize(@event),
                OccurredOnUtc = DateTime.UtcNow
            };

            _context.OutboxMessages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
