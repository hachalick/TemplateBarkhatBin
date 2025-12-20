using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Common;

namespace Template.Domain.Files.Events
{
    public class FileUploadedDomainEvent : IDomainEvent
    {
        public Guid FileJobId { get; }

        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

        public FileUploadedDomainEvent(Guid fileJobId)
        {
            FileJobId = fileJobId;
        }
    }
}
