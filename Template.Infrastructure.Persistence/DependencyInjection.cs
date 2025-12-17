using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Infrastructure.Persistence.Models.Entities;
using Template.Infrastructure.Persistence.OutboxMessages;
using Template.Application.Interfaces;

namespace Template.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TemplateBarkhatBinContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOutboxService, OutboxService>();

            return services;
        }
    }
}
