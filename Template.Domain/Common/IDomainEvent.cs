using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Domain.Common
{
    public interface IDomainEvent
    {
        DateTime OccurredOnUtc { get; }
    }
}
