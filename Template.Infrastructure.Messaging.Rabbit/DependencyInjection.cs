using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Infrastructure.Messaging.Rabbit.Consumer;

namespace Template.Infrastructure.Messaging.Rabbit
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureRabbitMq(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(
                        configuration["RabbitMq:Host"] ?? "localhost",
                        ushort.Parse(configuration["RabbitMq:Port"] ?? "5672"),
                        "/",
                        h =>
                        {
                            h.Username(configuration["RabbitMq:Username"] ?? "guest");
                            h.Password(configuration["RabbitMq:Password"] ?? "guest");
                        });

                    cfg.ReceiveEndpoint("order-created-queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedConsumer>(context);
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r =>
                            r.Interval(3, TimeSpan.FromSeconds(5)));
                    });
                });
            });

            return services;
        }
    }
}
