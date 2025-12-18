using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Template.Infrastructure.Persistence.Context.Template;

namespace Template.Worker
{
    public class OutboxProcessorWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OutboxProcessorWorker> _logger;

        public OutboxProcessorWorker(
            IServiceScopeFactory scopeFactory,
            IPublishEndpoint publishEndpoint,
            ILogger<OutboxProcessorWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContextSqlServerTemplate>();

                var messages = await db.OutboxMessages
                    .Where(x => x.ProcessedOnUtc == null)
                    .Take(20)
                    .ToListAsync(stoppingToken);

                foreach (var message in messages)
                {
                    var type = Type.GetType(message.Type)!;
                    var payload = JsonSerializer.Deserialize(message.Content, type)!;

                    await _publishEndpoint.Publish(payload, stoppingToken);

                    message.ProcessedOnUtc = DateTime.UtcNow;
                }

                await db.SaveChangesAsync(stoppingToken);
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
