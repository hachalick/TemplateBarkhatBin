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

        public static FileJob Create(string filePath)
        {
            var job = new FileJob
            {
                Id = Guid.NewGuid(),
                FilePath = filePath,
                Status = "Pending"
            };

            job.AddDomainEvent(
                new FileUploadedDomainEvent(job.Id));

            return job;
        }

        public void MarkCompleted()
            => Status = "Completed";

        public void MarkFailed()
            => Status = "Failed";

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
    }
}
