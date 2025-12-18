using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Common;

namespace Template.Domain.Orders.Events
{
    public sealed class OrderCreatedDomainEvent : IDomainEvent
    {
        public int OrderId { get; }
        public string CustomerName { get; }
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

        public OrderCreatedDomainEvent(
            int orderId,
            string customerName)
        {
            OrderId = orderId;
            CustomerName = customerName;
        }
    }
}
