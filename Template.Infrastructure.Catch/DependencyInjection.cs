using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;

namespace Template.Infrastructure.Catch
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureCache(this IServiceCollection services, IConfiguration config)
        {
            var redisConn = config.GetConnectionString("Redis") ?? config["Redis:Connection"] ?? "localhost:6379";
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConn));
            services.AddScoped<ICacheService, RedisCacheService>();
            return services;
        }
    }
}
