using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Domain.Common;
using Template.Infrastructure.Persistence.Models.Entities;
using Template.Infrastructure.Persistence.Models.Entities.Template;
using Template.Infrastructure.Persistence.OutboxMessages;

namespace Template.Infrastructure.Persistence.Interceptors
{
    public sealed class OutboxSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context == null) return result;

            var domainEntities = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Where(e => e.Entity.DomainEvents.Any())
                .ToList();

            foreach (var entityEntry in domainEntities)
            {
                foreach (var domainEvent in entityEntry.Entity.DomainEvents)
                {
                    var outboxMessage = new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        Type = domainEvent.GetType().AssemblyQualifiedName!,
                        Payload = JsonSerializer.Serialize(domainEvent),
                        OccurredOnUtc = DateTime.UtcNow
                    };

                    context.Set<OutboxMessage>().Add(outboxMessage);
                }

                entityEntry.Entity.ClearDomainEvents();
            }

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
