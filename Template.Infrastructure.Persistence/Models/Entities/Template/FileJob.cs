using System;
using System.Collections.Generic;

namespace Template.Infrastructure.Persistence.Models.Entities.Template;

public partial class FileJob
{
    public Guid Id { get; set; }

    public string Status { get; set; } = null!;

    public string FilePath { get; set; } = null!;
}
