using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Common;

namespace Template.Infrastructure.Persistence.Models.Entities
{
    public abstract class EntityBase
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }
}
