using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Domain.Common;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.Interceptors
{
    public sealed class OutboxSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var aggregates = context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            foreach (var aggregate in aggregates)
            {
                foreach (var domainEvent in aggregate.DomainEvents)
                {
                    var outbox = OutboxMessage.From(domainEvent);
                    context.Set<OutboxMessage>().Add(outbox);
                }

                aggregate.ClearDomainEvents();
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
