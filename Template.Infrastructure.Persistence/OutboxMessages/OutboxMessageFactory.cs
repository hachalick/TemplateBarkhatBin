using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Domain.Common;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.OutboxMessages
{
    public static class OutboxMessageFactory
    {
        public static OutboxMessage Create(IDomainEvent domainEvent)
        {
            return new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = domainEvent.GetType().AssemblyQualifiedName!,
                Content = JsonSerializer.Serialize(domainEvent),
                OccurredOnUtc = DateTime.UtcNow
            };
        }
    }
}
