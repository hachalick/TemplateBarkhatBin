using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.Orders.Events
{
    public record OrderCreatedIntegrationEvent(Guid OrderId, decimal Total, DateTime CreatedAt);
}
