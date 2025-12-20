using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Infrastructure.FileProcessing;
using Template.Infrastructure.Persistence.Repository;

namespace Template.Worker
{
    public class FileProcessorWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FileProcessorWorker> _logger;

        public FileProcessorWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<FileProcessorWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();

                var repo = scope.ServiceProvider
                    .GetRequiredService<IFileJobRepository>();

                var pendingJobs = await repo.GetPendingAsync(5);

                foreach (var job in pendingJobs)
                {
                    try
                    {
                        ExcelProcessor.Process(job.FilePath);

                        job.MarkCompleted();
                        await repo.SaveAsync(job);
                    }
                    catch (Exception ex)
                    {
                        job.MarkFailed();
                        _logger.LogError(ex, "File processing failed");
                    }
                }

                await Task.Delay(3000, stoppingToken);
            }
        }
    }

}
