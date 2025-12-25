using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Infrastructure.Persistence.Models.Entities.Template
{
    public partial class OutboxMessage
    {
        public void MarkProcessed()
        {
            OutboxStatus = (byte)EOutboxStatus.Processed;
            ProcessedOnUtc = DateTime.UtcNow;
            Error = null;
        }

        public void MarkFailed(string error)
        {
            RetryCount++;

            OutboxStatus = RetryCount >= 100
                ? (byte)EOutboxStatus.Poison
                : (byte)EOutboxStatus.Failed;

            Error = error;
        }
    }
}
