using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.OutboxMessages;

namespace Template.Infrastructure.Messaging
{
    public class OutboxProcessorWorker : BackgroundService
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

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var outboxRepo =
                    scope.ServiceProvider.GetRequiredService<IOutboxRepository>();

                var publishEndpoint =
                    scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var messages = await outboxRepo.GetUnprocessedAsync(10);

                foreach (var message in messages)
                {
                    try
                    {
                        var type = Type.GetType(message.Type)!;
                        var payload =
                            JsonSerializer.Deserialize(message.Content, type)!;

                        await publishEndpoint.Publish(payload, stoppingToken);

                        await outboxRepo.MarkAsProcessedAsync(message);
                    }
                    catch (Exception ex)
                    {
                        message.Error = ex.Message;
                        _logger.LogError(ex, "Outbox publish failed");
                    }
                }

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
