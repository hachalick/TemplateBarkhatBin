using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;

namespace Template.Infrastructure.Catch
{
    internal class RedisCacheService: ICacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer multiplexer)
        {
            _db = multiplexer.GetDatabase();
        }

        public async Task SetAsync(string key, string value, TimeSpan expiry)
        {
            await _db.StringSetAsync(key, value, expiry);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
