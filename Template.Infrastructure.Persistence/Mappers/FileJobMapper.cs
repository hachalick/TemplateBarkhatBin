using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Infrastructure.Persistence.Mappers
{
    public static class FileJobMapper
    {
        public static Template.Infrastructure.Persistence.Models.Entities.Template.FileJob ToEntity(this Template.Domain.Files.FileJob domain)
            => new Template.Infrastructure.Persistence.Models.Entities.Template.FileJob
            {
                Id = domain.Id,
                FilePath = domain.FilePath,
                Status = domain.Status
            };

        public static Template.Domain.Files.FileJob ToDomain(this Template.Infrastructure.Persistence.Models.Entities.Template.FileJob entity)
            => Template.Domain.Files.FileJob.Load(entity.Id, entity.FilePath, entity.Status);
    }
}
