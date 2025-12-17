using Microsoft.EntityFrameworkCore;
using Template.Api.Hubs;
using Template.Api.Middlewares;
using Template.Application;
using Template.Application.Interfaces;
using Template.Infrastructure.Messaging;
using Template.Infrastructure.Messaging.Rabbit;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Persistence.Models.Entities;
using Template.Infrastructure.Persistence.OutboxMessages;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructurePersistence(builder.Configuration)
    .AddInfrastructureRabbitMq(builder.Configuration)
    .AddSignalRModule();

builder.Services.AddDbContext<TemplateBarkhatBinContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<OutboxProcessorWorker>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health");
app.MapHub<NotificationsHub>("/hubs/notifications");

app.Run();