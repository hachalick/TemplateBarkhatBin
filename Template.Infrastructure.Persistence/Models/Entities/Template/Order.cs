using System;
using System.Collections.Generic;

namespace Template.Infrastructure.Persistence.Models.Entities.Template;

public partial class Order
{
    public int Id { get; set; }

    public string? Name { get; set; }
}
