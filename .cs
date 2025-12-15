public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
{
    services.AddSingleton<IConnectionMultiplexer>(sp =>
        ConnectionMultiplexer.Connect(config.GetConnectionString("Redis")));

    services.AddScoped<ICacheService, RedisCacheService>();

    return services;
}
