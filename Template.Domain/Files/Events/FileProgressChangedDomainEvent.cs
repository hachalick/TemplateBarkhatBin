using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Common;

namespace Template.Domain.Files.Events
{
    public record FileProgressChangedDomainEvent(
        Guid JobId,
        int Progress,
        DateTime OccurredOnUtc
    ) : IDomainEvent;
}
