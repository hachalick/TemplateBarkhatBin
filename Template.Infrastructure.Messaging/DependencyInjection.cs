using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Interfaces;
using StackExchange.Redis;

namespace Template.Infrastructure.Messaging
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
