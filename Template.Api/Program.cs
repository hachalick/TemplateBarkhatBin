using Microsoft.EntityFrameworkCore;
using Template.Api.Middlewares;
using Template.Application;
using Template.Application.Interfaces;
using Template.Infrastructure.Messaging;
using Template.Infrastructure.Messaging.Rabbit;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Models.Entities;
using Template.Infrastructure.Persistence.OutboxMessages;
using Template.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructurePersistence(builder.Configuration);
builder.Services.AddInfrastructureRabbitMq(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddSignalRModule(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContextSqlServerTemplate>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health");
app.MapHub<NotificationsHub>("/hubs/notifications");

app.Run();