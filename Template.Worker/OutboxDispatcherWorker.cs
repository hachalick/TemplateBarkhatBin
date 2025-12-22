using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.OutboxMessages;

namespace Template.Worker
{
    public class OutboxDispatcherWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OutboxDispatcherWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var repo = scope.ServiceProvider
                    .GetRequiredService<IOutboxRepository>();

                var publisher = scope.ServiceProvider
                    .GetRequiredService<INotificationPublisher>();

                var messages = await repo.GetUnprocessedAsync(10);

                foreach (var msg in messages)
                {
                    try
                    {
                        await publisher.PublishAsync(msg.Type, msg.Content);
                        await repo.MarkAsProcessedAsync(msg.Id);
                    }
                    catch (Exception ex)
                    {
                        await repo.MarkAsFailedAsync(msg.Id, ex.Message);
                    }
                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
