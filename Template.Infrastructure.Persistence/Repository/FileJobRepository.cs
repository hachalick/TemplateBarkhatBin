using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.Interfaces;
using Template.Domain.Files;
using Template.Infrastructure.Persistence.Context.Template;
using Template.Infrastructure.Persistence.Mappers;
using DomainJob = Template.Domain.Files.FileJob;

namespace Template.Infrastructure.Persistence.Repository
{
    public class FileJobRepository : IFileJobRepository
    {
        private readonly ApplicationDbContextSqlServerTemplate _context;

        public FileJobRepository(ApplicationDbContextSqlServerTemplate context)
        {
            _context = context;
        }

        public async Task AddAsync(DomainJob job)
        {
            var entity = FileJobMapper.ToEntity(job);
            await _context.FileJobs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DomainJob>> GetPendingAsync(int take)
        {
            var entities = await _context.FileJobs
                .Where(x => x.Status == "Pending")
                .Take(take)
                .ToListAsync();

            return entities
                .Select(FileJobMapper.ToDomain)
                .ToList();
        }

        public async Task SaveAsync(DomainJob job)
        {
            var entity = await _context.FileJobs
                .FirstAsync(x => x.Id == job.Id);

            entity.Status = job.Status;
            entity.FilePath = job.FilePath;

            await _context.SaveChangesAsync();
        }
    }
}
