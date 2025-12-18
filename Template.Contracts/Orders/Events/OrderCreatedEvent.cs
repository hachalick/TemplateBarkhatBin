using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Contracts.Orders.Events
{
    public record OrderCreatedEvent(
        int Id,
        string CustomerName
    );
}
