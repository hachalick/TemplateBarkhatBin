using System;
using System.Collections.Generic;
using System.Text;
using Template.Domain.Files;

namespace Template.Application.Interfaces
{
    public interface IFileJobRepository
    {
        Task AddAsync(FileJob job);
        Task<List<FileJob>> GetPendingAsync(int take);
        Task SaveAsync(FileJob job);
    }
}
