using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Contracts.Orders.Events;

namespace Template.Infrastructure.Messaging.Kafka
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureMessaging(this IServiceCollection services, IConfiguration config)
        {
            // Register consumers
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedIntegrationEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = config["RabbitMq:Host"] ?? "rabbitmq";
                    var user = config["RabbitMq:Username"] ?? "guest";
                    var pass = config["RabbitMq:Password"] ?? "guest";

                    cfg.Host(host, "/", h =>
                    {
                        h.Username(user);
                        h.Password(pass);
                    });

                    // HERE: this is the code you asked about:
                    cfg.ReceiveEndpoint("order-created-queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedIntegrationEventConsumer>(context);
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    });
                });
            });

            return services;
        }
    }
}
