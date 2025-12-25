using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Infrastructure.Persistence
{
    public enum EOutboxStatus: byte
    {
        Pending = 0,
        Processing = 1,
        Processed = 2,
        Failed = 3,
        Poison = 4
    }
}
