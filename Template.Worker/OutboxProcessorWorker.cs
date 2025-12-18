using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Models.Entities.Template;
using Template.Infrastructure.Persistence.OutboxMessages;

namespace Template.Worker
{
    public sealed class OutboxProcessorWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OutboxProcessorWorker> _logger;

        public OutboxProcessorWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<OutboxProcessorWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var repo = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();
                var publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var messages = await repo.GetUnprocessedAsync(20);

                foreach (var message in messages)
                {
                    try
                    {
                        var type = Type.GetType(message.Type)!;
                        var payload = JsonSerializer.Deserialize(message.Content, type)!;

                        await publisher.Publish(payload, stoppingToken);

                        await repo.MarkAsProcessedAsync(message.Id);
                    }
                    catch (Exception ex)
                    {
                        await repo.MarkAsFailedAsync(message.Id, ex.Message);
                        _logger.LogError(ex, "Outbox publish failed");
                    }
                }

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
