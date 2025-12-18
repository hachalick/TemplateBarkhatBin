using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Orders.DTOs
{
    public sealed record OrderDto(
        int Id,
        string CustomerName
    );
}
