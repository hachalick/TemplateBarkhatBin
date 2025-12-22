using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Interceptors;
using Template.Infrastructure.Persistence.OutboxMessages;

namespace Template.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDomainEventDispatcher, OutboxDomainEventDispatcher>();

            services.AddScoped<OutboxSaveChangesInterceptor>();

            services.AddDbContext<ApplicationDbContextSqlServerTemplate>((sp, options) =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                options.AddInterceptors(
                    sp.GetRequiredService<OutboxSaveChangesInterceptor>());
            });

            return services;
        }
    }
}
