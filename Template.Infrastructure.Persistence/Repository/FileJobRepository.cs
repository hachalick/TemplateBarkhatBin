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

        public async Task AddAsync(FileJob job)
        {
            await _context.FileJobs.AddAsync(job.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task<List<FileJob>> GetPendingAsync(int take)
        {
            return await _context.FileJobs
                .Where(x => x.Status == "Pending")
                .Take(take)
                .Select(x => x.ToDomain())
                .ToListAsync();
        }

        public async Task SaveAsync(FileJob job)
        {
            var entity = await _context.FileJobs.FindAsync(job.Id);
            entity.Status = job.Status;
            await _context.SaveChangesAsync();
        }
    }
}
