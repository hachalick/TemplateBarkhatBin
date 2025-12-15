using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.Events
{
    public record OrderCreatedIntegrationEvent(Guid OrderId, decimal Total, DateTime CreatedAt);
}
