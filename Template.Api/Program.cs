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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructurePersistence(builder.Configuration);
builder.Services.AddInfrastructureRabbitMq(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddSignalRModule(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContextSqlServerTemplate>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API v1");
        options.RoutePrefix = "swagger"; // دسترسی در /swagger
        options.DisplayRequestDuration();
        options.EnableTryItOutByDefault();
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health");
app.MapHub<NotificationsHub>("/hubs/notifications");

app.Run();