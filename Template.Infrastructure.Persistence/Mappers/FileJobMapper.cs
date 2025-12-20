using System;
using System.Collections.Generic;
using System.Text;
using DomainJob = Template.Domain.Files.FileJob;
using Entity = Template.Infrastructure.Persistence.Models.Entities.Template.FileJob;

namespace Template.Infrastructure.Persistence.Mappers
{
    public static class FileJobMapper
    {

        public static Entity ToEntity(DomainJob domain)
            => new Entity
            {
                Id = domain.Id,
                FilePath = domain.FilePath,
                Status = domain.Status
            };

        public static DomainJob ToDomain(Entity entity)
            => DomainJob.Load(
                entity.Id,
                entity.FilePath,
                entity.Status);
    }
}
