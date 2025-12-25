using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Common.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
