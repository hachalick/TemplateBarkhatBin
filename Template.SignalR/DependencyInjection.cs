using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Interfaces;

namespace Template.SignalR
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSignalRModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<INotificationPublisher, SignalRNotificationPublisher>();
            services.AddScoped<IOutboxEventHandler, SignalROutboxEventHandler>();
            return services;
        }
    }
}
