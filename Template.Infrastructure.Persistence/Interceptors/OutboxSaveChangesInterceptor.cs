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
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var domainEntities = context.ChangeTracker
                .Entries<EntityBase>()
                .Where(e => e.Entity.DomainEvents.Any())
                .ToList();

            var outboxMessages = domainEntities
                .SelectMany(e => e.Entity.DomainEvents)
                .Select(domainEvent => OutboxMessageFactory.Create(domainEvent))
                .ToList();

            context.Set<OutboxMessage>().AddRange(outboxMessages);

            domainEntities.ForEach(e => e.Entity.ClearDomainEvents());

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }

}
