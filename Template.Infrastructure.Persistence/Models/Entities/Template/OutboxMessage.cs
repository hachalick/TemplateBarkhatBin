using System;
using System.Collections.Generic;
using System.Text.Json;
using Template.Domain.Common;

namespace Template.Infrastructure.Persistence.Models.Entities.Template;

public partial class OutboxMessage
{
    public static OutboxMessage From(IDomainEvent domainEvent)
    {
        return new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = domainEvent.GetType().AssemblyQualifiedName!,
            Content = JsonSerializer.Serialize(domainEvent),
            OccurredOnUtc = DateTime.UtcNow
        };
    }

    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime OccurredOnUtc { get; set; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? Error { get; set; }
}
