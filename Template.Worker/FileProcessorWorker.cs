using OfficeOpenXml;
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
        private readonly IFileJobRepository _repository;

        public FileProcessorWorker(IFileJobRepository repository)
        {
            _repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var jobs = await _repository.GetPendingAsync(5);

                foreach (var job in jobs)
                {
                    try
                    {
                        job.MarkProcessing();
                        await _repository.SaveAsync(job);

                        for (int i = 1; i <= 10; i++)
                        {
                            job.ReportProgress(i * 10);
                            await _repository.SaveAsync(job);
                        }

                        job.MarkCompleted();
                        await _repository.SaveAsync(job);
                    }
                    catch
                    {
                        job.MarkFailed();
                        await _repository.SaveAsync(job);
                    }
                }
            }
        }
    }

}
