using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Template.Domain.Common;
using Template.Domain.Files.Events;

namespace Template.Domain.Files
{
    public class FileJob: AggregateRoot
    {
        public Guid Id { get; private set; }
        public string FilePath { get; private set; }
        public string Status { get; private set; }
        public int Progress { get; private set; }

        private FileJob() { }

        public FileJob(Guid id, string filePath)
        {
            Id = id;
            FilePath = filePath;
            Status = "Pending";
        }

        public void MarkCompleted()
        {
            Status = "Completed";
            AddDomainEvent(new FileProcessedDomainEvent(Id, Status));
        }

        public void MarkFailed()
        {
            Status = "Failed";
            AddDomainEvent(new FileProcessedDomainEvent(Id, Status));
        }

        public void MarkProcessing()
        {
            Status = "Processing";
            AddDomainEvent(new FileProcessingStartedDomainEvent(Id));
        }

        public static FileJob Load(
            Guid id,
            string filePath,
            string status,
            int progress)
        {
            return new FileJob
            {
                Id = id,
                FilePath = filePath,
                Status = status,
                Progress = progress
            };
        }

        public void ReportProgress(int progress)
        {
            if (progress < 0 || progress > 100)
                throw new InvalidOperationException("Progress must be between 0 and 100");

            Progress = progress;

            AddDomainEvent(new FileProgressChangedDomainEvent(Id, Progress));
        }
    }
}
