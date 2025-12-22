using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.DTOs;
using Template.Application.Interfaces;
using Template.Domain.Common;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.OutboxMessages
{
    public sealed class OutboxDomainEventDispatcher
        : IDomainEventDispatcher
    {
        private readonly IOutboxRepository _repository;

        public OutboxDomainEventDispatcher(
            IOutboxRepository repository)
        {
            _repository = repository;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvent in events)
            {
                var dto = new OutboxMessageDto
                {
                    Id = Guid.NewGuid(),
                    Type = domainEvent.GetType().Name,
                    Content = JsonSerializer.Serialize(domainEvent),
                    OccurredOnUtc = DateTime.UtcNow
                };

                await _repository.AddAsync(dto);
            }
        }
    }
}
