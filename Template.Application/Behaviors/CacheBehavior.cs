using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.Interfaces;

namespace Template.Application.Behaviors
{
    public class CacheBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableQuery
    {
        private readonly ICacheService _cache;

        public CacheBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var cacheKey = request.CacheKey;

            // check cached
            var cached = await _cache.GetAsync(cacheKey);
            if (cached is not null)
                return JsonSerializer.Deserialize<TResponse>(cached)!;

            // continue
            var response = await next();

            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(response), TimeSpan.FromMinutes(2));

            return response;
        }
    }
}
