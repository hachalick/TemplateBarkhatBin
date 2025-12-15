using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Messaging.Rabbit.Consumer;

namespace Template.Api.Hubs
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSignalRModule(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }
    }
}
