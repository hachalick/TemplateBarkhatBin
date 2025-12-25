using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.DTOs;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.Mappers
{
    internal static class OutboxMessageMapper
    {
        public static OutboxMessage ToEntity(this OutboxMessageDto dto)
            => new()
            {
                Id = dto.Id,
                Type = dto.Type,
                Payload = dto.Payload,
                OccurredOnUtc = dto.OccurredOnUtc,
                OutboxStatus = dto.OutboxStatus,
                ProcessedOnUtc = DateTime.UtcNow,
                RetryCount = dto.RetryCount
            };

        public static OutboxMessageDto ToDto(this OutboxMessage entity)
            => new()
            {
                Id = entity.Id,
                Type = entity.Type,
                Payload = entity.Payload,
                OccurredOnUtc = entity.OccurredOnUtc,
                RetryCount = entity.RetryCount,
                OutboxStatus = entity.OutboxStatus,
            };
    }
}
