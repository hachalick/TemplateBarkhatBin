using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Template.Application.Interfaces;
using Template.Infrastructure.Logging;

namespace Template.Infrastructure.Logging
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
