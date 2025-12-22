using System;
using System.Collections.Generic;
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
            => Status = "Processing";

        public static FileJob Load(
            Guid id,
            string filePath,
            string status)
        {
            return new FileJob
            {
                Id = id,
                FilePath = filePath,
                Status = status
            };
        }

        public void ReportProgress(int percent)
        {
            AddDomainEvent(new FileProgressChangedDomainEvent(
                Id,
                percent,
                DateTime.UtcNow
            ));
        }
    }
}
