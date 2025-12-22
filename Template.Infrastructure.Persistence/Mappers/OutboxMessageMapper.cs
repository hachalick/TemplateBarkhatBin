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
                Content = dto.Content,
                OccurredOnUtc = dto.OccurredOnUtc
            };

        public static OutboxMessageDto ToDto(this OutboxMessage entity)
            => new()
            {
                Id = entity.Id,
                Type = entity.Type,
                Content = entity.Content,
                OccurredOnUtc = entity.OccurredOnUtc
            };
    }
}
