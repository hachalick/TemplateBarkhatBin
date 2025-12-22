using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Common;

namespace Template.Domain.Files.Events
{
    public record FileProcessedDomainEvent(
        Guid JobId,
        string Status
    ) : IDomainEvent
    {
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}
