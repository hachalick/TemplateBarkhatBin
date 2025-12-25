using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Models.Entities.Template;
using Template.Infrastructure.Persistence.OutboxMessages;

namespace Template.Worker.Workers
{
    public class OutboxDispatcherWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OutboxDispatcherWorker> _logger;

        public OutboxDispatcherWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<OutboxDispatcherWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var repository = scope.ServiceProvider
                    .GetRequiredService<IOutboxStore>();

                var publishEndpoint = scope.ServiceProvider
                    .GetRequiredService<IPublishEndpoint>();

                var messages = await repository.GetPendingAsync(20, stoppingToken);

                foreach (var message in messages)
                {
                    try
                    {
                        var type = Type.GetType(message.Type);
                        if (type == null) continue;

                        var payload = JsonSerializer.Deserialize(message.Payload, type);
                        if (payload == null) continue;

                        await publishEndpoint.Publish(payload, stoppingToken);

                        message.MarkProcessed();
                    }
                    catch (Exception ex)
                    {
                        message.MarkFailed(ex.Message);
                        _logger.LogError(ex, "Outbox dispatch failed");
                    }
                }

                await repository.SaveChangesAsync(stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
