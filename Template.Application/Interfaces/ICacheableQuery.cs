using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
    }
}
