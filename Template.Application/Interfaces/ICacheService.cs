using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.Interfaces
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value, TimeSpan expiry);

        Task<string?> GetAsync(string key);

        Task RemoveAsync(string key);
    }
}
