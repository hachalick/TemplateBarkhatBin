using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.DTOs
{
    public sealed class OutboxMessageDto
    {
        public Guid Id { get; init; }
        public string Type { get; init; } = default!;
        public string Payload { get; init; } = default!;
        public DateTime OccurredOnUtc { get; init; }
        public byte OutboxStatus { get; set; }
        public int RetryCount { get; set; }

    }
}
