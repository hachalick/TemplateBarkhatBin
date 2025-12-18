using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Template.Application.Interfaces;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Models.Entities.Template;

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
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider
                        .GetRequiredService<ApplicationDbContextSqlServerTemplate>();

                    var publisher = scope.ServiceProvider
                        .GetRequiredService<IEventPublisher>();

                    var messages = await dbContext.Set<OutboxMessage>()
                        .Where(m => m.ProcessedOnUtc == null)
                        .OrderBy(m => m.OccurredOnUtc)
                        .Take(20)
                        .ToListAsync(stoppingToken);

                    foreach (var message in messages)
                    {
                        try
                        {
                            var eventType = Type.GetType(message.Type);
                            var domainEvent = JsonSerializer.Deserialize(
                                message.Content, eventType!);

                            await publisher.PublishAsync(domainEvent!, stoppingToken);

                            message.ProcessedOnUtc = DateTime.UtcNow;
                        }
                        catch (Exception ex)
                        {
                            message.Error = ex.Message;
                            _logger.LogError(ex,
                                "Outbox message failed: {MessageId}", message.Id);
                        }
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Outbox worker crashed");
                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
